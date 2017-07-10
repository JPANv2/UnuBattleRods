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
    public class BoxOfLures : FishingLure
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Box of Lures");
            Tooltip.SetDefault("Allows for casting sixteen extra lines while fishing.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 5;
            lures = 16;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Bars", 5);
            recipe.AddIngredient(mod, "OctoLure", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<FishPlayer>(mod).multilineFishing += 16;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

    }
}
