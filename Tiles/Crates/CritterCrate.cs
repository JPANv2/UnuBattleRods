using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class CritterCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("CritterCrate");
            name = CreateMapEntryName();
            name.SetDefault("Critter Crate");
            base.SetDefaults();
        }
    }
}
