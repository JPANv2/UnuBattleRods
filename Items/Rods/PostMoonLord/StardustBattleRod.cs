using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.PostMoonLord
{
	public class StardustBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Battle Rod");
            Tooltip.SetDefault("Creates two Stardust Cell minions on swing.\nReally good at fishing!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 21f;
            base.item.shoot = base.mod.ProjectileType("StardustBobber");
            base.item.damage = 500;
            base.item.rare = 10;
            base.item.fishingPole = 70;
            base.item.value = Item.sellPrice(0,40,0,0);
            noOfBobs = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentStardust, 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<FishPlayer>().stardustCells < 2)
            {
                int p = Projectile.NewProjectile(position + new Vector2(16, 16), new Vector2(speedX, speedY), ProjectileID.StardustCellMinion, damage, knockBack, player.whoAmI);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].minionSlots = 0;
                    p = Projectile.NewProjectile(position - new Vector2(16, 16), new Vector2(speedX, speedY), ProjectileID.StardustCellMinion, damage, knockBack, player.whoAmI);
                    if (p >= 0 && p < Main.projectile.Length)
                    {
                        Main.projectile[p].minionSlots = 0;
                    }
                    player.AddBuff(BuffID.StardustMinion, 120);
                    player.GetModPlayer<FishPlayer>().stardustCells += 2;
                }
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
