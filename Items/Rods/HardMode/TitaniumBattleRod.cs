using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class TitaniumBattlerod : BattleRod
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Battle Rod");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 17.0f;
            base.item.shoot = base.mod.ProjectileType("TitaniumBobber");
            
            base.item.damage = 130;
            base.item.crit = 15;
            base.item.rare = 4;
            base.item.fishingPole = 42;
            base.item.value = Item.sellPrice(0,3,20,0);
            noOfBobs = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
