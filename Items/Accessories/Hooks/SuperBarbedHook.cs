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
    public class SuperBarbedHook: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Barbed Hook");
            Tooltip.SetDefault("Increase fishing crit by 20%");
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
            recipe.AddIngredient(ItemID.WoodenSpike, 25);
            recipe.AddIngredient(mod, "BarbedHook");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().bobberCrit += 20;   
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            int[] hooks = { ModContent.ItemType<HookSet>(), ModContent.ItemType<BarbedHook>()};
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hooks[0])
                {
                    return false;
                }
                if (player.armor[i].type == hooks[1])
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hooks[0])
                {
                    return false;
                }
                if (player.armor[i].type == hooks[1])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
