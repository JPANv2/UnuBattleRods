using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRods.Tiles.Crates
{
    class MeteorCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("MeteorCrate");
            name = CreateMapEntryName();
            name.SetDefault("Meteor Crate");
            base.SetDefaults();
        }
    }
}
