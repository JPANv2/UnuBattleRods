using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class BeetleBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Battle Rod");
            Tooltip.SetDefault("Releases beetles.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("BeetleBobber");
            base.item.damage = 190;
            base.item.crit = 15;
            base.item.rare = 8;
            base.item.fishingPole = 50;
            base.item.value = Item.sellPrice(0,5,0,0);

            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.BeetleHusk, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
