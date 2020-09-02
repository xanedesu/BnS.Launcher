using Newtonsoft.Json;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using System.ComponentModel.Composition;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(GameRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LauncherConfigJsonParser : ILauncherConfigParser
    {
        public LauncherConfig Parse(string text)
        {
            return JsonConvert.DeserializeObject<LauncherConfig>(text);
        }

        public string Stringify(LauncherConfig config)
        {
            return JsonConvert.SerializeObject(config);
        }
    }
}
