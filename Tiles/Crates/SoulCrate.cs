using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class SoulCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("SoulCrate");
            name = CreateMapEntryName();
            name.SetDefault("Soul Crate");
            base.SetDefaults();
        }
    }
}
