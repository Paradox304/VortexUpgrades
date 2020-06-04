using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            Instance = this;

            Logger.Log("VortexUpgrades plugin has been loaded.");
            Logger.Log("Version: 1.0");
            Logger.Log("Made by Paradox.");
        }

        protected override void Unload()
        {
            Logger.Log("VortexUpgrades plugin has been unloaded.");
        }

        public static Main Instance;
    }
}
