using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Modulo3.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("**olá tudo bem?**");
            var message = activity.CreateReply();

            var activityText = activity.Text.Trim().ToLower();
            if (activityText == "herocard")
            {
                Attachment heroCard = CreateHeroCard();
                message.Attachments.Add(heroCard);
            }
            else if (activityText == "videocard")
            {
                Attachment videoCard = CreateVideoCard();
                message.Attachments.Add(videoCard);
            }
            else if (activityText == "audiocard")
            {
                Attachment audioCard = CreateAudioCard();
                message.Attachments.Add(audioCard);
            }
            else if (activityText == "animationcard")
            {
                var annimationCard = CreateAnimationCard();
                message.Attachments.Add(annimationCard);
            }
            else if (activityText == "carouselcard")
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                message.Attachments.Add(CreateAnimationCard());
                message.Attachments.Add(CreateAudioCard());
                message.Attachments.Add(CreateHeroCard());
                message.Attachments.Add(CreateVideoCard());
            }
            else
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                message.Text = $"You sent '{activity.Text}' which was {length} characters";
            }
            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAudioCard()
        {
            return new AudioCard()
            {
                Title = "Um audio qualquer",
                Subtitle = "Apenas um audio",
                Autostart = true,
                Autoloop = false,
                Media = new List<MediaUrl>() {
                        new MediaUrl("http://www.wavlist.com/humor/001/aerorubb.wav")
                    }
            }.ToAttachment();
        }

        private Attachment CreateVideoCard()
        {
            return new VideoCard()
            {
                Title = "Um vídeo de finanças",
                Subtitle = "O canal mais rico do Brasil",
                Autostart = true,
                Autoloop = false,
                Media = new List<MediaUrl>() {
                        new MediaUrl("https://www.youtube.com/watch?v=bZahWOlKn78")
                    }
            }.ToAttachment();
        }

        private Attachment CreateHeroCard()
        {
            var heroCard = new HeroCard();
            heroCard.Title = "Título";
            heroCard.Subtitle = "Subtitulo";
            heroCard.Images = new List<CardImage>() {
                    new CardImage("http://www.lopes.com.br/blog/wp-content/uploads/2017/04/sunrise-1756274_640.jpg", "planeta")
                };
            return heroCard.ToAttachment();
        }

        private Attachment CreateAnimationCard()
        {
            var audioCard = new AnimationCard()
            {
                Title = "Um gif fofo",
                Subtitle = "curtiu né?",
                Autostart = true,
                Autoloop = false,
                Media = new List<MediaUrl>() {
                        new MediaUrl("https://img.buzzfeed.com/buzzfeed-static/static/enhanced/webdr06/2013/5/31/10/anigif_enhanced-buzz-3734-1370010471-16.gif")
                    }
            };
            return audioCard.ToAttachment();

        }
    }
}