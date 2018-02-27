using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo3.Dialogs
{
    [LuisModel(modelID: "14c1529e-c9d5-497a-ab07-93f148fff460", subscriptionKey: "4a8bdcfbfa904c2f886f5787259d6f6a")]
    [Serializable]
    public class CotacaoDialog : LuisDialog<object>
    {
        [LuisIntent(intentName: "None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender a frase {result.Query}.");
        }

        [LuisIntent(intentName: "Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eu sou um bot e estou sempre aprendendo, tenha paciência comigo.");
        }


        [LuisIntent(intentName: "Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Olá eu sou um bot que faz cotação de moedas.");
        }


        [LuisIntent(intentName: "Cotação")]
        public async Task Cotacao(IDialogContext context, LuisResult result)
        {
            var moedas = result.Entities?.Select(p => p.Entity);

            await context.PostAsync($"Eu farei uma cotação para as moedas e criptomoedas {string.Join(",", moedas.ToArray())}.");
        }

    }
}
