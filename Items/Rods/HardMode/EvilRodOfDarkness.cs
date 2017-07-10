using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class EvilRodOfDarkness : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evil Rod of Darkness");
            Tooltip.SetDefault("Provides slight mana shyphon.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 17.5f;
            base.item.shoot = base.mod.ProjectileType("EvilRodOfDarknessBobber");

            base.item.damage = 160;
            base.item.crit = 10;
            base.item.rare = 5;
            base.item.fishingPole = 42;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Bars", 12);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Bars", 12);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 12);
            recipe.AddIngredient(ItemID.ShadowScale, 50);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Rods", 1);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Rods", 1);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Rods", 1);
            recipe.AddIngredient(ItemID.ShadowScale, 50);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
