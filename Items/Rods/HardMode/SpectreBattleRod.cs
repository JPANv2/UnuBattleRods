using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class SpectreBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre Battle Rod");
            Tooltip.SetDefault("Spreads magnet spheres when idle. Slight life steal.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 18.0f;
            base.item.shoot = base.mod.ProjectileType("SpectreBobber");
            base.item.damage = 240;
            base.item.crit = 20;
            base.item.rare = 8;
            base.item.fishingPole = 44;
            base.item.value = Item.sellPrice(0,6,0,0);

            noOfBobs = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
