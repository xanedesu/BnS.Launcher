using System.ComponentModel.Composition;
using System.IO;
using System.Xml.Serialization;
using Unlakki.Bns.Launcher.Shared.Models.GameConfig;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
{
    [Export(typeof(IGamesConfigParser))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class GamesConfigXmlParser : IGamesConfigParser
    {
        public GamesConfig Parse(string data)
        {
            using (TextReader textReader = new StringReader(data))
            {
                return (GamesConfig) new XmlSerializer(typeof(GamesConfig)).Deserialize(textReader);
            }
        }
    }
}
