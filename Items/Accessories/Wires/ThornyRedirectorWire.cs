using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Accessories.Wires
{
    public class ThornyRedirectorWire: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorny Redirector Wire");
            Tooltip.SetDefault("Thorns damage is also dealt equally to all enemies with your bobber attached.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 1;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<RedirectorWire>());
            recipe.AddIngredient(ItemID.ThornsPotion, 5);
            recipe.AddIngredient(ItemID.Cactus, 25);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().linkDamage = true;
            player.GetModPlayer<FishPlayer>().redirectThorns = false;   
        }
    }
}
