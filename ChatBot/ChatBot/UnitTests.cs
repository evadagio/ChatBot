using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatBot
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test()
        {
            Entities a = new Entities();
            var b = a.Sites;
            var c = b.Site;
            

        }
    }
}