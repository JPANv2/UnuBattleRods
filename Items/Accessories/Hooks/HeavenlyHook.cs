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
    public class HeavenlyHook: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Hook");
            Tooltip.SetDefault("Increase fishing damage based on player altitude.");
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
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            if (player.ZoneUnderworldHeight || player.ZoneSkyHeight) {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.3f;
                return;
            }
            if (player.ZoneRockLayerHeight)
            {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.2f;
                return;
            }
            if (player.ZoneDirtLayerHeight)
            {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.1f;
                return;
            }
               
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
