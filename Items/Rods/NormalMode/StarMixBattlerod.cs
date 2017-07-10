using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Materials;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class StarMixBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Mix Battle Rod");
            Tooltip.SetDefault("Doubles the damage against bosses.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 12.0f;
            base.item.shoot = base.mod.ProjectileType("StarMixBobber");
            base.item.damage = 42;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 24;
            base.item.value = Item.sellPrice(0,0,90,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Bars", 10);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Bars",10);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 10);
            recipe.AddIngredient(mod.ItemType<StarMix>(), 8);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Rods");
            recipe.AddRecipeGroup("UnuBattleRods:Tier1Rods");
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Rods");
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Rods");
            recipe.AddIngredient(mod.ItemType<StarMix>(), 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}

