using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.ItemBoxes
{
    public class MasterBaitBox : ItemBox
    {
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master Bait Box");
            Tooltip.SetDefault("A box containing 6 Master Bait. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 6;
            boxedItemID = ItemID.MasterBait;
            boxedItemMax = boxedItemMin = 6;
        }
    }
}
