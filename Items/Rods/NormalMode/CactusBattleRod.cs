using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class CactusBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Battle Rod");
            Tooltip.SetDefault("Prickly!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 9f;
            base.item.shoot = base.mod.ProjectileType("CactusBobber");
            base.item.damage = 16;
            base.item.rare = 1;
            base.item.fishingPole = 5;
            base.item.value = Item.buyPrice(0,0,0,50);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
