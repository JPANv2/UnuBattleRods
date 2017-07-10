using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class ObsidianCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("ObsidianCrate");
            name = CreateMapEntryName();
            name.SetDefault("Obsidian Crate");
            base.SetDefaults();
        }
    }
}
