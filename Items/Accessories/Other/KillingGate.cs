using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Other
{
    public class KillingGate: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Killing Gate");
            Tooltip.SetDefault("Converts caught items with a Max Stack of 1 into coins.\nTurns all caught fish into Fish Steaks (if they can be converted).");
        }

        public override void SetDefaults()
        {             
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);            
            recipe.AddIngredient(mod,"FishSlicer", 1);
            recipe.AddIngredient(mod, "SellGate", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().fishSlicer = true;
            player.GetModPlayer<FishPlayer>().sellGate = true;
        }
    }
}
