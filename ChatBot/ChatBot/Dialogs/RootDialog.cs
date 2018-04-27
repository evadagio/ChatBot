using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var userName = context.MakeMessage().Recipient.Name;
            await context.PostAsync($"Hi {userName}, how may I help you?");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var msg = await argument;
            await Respond(context);
            context.Done(msg);

            /* var activity = await result as Activity;
 
             // calculate something for us to return
             int length = (activity.Text ?? string.Empty).Length;
 
             // return our reply to the user
             await context.PostAsync($"You sent {activity.Text} which was {length} characters");
 
             context.Wait(MessageReceivedAsync);*/
        }

        private static async Task Respond(IDialogContext context)
        {
            await context.PostAsync("You may select from the following options.");
        }
    }
}