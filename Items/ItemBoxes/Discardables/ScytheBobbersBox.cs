﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnuBattleRods.Items.Discardables;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.ItemBoxes.Discardables
{
    public class ScytheBobbersBox : ItemBox
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
            DisplayName.SetDefault("Scythe Bobbers Box");
            Tooltip.SetDefault("A box containing 6 Scythe Bobbers. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 6;
            boxedItemID = ModContent.ItemType<ScytheBobbers>();
            boxedItemMax = boxedItemMin = 6;
        }

    }
}
