using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class TerraCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("TerraCrate");
            name = CreateMapEntryName();
            name.SetDefault("Terra Crate");
            base.SetDefaults();
        }
    }
}
