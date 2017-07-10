using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Metronomes
{
    public abstract class Metronome : ModItem
    {
        public float bobberDamage = 0f;
        public float bobberSpeed = 0f;
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FishPlayer>(mod).bobberDamage += bobberDamage;
            player.GetModPlayer<FishPlayer>(mod).bobberSpeed += bobberSpeed;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is Metronome)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is Metronome)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
