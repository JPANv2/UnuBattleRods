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
    public class HookSet: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hook Set");
            Tooltip.SetDefault("Increase fishing damage based on player altitude\nIncrease fishing damage if enemy is under water\n" +
                "Seals bobber-stuck enemies damage by 20%\n" + "Increase fishing crit by 20%");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,5,0,0);
            item.rare = 10;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "SuperBarbedHook");
            recipe.AddIngredient(mod, "SealedHook");
            recipe.AddIngredient(mod, "RustyHook");
            recipe.AddIngredient(mod, "HeavenlyHook");
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneUnderworldHeight || player.ZoneSkyHeight) {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.3f;                
            }else if (player.ZoneRockLayerHeight)
            {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.2f;
            }else if (player.ZoneDirtLayerHeight)
            {
                player.GetModPlayer<FishPlayer>(mod).bobberDamage += 0.1f;
            }

            underwaterBoost(player);
            
            player.GetModPlayer<FishPlayer>(mod).seals = true;

            player.GetModPlayer<FishPlayer>(mod).bobberCrit += 20;
        }

        private void underwaterBoost(Player player)
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

            int[] hooks = new int[] { mod.ItemType<RustyHook>(), mod.ItemType<BarbedHook>(), mod.ItemType<SuperBarbedHook>(),
            mod.ItemType<HeavenlyHook>(), mod.ItemType<SealedHook>(), this.item.type};
            for(int i = 3; i< 8 + player.extraAccessorySlots; i++)
            {
                for(int j = 0; j<hooks.Length; j++)
                {
                    if (player.armor[i].type == hooks[j])
                        return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                for (int j = 0; j < hooks.Length; j++)
                {
                    if (player.armor[i].type == hooks[j])
                        return false;
                }
            }
            return true;
        }

    }
}
