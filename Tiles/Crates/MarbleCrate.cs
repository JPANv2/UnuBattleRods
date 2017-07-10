using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class MarbleCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("MarbleCrate");
            name = CreateMapEntryName();
            name.SetDefault("Marble Crate");
            base.SetDefaults();
        }
    }
}
