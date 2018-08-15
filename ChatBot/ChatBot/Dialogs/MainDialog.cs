using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace ChatBot.Dialogs
{
    public class MainDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
                new RegexCase<IDialog<string>>(new Regex("^Report", RegexOptions.IgnoreCase), (context, txt) => FormDialog.FromForm(Report.BuildForm, FormOptions.PromptInStart).ContinueWith(AfterGreetingContinuation)),
                new DefaultCase<string, IDialog<string>>((context, txt) => new RootDialog().ContinueWith(AfterGreetingContinuation))
                )
            .Unwrap()
            .PostToUser();

        public static async Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> res)
        {
            return Chain.Return("Thank you for using the FTbot");
        }
    }
}