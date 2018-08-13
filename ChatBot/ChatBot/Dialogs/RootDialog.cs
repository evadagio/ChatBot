using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml.Linq;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public bool Reset;

        /*public RootDialog(bool reset)
        {
            this.Reset = reset;
        }*/
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi I'm FT Bot");
            //await Respond(context);
            await context.PostAsync(CreateMenu(context));
            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            /*var userName = String.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hi {0}.  How are you today?", userName));
            }*/

            //await context.PostAsync(CreateMenu(context));

        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var userName = String.Empty;
            var getName = false;
            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);
            
            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
                //await Respond(context);
                context.Wait(MessageReceivedAsync);
            }
            /*else
            {
                context.Done(message);
            }*/


        }

        /* public async Task StartAsync(IDialogContext context)
         {
             //await Respond(context);
             context.Wait(MessageReceivedAsync);
         }

         public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
         {
             var message = await argument;
             var res = Reset ? "initial" : message.Text;
             switch (res)
             {
                 case "site":
                     var attachments = new List<Attachment>();
                     var actions = new List<CardAction>();
                     var items = new Dictionary<string,string>();
                     items["Fulfillment Dashboard - STG"] = "https://fulfillment-dashboard.pnistaging.com/";
                     items["Fulfillment Dashboard - PRD"] = "https://fulfillment-dashboard.pnimedia.com/";
                     items["Kibana - STG"] = "https://b3f3ad2ed530485c31ba565d8e1153ca.us-east-1.aws.found.io";
                     items["Kibana - PRD"] = "https://1a5ce950fbd8aaa5ae217310eb29a984.us-east-1.aws.found.io";

                     foreach (var item in items)
                     {
                         actions.Add(new CardAction(ActionTypes.OpenUrl, item.Key, value:item.Value));
                     }
                     await context.PostAsync(CreateActivity(context,actions,attachments));
                     break;
                 case "help":
                     await context.PostAsync("Help page on construction!");
                     break;
                 case "initial":
                     await context.PostAsync($"Hi {context.MakeMessage().Recipient.Name}, Welcome to FT-Bot!");
                     var attach = new List<Attachment>();
                     var act = new List<CardAction>();
                     var its = new List<string>()
                     {
                         "site",
                         "help",
                         "other"
                     };
                     foreach (var it in its)
                     {
                         act.Add(new CardAction(ActionTypes.ImBack, it, text:it, value:it));
                     }
                     await context.PostAsync(CreateActivity(context, act, attach));
                     break;
                 default:
                     await context.PostAsync("WTF?");
                     break;
             }
             Reset = false;
             context.Done(message);
         }
        /* private static async Task Respond(IDialogContext context, string message = "initial")
         {

             //context.Done(message);
         }#1#
         */
        private static IMessageActivity CreateActivity(IDialogContext context, List<CardAction> actions, List<Attachment> attachments)
        {
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

        private static IMessageActivity CreateMenu(IDialogContext context)
        {
            var attachments = new List<Attachment>();
            var actions = new List<CardAction>();
            var c = HostingEnvironment.ApplicationPhysicalPath;
            Entities entity = new Entities();
            
            actions.Add(new CardAction(ActionTypes.ImBack, "Sites", value: entity.Sites));
            actions.Add(new CardAction(ActionTypes.ImBack, "Report", value: entity.Report));
            actions.Add(new CardAction(ActionTypes.ImBack, "Help", value:"Help"));
            
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
    }

  
}