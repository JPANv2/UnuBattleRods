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
    public class RustyHook: ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusty Hook");
            Tooltip.SetDefault("Increase fishing damage if enemy is under water.");
        }

        public override void SetDefaults()
        {            
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 3;
            item.accessory = true;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 15);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].modProjectile != null && Main.projectile[i].modProjectile is Bobber)
                {
                    Entity stuck = ((Bobber)(Main.projectile[i].modProjectile)).getStuckEntity();
                    if (stuck.wet && !stuck.lavaWet && !stuck.honeyWet)
                    {
                        player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.2f;
                        return;
                    }

                }
            }

        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            int hook = mod.ItemType<HookSet>();
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook) {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook) {
                    return false;
                }
            } 
            return true;
        }

    }
}
