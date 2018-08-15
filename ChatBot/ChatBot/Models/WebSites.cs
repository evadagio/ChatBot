using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ChatBot.Models
{
    public class WebSites
    {
        public Sites Sites
        {
            get
            {
                var xml = XDocument.Load($"{HostingEnvironment.ApplicationPhysicalPath}/Resource/sites.xml");
                var serializer = new XmlSerializer(typeof(Sites));
                return (Sites)serializer.Deserialize(xml.CreateReader());
            }
        }

        public Site[] GetSites()
        {
            return Sites.Site;
        }

        public Site GetSite(string site)
        {
            return Sites.Site.First(m => m.Key.Equals(site));
        }

        public Enviroments[] GetEnviroments(string site)
        {
            return GetSite(site).Enviroments;
        }

        public Enviroments GetEnviroment(Site site, string en)
        {
            return site.Enviroments.First(m => m.Key.Equals(en));
        }
        /*public Site[] Options
        {
            get
            {
                var inner = new Dictionary<string, Enviroments>();
                var outter = new Dictionary<string, Dictionary<string,Site>>();

                foreach (var item in Sites.Site)
                {
                    foreach (var i in item.Enviroment)
                    {
                        inner.Add(i.Key,i);
                    }
                    outter.Add(item.Key,inner);
                }
            }
        }*/
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
        public Enviroments[] Enviroments { get; set; }
    }

    [Serializable]
    public class Enviroments
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("link")]
        public string Link { get; set; }
    }

}