using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class SpookyBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Battle Rod");
            Tooltip.SetDefault("Shoots bats.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 18.0f;
            base.item.shoot = base.mod.ProjectileType("SpookyBobber");
            base.item.damage = 150;
            base.item.crit = 25;
            base.item.rare = 7;
            base.item.fishingPole = 55;
            base.item.value = Item.sellPrice(0,3,0,0);

            noOfBobs = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyWood, 250);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
