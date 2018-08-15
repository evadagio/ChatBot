using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;

namespace ChatBot
{
    public class Entities
    {
        public List<string> Items
        {
            get
            {
                var list = GetType().GetProperties().Select(item => item.Name).ToList();
                list.Remove("Items");
                return list;
            }
        }

        public WebSites Sites => new WebSites();
        public Report Report => new Report();
        public string Help => "Please contact Peter Tribe at team area.";

    }


}