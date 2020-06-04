using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades
{
    public static class GetItem
    {
        public static ItemAsset GetItemAsset(string itemNameOrId)
        {
            var assets = Assets.find(EAssetType.ITEM).Cast<ItemAsset>().Where(k => k?.itemName != null && k.name != null).OrderBy(k => k.itemName.Length).ToList();
            var itemAsset = assets.FirstOrDefault(k =>
                    itemNameOrId.Equals(k.id.ToString(), StringComparison.OrdinalIgnoreCase) ||
                    itemNameOrId.Split(' ').All(l => k.itemName.ToLower().Contains(l)) ||
                    itemNameOrId.Split(' ').All(l => k.name.ToLower().Contains(l)));

            return itemAsset;
        }
    }
}
