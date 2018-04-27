using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRods;

namespace UnuBattleRods.Items.Accessories.Lures
{
    public class ExtraLure : FishingLure
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Extra Lure");
            Tooltip.SetDefault("Allows for casting one extra lines while fishing.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 3;
            lures = 1;
        }
        public override void AddRecipes()
        {
            /*
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
