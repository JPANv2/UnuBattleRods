using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class GoblinCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("GoblinCrate");
            name = CreateMapEntryName();
            name.SetDefault("Goblin Crate");
            base.SetDefaults();
        }
    }
}
