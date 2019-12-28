using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.ItemBoxes
{
    public class JourneymanBaitBox : ItemBox
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
            DisplayName.SetDefault("Journeyman Bait Box");
            Tooltip.SetDefault("A box containing 6 Journeyman Bait. Right-click it to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 6;
            boxedItemID = ItemID.JourneymanBait;
            boxedItemMax = boxedItemMin = 6;
        }
    }
}
