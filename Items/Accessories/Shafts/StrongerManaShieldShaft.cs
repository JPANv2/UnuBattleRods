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
    public class StrongerManaShieldShaft: ManaShieldShaft
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stronger Mana Shield Shaft");
            Tooltip.SetDefault("Up to 30% of all damage received is deducted from your Mana instead.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ManaShieldShaft", 1);
            recipe.AddIngredient(mod, "EnergyAmalgamate", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            base.AddRecipes();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).manaShield = true;
            player.GetModPlayer<FishPlayer>(mod).manaShieldPercentage += 0.3f;
        }
    }
}
