using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnuBattleRods.Items.Discardables;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.ItemBoxes.Discardables
{
    public class ShadowBobbersBox : ItemBox
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
            DisplayName.SetDefault("Shadow Blade Bobbers Box");
            Tooltip.SetDefault("A box containing 6 Shadow Blade Bobbers. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 6;
            boxedItemID = ModContent.ItemType<ShadowBobbers>();
            boxedItemMax = boxedItemMin = 6;
        }

    }
}
