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
    public class CurrencyHook: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Currency Hook");
            Tooltip.SetDefault("0.5% chance for hooked enemy to drop some money after a bob. Money types change with game progression.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,8,0,0);
            item.rare = 3;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LuckyCoin, 1);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).moneyPercent += 50;   
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

    }
}
