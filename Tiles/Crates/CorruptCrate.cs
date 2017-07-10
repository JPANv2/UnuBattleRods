using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class CorruptCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("CorruptCrate");
            name = CreateMapEntryName();
            name.SetDefault("Corrupt Crate");
            base.SetDefaults();
        }
    }
}
