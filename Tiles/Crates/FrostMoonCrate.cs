using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class FrostMoonCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("FrostMoonCrate");
            name = CreateMapEntryName();
            name.SetDefault("Frost Moon Crate");
            base.SetDefaults();
        }
    }
}
