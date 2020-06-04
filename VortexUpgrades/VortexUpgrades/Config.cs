using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VortexUpgrades
{
    public class Config : IRocketPluginConfiguration
    {
        public List<Gun> Guns;

        public void LoadDefaults()
        {
            Guns = new List<Gun> { new Gun(363, new List<ushort> { 116, 1024, 297 }, new List<ushort> { 97, 107, 1021 }, 50, 30, 100) };
        }
    }
}
