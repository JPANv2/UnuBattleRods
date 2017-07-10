using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Wires
{
    public class TitanWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Wire");
            Tooltip.SetDefault("Can always reel in the enemy, no matter the size (except if immobile). Fishing line never breaks.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,2,00,0);
            item.rare = 5;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 15);
            recipe.AddIngredient(ItemID.HighTestFishingLine, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FishPlayer>(mod).sizeMultiplierMultiplier = 9999f;
            player.accFishingLine = true;
        }
    }
}
