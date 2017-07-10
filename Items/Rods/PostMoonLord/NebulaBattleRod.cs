using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.PostMoonLord
{
	public class NebulaBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Battle Rod");
            Tooltip.SetDefault("Releases helpfull pickups when wet.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 22.0f;
            base.item.shoot = base.mod.ProjectileType("NebulaBobber");
            base.item.damage = 666;
            base.item.crit = 20;
            base.item.rare = 9;
            base.item.fishingPole = 75;
            base.item.value = Item.sellPrice(0,5,0,0);

            noOfBobs = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
