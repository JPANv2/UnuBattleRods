using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class FrostBattlerod : BattleRod
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Battle Rod");
            Tooltip.SetDefault("Imbued with the power of Frost Fire.");
        }


        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 17.5f;
            base.item.shoot = base.mod.ProjectileType("FrostBobber");
            base.item.damage = 150;
            base.item.crit = 15;
            base.item.rare = 5;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            
        }
    }
}
