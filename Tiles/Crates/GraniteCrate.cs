using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class GraniteCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("GraniteCrate");
            name = CreateMapEntryName();
            name.SetDefault("Granite Crate");
            base.SetDefaults();
        }
    }
}
