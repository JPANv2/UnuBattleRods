using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class LifeforceBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lifeforce Battle Rod");
            Tooltip.SetDefault("Drains lifeforce from your opponent. Suprisingly fast.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 22.0f;
            base.item.shoot = base.mod.ProjectileType("LifeforceBobber");
            base.item.damage = 210;
            base.item.crit = 20;
            base.item.rare = 9;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,6,0,0);

            noOfBobs = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ChlorophyteBattlerod");
            recipe.AddIngredient(mod, "ShroomiteBattlerod");
            recipe.AddIngredient(mod, "SpectreBattlerod");
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
