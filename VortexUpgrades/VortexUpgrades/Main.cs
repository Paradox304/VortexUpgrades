using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public override TranslationList DefaultTranslations => new TranslationList() 
        {
            { "GunNameWrong", "Gun name or id is wrong" },
            { "CantUpgrade", "You can't upgrade this gun, check which guns you can upgrade by doing /checkgun (Gun Name/ID)" },
            { "ItemNotInInventory", "You don't have {0} in your inventory" },
            { "NotEnoughBalance", "You don't have enough balance to do this upgrade" },
            { "Upgrade", "You won and upgraded your gun to {0}" },
            { "Downgrade", "You lost and downgraded your gun to {0}" },
            { "Lost", "You lost your gun" },
            { "UpgradeNotAvailable", "{0} isn't available for any upgrades" },
            { "UpgradeFound", "{0} is available for upgrades \n Cost: {1} \n Upgrade Chance: {2} \n Downgrade Chance: {3}" }
        
        };


        public static Main Instance;
    }
}
