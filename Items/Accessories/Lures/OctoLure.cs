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
    public class OctoLure : FishingLure
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Octo-Lure");
            Tooltip.SetDefault("Allows for casting eight extra lines while fishing.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            lures = 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.AddIngredient(mod, "QuadLure", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
