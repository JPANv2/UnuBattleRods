using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class MeteorBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Battle Rod");
            Tooltip.SetDefault("Sets enemies on fire!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 14.0f;
            base.item.shoot = base.mod.ProjectileType("MeteorBobber");
            base.item.damage = 50;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 27;
            base.item.value = Item.sellPrice(0,1,20,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
