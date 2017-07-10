using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class LuminiteCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("LuminiteCrate");
            name = CreateMapEntryName();
            name.SetDefault("Luminite Crate");
            base.SetDefaults();
        }
    }
}
