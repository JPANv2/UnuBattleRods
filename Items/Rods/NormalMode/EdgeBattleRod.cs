using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class EdgeBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Edge Battle Rod");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 15.0f;
            base.item.shoot = base.mod.ProjectileType("EdgeBobber");
            
            base.item.damage = 90;
            base.item.crit = 10;
            base.item.rare = 3;
            base.item.fishingPole = 32;
            base.item.value = Item.sellPrice(0,1,50,0);
            noOfBobs = 2;
        }

        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod,"JungleBattlerod");
            recipe.AddIngredient(mod, "DungeonBattlerod");
            recipe.AddIngredient(mod, "CorruptBattlerod");
            recipe.AddIngredient(mod, "HellstoneBattlerod");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "JungleBattlerod");
            recipe.AddIngredient(mod, "DungeonBattlerod");
            recipe.AddIngredient(mod, "CrimsonBattlerod");
            recipe.AddIngredient(mod, "HellstoneBattlerod");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
