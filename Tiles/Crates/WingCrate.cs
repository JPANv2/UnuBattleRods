using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class WingCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("WingCrate");
            name = CreateMapEntryName();
            name.SetDefault("Wing Crate");
            base.SetDefaults();
        }
    }
}
