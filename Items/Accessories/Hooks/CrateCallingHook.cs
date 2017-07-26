using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Accessories.Hooks
{
    public class CrateCallingHook: ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crate Calling Hook");
            Tooltip.SetDefault("5% chance for the enemy dropping a crate on kill. Crate type depends on the rod used.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 3;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenCrate, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).cratePercent += 500;   
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

    }
}
