using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ChatBot.Models;

namespace ChatBot
{
    public class Entities
    {
        public Sites Sites
        {
            get
            {
                var xml = XDocument.Load($"{HostingEnvironment.ApplicationPhysicalPath}/Resource/sites.xml");
                var serializer = new XmlSerializer(typeof(Sites));
                return (Sites) serializer.Deserialize(xml.CreateReader());
            }
        }

        public Report Report => new Report();
    }

    [Serializable]
    [XmlRoot]
    public class Sites
    {
        [XmlElement("Site")]
        public Site[] Site { get; set; }
    }

    [Serializable]
    public class Site
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Enviroment")]
        public Enviroment[] Enviroment { get; set; }
    }

    [Serializable]
    public class Enviroment
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("link")]
        public string Link { get; set; }
    }


}