using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class CobaltBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Battle Rod");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 14.50f;
            base.item.shoot = base.mod.ProjectileType("CobaltBobber");
            
            base.item.damage = 95;
            base.item.crit = 15;
            base.item.rare = 4;
            base.item.fishingPole = 30;
            base.item.value = Item.sellPrice(0,2,0,0);

            noOfBobs = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
