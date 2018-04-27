using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class BetsyBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Betsy's Battle Rod");
            Tooltip.SetDefault("Inflicts Betsy's Curse debuff(on npcs).\nAllows 2 different powered baits at once.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("BetsyBobber");
            base.item.damage = 320;
            base.item.crit = 20;
            base.item.rare = 8;
            base.item.fishingPole = 70;
            base.item.value = Item.sellPrice(0,10,0,0);

            noOfBobs = 3;
            noOfBaits = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 16);
            recipe.AddIngredient(mod ,"BetsyScales", 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
