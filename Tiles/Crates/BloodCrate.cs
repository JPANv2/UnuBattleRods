using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class BloodCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("BloodCrate");
            name = CreateMapEntryName();
            name.SetDefault("Blood Crate");
            base.SetDefaults();
        }
    }
}
