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
    public class BoxOfCountlessLures : FishingLure
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Box of Countless Lures");
            Tooltip.SetDefault("Allows for casting thirty-two extra lines while fishing.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 8;
            lures = 32;
        }
        public override void AddRecipes()
        {
            /*
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(mod, "BoxOfLures", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
