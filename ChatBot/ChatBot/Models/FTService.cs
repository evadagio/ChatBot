using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Models
{
    public enum Enviroment
    {
        Staging,
        Production
    }

    public enum Sites
    {
        VSTS,
        FulfilmentPage,
        JIRA,
        FulfilmentDashboard,
        Kibana
    }

    [Serializable]
    public class FTService
    {
        public Sites? Sites;
        public Enviroment? Enviroment;

        public static IForm<FTService> BuildForm()
        {
            return new FormBuilder<FTService>().Message("Welcome to FT Bot!").Build();
        }
    }
}
