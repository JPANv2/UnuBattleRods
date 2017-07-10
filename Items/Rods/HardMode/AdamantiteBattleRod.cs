using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class AdamantiteBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 17.0f;
            base.item.shoot = base.mod.ProjectileType("AdamantiteBobber");
            
            base.item.damage = 120;
            base.item.crit = 15;
            base.item.rare = 4;
            base.item.fishingPole = 40;
            base.item.value = Item.sellPrice(0,3,0,0);

            noOfBobs = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
