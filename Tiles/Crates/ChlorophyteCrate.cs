using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class ChlorophyteCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("ChlorophyteCrate");
            name = CreateMapEntryName();
            name.SetDefault("Chlorophyte Crate");
            base.SetDefaults();
        }
    }
}
