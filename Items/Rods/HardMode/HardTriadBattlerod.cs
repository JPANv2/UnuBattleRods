using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class HardTriadBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hard Triad Battle Rod");
            Tooltip.SetDefault("Good against Bosses, but best against regular en enemies.\nGenerates tornadoes when wet and inflicts Frost Fire.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 19.0f;
            base.item.shoot = base.mod.ProjectileType("HardTriadBobber");
            base.item.damage = 200;
            base.item.crit = 15;
            base.item.rare = 7;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FrostBattlerod", 1);
            recipe.AddIngredient(mod, "ForbiddenBattlerod", 1);
            recipe.AddIngredient(mod, "HallowedBattlerod", 1);
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddIngredient(mod, "EnergyAmalgamate", 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            
        }
    }
}
