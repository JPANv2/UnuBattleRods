using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class SlimeCrate : CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("SlimeCrate");
            name = CreateMapEntryName();
            name.SetDefault("Slime Crate");
            base.SetDefaults();
        }
    }
}
