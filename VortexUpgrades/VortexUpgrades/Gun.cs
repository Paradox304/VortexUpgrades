using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades
{
    public class Gun
    {
        public ushort ID;
        public List<ushort> Upgrades;
        public List<ushort> Downgrades;
        public int UpgradeChance;
        public int DowngradeChance;
        public decimal Cost;

        public Gun()
        {

        }

        public Gun(ushort id, List<ushort> Upgrade, List<ushort> Downgrade, int upgradeChance, int downgradeChance, int amount)
        {
            ID = id;
            Upgrades = Upgrade;
            UpgradeChance = upgradeChance;
            DowngradeChance = downgradeChance;
            Cost = amount;
            Downgrades = Downgrade;
        }
    }
}
