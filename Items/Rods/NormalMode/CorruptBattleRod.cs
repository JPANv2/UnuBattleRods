using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class CorruptBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Battle Rod");
            Tooltip.SetDefault("Very slight mana syphon.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults(); 
            base.item.shootSpeed = 12.0f;
            base.item.shoot = base.mod.ProjectileType("CorruptBobber");
            base.item.damage = 40;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 22;
            base.item.value = Item.sellPrice(0,0,90,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 2);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
