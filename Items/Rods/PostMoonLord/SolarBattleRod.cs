using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.PostMoonLord
{
	public class SolarBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Battle Rod");
            Tooltip.SetDefault("Creates an area where it infilcts enemies with the Solar Fire debuff.\nReally strong.\nCan fish in lava.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 22.0f;
            base.item.shoot = base.mod.ProjectileType("SolarBobber");
            ItemID.Sets.CanFishInLava[item.type] = true;
            base.item.damage = 700;
            base.item.crit = 20;
            base.item.rare = 9;
            base.item.fishingPole = 65;
            base.item.value = Item.sellPrice(0,5,0,0);

            noOfBobs = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
