using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public bool Reset;
        public static Entities Entity = new Entities();
        public static WebSites WebSites = new WebSites();
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi I'm FT Bot");
            await context.PostAsync(CreateMenu(context));
            context.Wait(MessageReceivedAsync);
        }


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text != null && Entity.Items.Contains(message.Text))
            {
                switch (message.Text)
                {
                    case "Sites":
                        HandleSites(context);
                        break;
                    case "Report":
                        context.Call(FormDialog.FromForm(Report.BuildForm, FormOptions.PromptInStart), ResumeAll);
                        break;
                    case "Help":
                        await context.PostAsync(Entity.Help);
                        break;
                }
            }
            else
            {
                await context.PostAsync(CreateMenu(context));
            }
        }

        private async Task ResumeSite(IDialogContext context, IAwaitable<string> result)
        {
            var site = await result;
            var attachments = new List<Attachment>();
            //var actions = site.Enviroments.Select(item => new CardAction(ActionTypes.OpenUrl, item.Key, value: item.Link)).ToList();
            var actions = WebSites.GetEnviroments(site)
                .Select(item => new CardAction(ActionTypes.OpenUrl, item.Key, value: item.Link)).ToList();
            var heroCard = new HeroCard()
            {
                Buttons = actions
            };
            attachments.Add(heroCard.ToAttachment());
            var reply = context.MakeMessage();
            reply.Text = string.Empty;
            reply.AttachmentLayout = AttachmentLayoutTypes.List;
            reply.Attachments = attachments;
            await context.PostAsync(reply);
        }

        private void HandleSites(IDialogContext context)
        {

            var options = new List<string>();
            var descriptions = new List<string>();
            foreach (var site in WebSites.GetSites())
            {
                options.Add(site.Key);
                descriptions.Add(site.Name);
            }

            PromptDialog.Choice(context, ResumeSite, options, "  ", descriptions: descriptions);
        }

        private static IMessageActivity CreateMenu(IDialogContext context)
        {
            var attachments = new List<Attachment>();
            var actions = new List<CardAction>();
            foreach (var item in Entity.Items)
            {
                actions.Add(new CardAction(ActionTypes.PostBack, item, value: item));
            }
            var heroCard = new HeroCard()
            {
                Buttons = actions
            };
            attachments.Add(heroCard.ToAttachment());
            var reply = context.MakeMessage();
            reply.Text = "You may select from the following options.";
            reply.AttachmentLayout = AttachmentLayoutTypes.List;
            reply.Attachments = attachments;
            return reply;
        }


        private async Task ResumeAll(IDialogContext context, IAwaitable<Report> result)
        {
            context.Done("");
        }

    }

  
}