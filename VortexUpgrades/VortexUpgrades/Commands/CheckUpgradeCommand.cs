using Rocket.API;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades.Commands
{
    public class CheckUpgradeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "checkupgrade";
        public string Help => "Checks if the gun can be upgraded";
        public string Syntax => "/checkupgrade (Gun Name/Id)";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>();
        public void Execute(IRocketPlayer rocketPlayer, string[] command)
        {
            if (command.Length > 1 || command.Length == 0)
            {
                UnturnedChat.Say(rocketPlayer, $"Correct Usage: {Syntax}");
                return;
            }

            String gunName = command[0];
            ItemAsset item = GetItem.GetItemAsset(gunName);
            if (item == null)
            {
                UnturnedChat.Say(rocketPlayer, "Gun name or id is wrong");
                return;
            }

            Gun g = null;
            bool foundConfig = false;
            foreach (Gun gun in Main.Instance.Configuration.Instance.Guns.ToList())
            {
                if (gun.ID == item.id)
                {
                    foundConfig = true;
                    g = gun;
                    break;
                }
            }
            if (!foundConfig)
            {
                UnturnedChat.Say(rocketPlayer, "This gun isn't available for any upgrades.");
                return;
            }

            if (foundConfig)
            {
                UnturnedChat.Say($"{item.name} is available for upgrade.");
                UnturnedChat.Say($"Cost: {g.Cost}");
                UnturnedChat.Say($"Upgrade Chance: {g.UpgradeChance}%");
                UnturnedChat.Say($"Downgrade Chance: {g.DowngradeChance}%");
            }
        }
    }
}
