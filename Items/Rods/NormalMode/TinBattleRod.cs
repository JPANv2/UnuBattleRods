using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class TinBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 9.5f;
            base.item.shoot = base.mod.ProjectileType("TinBobber");
            base.item.damage = 21;
            base.item.crit = 5;
            base.item.rare = 1;
            base.item.fishingPole = 12;
            base.item.value = Item.sellPrice(0,0,1,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TinBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
