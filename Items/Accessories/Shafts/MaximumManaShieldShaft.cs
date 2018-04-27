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
    public class MaximumManaShieldShaft: ManaShieldShaft
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maximum Mana Shield Shaft");
            Tooltip.SetDefault("Up to 50% of all damage received is deducted from your Mana instead.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "StrongerManaShieldShaft", 1);
            recipe.AddIngredient(mod, "FractaliteBar", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).manaShield = true;
            player.GetModPlayer<FishPlayer>(mod).manaShieldPercentage += 0.5f;
        }
    }
}
