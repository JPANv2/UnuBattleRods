using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class SandstormCrate : CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("SandstormCrate");
            name = CreateMapEntryName();
            name.SetDefault("Sandstorm Crate");
            base.SetDefaults();
        }
    }
}
