using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace UnuBattleRods.Items.Accessories.Lures
{
    public class DoubleSelectiveBobbers: SelectiveLure
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
            base.SetStaticDefaults();
            DisplayName.SetDefault("Double Selective Bobbers");
            Tooltip.SetDefault("Only two bobbers will hook to the same enemy.");
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           item.value = Item.sellPrice(0, 0, 25, 0);
           item.rare = 3;
           maxHooking = 2;
        }
        public override void AddRecipes()
        {

        }
    }
}
