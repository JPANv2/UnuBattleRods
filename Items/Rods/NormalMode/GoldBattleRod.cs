using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class GoldBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Battle Rod");
            Tooltip.SetDefault("Not a Golden Fishing Rod...\nI can hear the money raining down!");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 12.5f;
            base.item.shoot = base.mod.ProjectileType("GoldBobber");
            base.item.damage = 35;
            base.item.crit = 5;
            base.item.rare = 2;
            base.item.fishingPole = 20;
            base.item.value = Item.sellPrice(0,0,75,0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).moneyPercent += 100;
        }
    }
}
