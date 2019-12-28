using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class OldOnesCrate : CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("OldOnesCrate");
            name = CreateMapEntryName();
            name.SetDefault("Old Ones Crate");
            base.SetDefaults();
        }
    }
}
