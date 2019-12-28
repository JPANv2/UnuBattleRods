using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRods;

namespace UnuBattleRods.Items.Accessories.Lures
{
    public abstract class SelectiveLure : ModItem
    {

        public int maxHooking = 0;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            if (maxHooking == 0)
            {
                SetDefaults();
            }
            if (maxHooking == -1)
            {
                player.GetModPlayer<FishPlayer>().maxBobbersPerEnemy = 0;
            }
            else
            {
                player.GetModPlayer<FishPlayer>().maxBobbersPerEnemy += maxHooking;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            if (player.armor[slot].modItem != null && player.armor[slot].modItem is SelectiveLure)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is SelectiveLure)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is SelectiveLure)
                {
                    return false;
                }
            }
            return true;
        }
    }
}