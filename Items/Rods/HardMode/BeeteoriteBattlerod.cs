using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class BeeteoriteBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beeteorite Battle Rod");
            Tooltip.SetDefault("Spawns fire bees that inflict On Fire and any other debuffs your baits may have.\nBait time decreased each time bees get spawned.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 19.0f;
            base.item.shoot = base.mod.ProjectileType("BeeteoriteBobber");
            base.item.damage = 180;
            base.item.crit = 15;
            base.item.rare = 7;
            base.item.fishingPole = 50;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 3;
            noOfBaits = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BeeBattlerod", 1);
            recipe.AddIngredient(mod, "MeteorBattlerod", 1);
            recipe.AddIngredient(mod, "LesserEnergyAmalgamate", 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            
        }
    }
}
