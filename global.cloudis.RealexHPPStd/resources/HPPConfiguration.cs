using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace global.cloudis.RealexHPP.resources
{
    public class HPPConfiguration : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var messagedict = new Dictionary<string, string>();

            var messages = section.SelectNodes(@"messages");

            foreach (XmlNode message in messages.Item(0).ChildNodes)
            {
                string messagename = message.SelectSingleNode(@"@name").InnerText;
                string messagetext = message.InnerText;
                messagedict.Add(messagename, messagetext);
                
            }
            return messagedict;
        }
    }
}
