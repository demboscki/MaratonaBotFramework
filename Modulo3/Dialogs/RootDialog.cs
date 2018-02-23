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
        private const string SEGREDO = "segredo";
        private const string OPCAO1 = "OPCAO1";
        private const string OPCAO2 = "OPCAO2";
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
                var heroCard = CreateHeroCard();
                heroCard.Buttons = new List<CardAction>() {
                    new CardAction (){
                        Text ="Quer saber meu segredo?",
                        DisplayText = "Display",
                        Title = "Title",
                        Type = ActionTypes.PostBack ,
                        Value = SEGREDO
                    }
                };
                message.Attachments.Add(heroCard.ToAttachment());

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
                message.Attachments.Add(CreateHeroCard().ToAttachment());
                message.Attachments.Add(CreateVideoCard());
            }
            else if (activityText == SEGREDO.ToLower())
            {
                message.Text = "Peidei :)";
            }
            else if (activityText == "menu")
            {
                var btn = new HeroCard();
                btn.Buttons = new List<CardAction>{
                    new CardAction()
                    {
                        DisplayText = "Display",
                        Text = "Texto",
                        Title = "1 Inscrições",
                        Image = "http://www.icons101.com/icon_ico/id_36436/Mushroom__1UP.ico",
                        Type = ActionTypes.PostBack,
                        Value = OPCAO1
                    },
                    new CardAction()
                    {
                        DisplayText = "Display",
                        Text = "Texto",
                        Title = "2 Informações",
                        Image = "http://www.icons101.com/icon_ico/id_36436/Mushroom__1UP.ico",
                        Type = ActionTypes.PostBack,
                        Value = OPCAO2
                    }
                };
                message.Attachments.Add(btn.ToAttachment());
            }
            else if (activityText == OPCAO1.ToLower()) {
                message.Text = "inscrições x y z";
            }
            else if (activityText == OPCAO2.ToLower())
            {
                message.Text = "essa é uma aplicação de estudo.";
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

        private HeroCard CreateHeroCard()
        {
            var heroCard = new HeroCard();
            heroCard.Title = "Título";
            heroCard.Subtitle = "Subtitulo";
            heroCard.Images = new List<CardImage>() {
                    new CardImage("http://www.lopes.com.br/blog/wp-content/uploads/2017/04/sunrise-1756274_640.jpg"
                    , "planeta"
                    , new CardAction(
                        ActionTypes.OpenUrl
                        , "Microsoft"
                        , null
                        , "http://microsoft.com.br"
                    )
                )
            };
            return heroCard;
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