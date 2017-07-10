using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class ShroomiteBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Battle Rod");
            Tooltip.SetDefault("Spreads mushrooms when idle. Suprisingly fast.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 18.0f;
            base.item.shoot = base.mod.ProjectileType("ShroomiteBobber");
            base.item.damage = 120;
            base.item.crit = 20;
            base.item.rare = 8;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,6,0,0);

            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
