using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class ShroomiteCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("ShroomiteCrate");
            name = CreateMapEntryName();
            name.SetDefault("Shroomite Crate");
            base.SetDefaults();
        }
    }
}
