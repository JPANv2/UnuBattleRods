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
    public abstract class FishingLure : ModItem
    {

        public int lures = 0;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<FishPlayer>(mod).multilineFishing += lures;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is FishingLure)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is FishingLure)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
