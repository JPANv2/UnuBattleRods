using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class TerraBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Battle Rod");
            Tooltip.SetDefault("The Legendary Rod. Shoots blades when idle.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("TerraBobber");
            base.item.damage = 450;
            base.item.crit = 20;
            base.item.rare = 8;
            base.item.fishingPole = 60;
            base.item.value = Item.sellPrice(0,10,0,0);

            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod,"EdgeBattlerod");
            recipe.AddIngredient(mod, "HallowedBattlerod");
            recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
