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
    public class SelectiveBobbers: SelectiveLure
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
            DisplayName.SetDefault("Selective Bobbers");
            Tooltip.SetDefault("Only one bobber will hook to the same enemy.");
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           item.value = Item.sellPrice(0, 0, 5, 0);
           item.rare = 2;
           maxHooking = 1;
        }
        public override void AddRecipes()
        {

        }
    }
}
