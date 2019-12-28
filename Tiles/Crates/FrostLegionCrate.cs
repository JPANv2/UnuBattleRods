using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class FrostLegionCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("FrostLegionCrate");
            name = CreateMapEntryName();
            name.SetDefault("Frost Legion Crate");
            base.SetDefaults();
        }
    }
}
