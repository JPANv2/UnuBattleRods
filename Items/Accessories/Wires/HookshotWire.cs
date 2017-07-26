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
    public class HookshotWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hookshot Wire");
            Tooltip.SetDefault("Always reeled in towards the enemy.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,0,20,0);
            item.rare = 1;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
           
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).sizeMultiplierMultiplier = 0f;
            
        }
    }
}
