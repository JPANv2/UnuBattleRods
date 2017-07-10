using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class ForbiddenBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Battle Rod");
            Tooltip.SetDefault("Watch out for sandnados!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 17.5f;
            base.item.shoot = base.mod.ProjectileType("ForbiddenBobber");
            base.item.damage = 165;
            base.item.crit = 15;
            base.item.rare = 5;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            
        }
    }
}
