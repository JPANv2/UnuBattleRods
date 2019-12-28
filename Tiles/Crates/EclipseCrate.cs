using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class EclipseCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("EclipseCrate");
            name = CreateMapEntryName();
            name.SetDefault("Eclipse Crate");
            base.SetDefaults();
        }
    }
}
