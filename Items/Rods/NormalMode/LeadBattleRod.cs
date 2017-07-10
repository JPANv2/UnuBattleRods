using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class LeadBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lead Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 10f;
            base.item.shoot = base.mod.ProjectileType("LeadBobber");
            base.item.damage = 26;
            base.item.crit = 5;
            base.item.rare = 1;
            base.item.fishingPole = 16;
            base.item.value = Item.sellPrice(0,0,20,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
