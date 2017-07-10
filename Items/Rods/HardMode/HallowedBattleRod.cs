using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class HallowedBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Battle Rod");
            Tooltip.SetDefault("Super effective against Bosses!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 19.0f;
            base.item.shoot = base.mod.ProjectileType("HallowedBobber");
            base.item.damage = 180;
            base.item.crit = 15;
            base.item.rare = 5;
            base.item.fishingPole = 45;
            base.item.value = Item.sellPrice(0,5,0,0);
            noOfBobs = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            
        }
    }
}
