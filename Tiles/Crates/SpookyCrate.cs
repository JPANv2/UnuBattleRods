using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class SpookyCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("SpookyCrate");
            name = CreateMapEntryName();
            name.SetDefault("Spooky Crate");
            base.SetDefaults();
        }
    }
}
