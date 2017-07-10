using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class TungstenBattlerod : BattleRod
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 11f;
            base.item.shoot = base.mod.ProjectileType("TungstenBobber");
            base.item.damage = 30;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 19;
            base.item.value = Item.sellPrice(0,0,50,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
