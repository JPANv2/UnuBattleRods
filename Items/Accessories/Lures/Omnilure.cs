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
    public class Omnilure : FishingLure
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Omnilure");
            Tooltip.SetDefault("Forces only one lure, but with 15% more damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 4;
            lures = -99999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 15);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.15f;
        }
    }
}
