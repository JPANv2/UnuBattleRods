using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class DragonMixBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Mix Battle Rod");
            Tooltip.SetDefault("The Power of Dragons!\nInflicts Betsy's Curse debuff(on npcs).\nReleases bubbles.\nAllows 2 different powered baits at once.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("DragonMixBobber");
            base.item.damage = 300;
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
            recipe.AddIngredient(mod, "FishronBattlerod");
            recipe.AddIngredient(mod ,"BetsyBattlerod");
            recipe.AddIngredient(mod, "EnergyAmalgamate", 2);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
