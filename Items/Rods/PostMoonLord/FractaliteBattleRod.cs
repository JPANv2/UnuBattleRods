using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.PostMoonLord
{
	public class FractaliteBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fractalite Battle Rod");
            Tooltip.SetDefault("Creates an area where it infilcts enemies with the Solar Fire debuff.\nReally strong.\nFires homing bullets.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 21f;
            base.item.shoot = base.mod.ProjectileType("FractaliteBobber");
            base.item.damage = 700;
            base.item.rare = 10;
            base.item.fishingPole = 70;
            base.item.value = Item.sellPrice(0,40,0,0);
            noOfBobs = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "SolarBattlerod");
            recipe.AddIngredient(mod, "NebulaBattlerod");
            recipe.AddIngredient(mod, "VortexBattlerod");
            recipe.AddIngredient(mod, "StardustBattlerod");
            recipe.AddIngredient(mod, "FractaliteBar", 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

    }
}
