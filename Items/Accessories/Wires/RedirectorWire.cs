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
    public class RedirectorWire: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Redirector Wire");
            Tooltip.SetDefault("Thorns damage is instead dealt equally to all enemies with your bobber attached.");
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
            recipe.AddIngredient(mod.ItemType<LinkCable>());
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FishPlayer>(mod).linkDamage = true;
            player.GetModPlayer<FishPlayer>(mod).redirectThorns = true;   
        }
    }
}
