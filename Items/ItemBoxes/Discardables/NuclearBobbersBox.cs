using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnuBattleRods.Items.Discardables;
using Terraria.ModLoader;
namespace UnuBattleRods.Items.ItemBoxes.Discardables
{
    public class NuclearBobbersBox : ItemBox
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
            DisplayName.SetDefault("Nuclear Bobbers Box");
            Tooltip.SetDefault("A box containing 6 Nuclear Bobbers. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 6;
            boxedItemID = ModContent.ItemType<NuclearBobbers>();
            boxedItemMax = boxedItemMin = 6;
        }

    }
}
