using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class IronBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 10f;
            base.item.shoot = base.mod.ProjectileType("IronBobber");
            base.item.damage = 23;
            base.item.crit = 5;
            base.item.rare = 1;
            base.item.fishingPole = 15;
            base.item.value = Item.sellPrice(0,0,20,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
