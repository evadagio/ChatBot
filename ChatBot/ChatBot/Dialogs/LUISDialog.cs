using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ChatBot.Dialogs
{
   // [LuisModel()]
    [Serializable]
    public class LUISDialog : LuisDialog<TestDialog>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
           
        }


        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

    }
}