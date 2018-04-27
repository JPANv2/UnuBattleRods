using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.PostMoonLord
{
	public class RodContainmentUnit : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rod Containment Unit");
            Tooltip.SetDefault("The Ultimate Battle Rod!\nCreates a large area around the bobber that damages whoever is inside.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 21f;
            base.item.shoot = base.mod.ProjectileType("RodContainmentUnitBobber");
            ItemID.Sets.CanFishInLava[item.type] = true;
            base.item.damage = 800;
            base.item.rare = 11;
            base.item.fishingPole = 100;
            base.item.value = Item.sellPrice(1,0,0,0);
            noOfBobs = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "WoodenBattlerod");
            recipe.AddIngredient(mod, "CactusBattlerod");
            recipe.AddIngredient(mod, "StarMixBattlerod");
            recipe.AddIngredient(mod, "CoolerBattlerod");
            recipe.AddRecipeGroup("UnuBattleRods:EvilRods");
            recipe.AddIngredient(mod, "BeeteoriteBattlerod");
            recipe.AddIngredient(mod, "HardTriadBattlerod");
            recipe.AddIngredient(mod, "TurtleBattlerod");
            recipe.AddIngredient(mod, "BeetleBattlerod");
            recipe.AddIngredient(mod, "LifeforceBattlerod");
            recipe.AddIngredient(mod, "DragonMixBattlerod");
            recipe.AddIngredient(mod, "TerraBattlerod");
            recipe.AddIngredient(mod, "FractaliteBattlerod");
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

    }
}
