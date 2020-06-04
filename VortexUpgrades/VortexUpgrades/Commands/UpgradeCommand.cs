using fr34kyn01535.Uconomy;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades.Commands
{
    public class UpgradeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "upgrade";
        public string Help => "Upgrades gun";
        public string Syntax => "/upgrade (Gun Name/Id)";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>();
        public void Execute(IRocketPlayer rocketPlayer, string[] command)
        {
            UnturnedPlayer player = rocketPlayer as UnturnedPlayer;

            // Check if Syntax is correct
            if (command.Length > 1 || command.Length == 0)
            {
                UnturnedChat.Say(rocketPlayer, $"Correct Usage: {Syntax}");
                return;
            }

            // Find which item player has inputted
            String gunName = command[0];
            ItemAsset item = GetItem.GetItemAsset(gunName);
            if (item == null)
            {
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("GunNameWrong"));
                return;
            }

            // Find if the gun is available to upgrade
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
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("CantUpgrade"));
                return;
            }

            // Find if the player has that item in their inventory
            bool foundItem = false;
            byte invPages = (byte)(PlayerInventory.PAGES - 1);
            byte page = 0;
            byte index = 0;
            for (byte b = 0; b < invPages; b = (byte)(b + 1))
            {
                byte itemCount = player.Player.inventory.getItemCount(b);
                for (byte b2 = 0; b2 < itemCount; b2 = (byte)(b2 + 1))
                {
                    if (player.Player.inventory.getItem(b, b2).item.id == item.id)
                    {
                        foundItem = true;
                        page = b;
                        index = b2;
                        break;
                    }
                }
                if (foundItem)
                    break;
            }
            if (!foundItem)
            {
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("ItemNotInInventory", item.name));
                return;
            }

            // Check if player has correct balance
            decimal playerBalance = Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString());
            if (playerBalance < g.Cost)
            {
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("NotEnoughBalance"));
                return;
            }

            // If player won
            if (g.UpgradeChance >= UnityEngine.Random.Range(1, 101))
            {
                ItemAsset upgradeItem = GetItem.GetItemAsset(g.Upgrades[UnityEngine.Random.Range(0, g.Upgrades.Count)].ToString());
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("Upgrade", upgradeItem.name));
                Uconomy.Instance.Database.IncreaseBalance(player.CSteamID.ToString(), -g.Cost);
                player.Player.inventory.removeItem(page, index);
                player.Player.inventory.forceAddItem(new Item(upgradeItem.id, true), true);
                return;
            }
            else if (g.DowngradeChance >= UnityEngine.Random.Range(1, 101)) // If player lost
            {
                ItemAsset downgradeItem = GetItem.GetItemAsset(g.Downgrades[UnityEngine.Random.Range(0, g.Downgrades.Count)].ToString());
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("Downgrade", downgradeItem.name));
                Uconomy.Instance.Database.IncreaseBalance(player.CSteamID.ToString(), -g.Cost);
                player.Player.inventory.removeItem(page, index);
                player.Player.inventory.forceAddItem(new Item(downgradeItem.id, true), true);
                return;
            }
            else // Player lost gun
            {
                UnturnedChat.Say(rocketPlayer, Main.Instance.Translate("Lost"));
                Uconomy.Instance.Database.IncreaseBalance(player.CSteamID.ToString(), -g.Cost);
                player.Player.inventory.removeItem(page, index);
                return;
            }
        }

    }
}
