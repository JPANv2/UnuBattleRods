using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class DungeonBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Battle Rod");
            Tooltip.SetDefault("I feel like I'm being wached...");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 14.0f;
            base.item.shoot = base.mod.ProjectileType("DungeonBobber");
            
            base.item.damage = 68;
            base.item.crit = 10;
            base.item.rare = 2;
            base.item.fishingPole = 27;
            base.item.value = Item.sellPrice(0,0,90,0);
        }

        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 2);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }*/
    }
}
