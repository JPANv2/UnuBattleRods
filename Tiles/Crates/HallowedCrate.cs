using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class HallowedCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("HallowedCrate");
            name = CreateMapEntryName();
            name.SetDefault("Hallowed Crate");
            base.SetDefaults();
        }
    }
}
