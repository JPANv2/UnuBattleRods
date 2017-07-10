using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class CrimsonBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Battle Rod");
            Tooltip.SetDefault("Very slight life steal.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 12.0f;
            base.item.shoot = base.mod.ProjectileType("CrimsonBobber");           
            base.item.damage = 42;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 27;
            base.item.value = Item.sellPrice(0,0,90,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 2);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
