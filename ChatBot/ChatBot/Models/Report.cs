using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Models
{
    public enum Enviroment
    {
        Produtction = 1,
        OOR = 2,
        Staging = 3,
    }

    public enum Reproducibility
    {
        Always = 1,
        Sometimes = 2,
        Rarely = 3,
        Unable = 4
    }

    public enum Priority
    {
        Crtitical = 1,
        Major = 2,
        Minor = 3,
        Trivial = 4,
        Other = 5
    }

    [Serializable]
    public class Report
    {
        public string Title;
        [Prompt("Enter a description for your report")]
        public string Description;
        [Prompt("What is your name?")]
        public string Name;
        public Enviroment Enviroment;
        public Priority Priority;
        public Reproducibility Reproduce;
        
        public static IForm<Report> BuildForm()
        {
            return new FormBuilder<Report>().Message("Please fill in the form:").Build();
        }
    }
}
