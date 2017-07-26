using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Shafts
{
    public class ManaShieldShaft: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Shield Shaft");
            Tooltip.SetDefault("Up to 15% of all damage received is deducted from your Mana instead.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:BossBars", 15);
            recipe.AddIngredient(mod, "StarMix", 10);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            base.AddRecipes();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).manaShield = true;
            player.GetModPlayer<FishPlayer>(mod).manaShieldPercentage += 0.15f;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            if (player.armor[slot].modItem != null && player.armor[slot].modItem is ManaShieldShaft)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is ManaShieldShaft)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is ManaShieldShaft)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
