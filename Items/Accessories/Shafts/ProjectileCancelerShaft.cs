using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Shafts
{
    public class ProjectileCancelerShaft: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anti-Projectile Shaft");
            Tooltip.SetDefault("30% chance to disable an incoming hostile projectile if hooked to an enemy.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FractaliteBar", 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            base.AddRecipes();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).projectileDestroyPercentage += 3000;
        }
    }
}
