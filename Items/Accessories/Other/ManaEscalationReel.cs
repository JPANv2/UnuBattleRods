using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Other
{
    public class ManaEscalationReel: ModItem
    {
        public override void SetStaticDefaults()
        {           
            DisplayName.SetDefault("Mana Escalation Reel");
            Tooltip.SetDefault("Increases bobber damage by 2% each second, but costs 8 mana per second.");
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
            recipe.AddIngredient(mod, "StarMix", 5);
            recipe.AddIngredient(mod,"BobEscalationPotion", 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.escalation = true;
            p.escalationManaCost += 8;
            p.escalationFromMana = true;
            p.escalationFromManaBonus = 0.02f;
            //p.escalationFromManaMax = 1.0f;
            
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            if (player.armor[slot].modItem != null && player.armor[slot].modItem is ManaEscalationReel)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is ManaEscalationReel)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is ManaEscalationReel)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
