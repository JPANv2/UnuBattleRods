using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class SnowstormCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("SnowstormCrate");
            name = CreateMapEntryName();
            name.SetDefault("Snowstorm Crate");
            base.SetDefaults();
        }
    }
}
