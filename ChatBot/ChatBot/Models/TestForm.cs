using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Models
{
    [Serializable]
    public class TestForm
    {
        public static IForm<TestForm> BuildForm()
        {
            return new FormBuilder<TestForm>()
                .Message("Test").Build();
        }

    }
}