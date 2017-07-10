using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class JungleBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Battle Rod");
            Tooltip.SetDefault("Poison enemies.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 12.0f;
            base.item.shoot = base.mod.ProjectileType("JungleBobber");
            base.item.damage = 48;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 27;
            base.item.value = Item.sellPrice(0,1,0,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 15);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddIngredient(ItemID.Vine, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
