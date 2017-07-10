using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class HellstoneBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellstone Battle Rod");
            Tooltip.SetDefault("Sets enemies on fire!\nCan fish in lava.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 13.5f;
            base.item.shoot = base.mod.ProjectileType("HellstoneBobber");
            ItemID.Sets.CanFishInLava[item.type] = true;
            base.item.damage = 65;
            base.item.crit = 10;
            base.item.rare = 2;
            base.item.fishingPole = 30;
            base.item.value = Item.sellPrice(0,1,20,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
