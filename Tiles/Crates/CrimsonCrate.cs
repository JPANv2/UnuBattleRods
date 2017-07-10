using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class CrimsonCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("CrimsonCrate");
            name = CreateMapEntryName();
            name.SetDefault("Crimson Crate");
            base.SetDefaults();
        }
    }
}
