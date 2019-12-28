using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class TreasureCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("TreasureCrate");
            name = CreateMapEntryName();
            name.SetDefault("Treasure Crate");
            base.SetDefaults();
        }
    }
}
