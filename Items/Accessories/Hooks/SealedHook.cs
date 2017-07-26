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
    public class SealedHook: ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sealed Hook");
            Tooltip.SetDefault("Seals bobber-stuck enemies damage by 20%");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 5;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "StarMix",3);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).seals = true;
               
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            int hook = mod.ItemType<HookSet>();
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
