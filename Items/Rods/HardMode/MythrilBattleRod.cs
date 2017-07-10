using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class MythrilBattlerod : BattleRod
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 15.0f;
            base.item.shoot = base.mod.ProjectileType("MythrilBobber");
            
            base.item.damage = 110;
            base.item.crit = 15;
            base.item.rare = 4;
            base.item.fishingPole = 35;
            base.item.value = Item.sellPrice(0,2,50,0);

            noOfBobs = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
