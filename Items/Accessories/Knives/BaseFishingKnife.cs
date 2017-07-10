using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Knives
{
    public abstract class BaseFishingKnife : ModItem
    {
        public int baseDamage = 0;
        public float baseKnockback = 0;
        public int cooldown = 0;
        public float radius = 0;
        public int buffID = 0;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>(mod);
            p.knifeBaseDamage = baseDamage;
            p.knifeCooldown = cooldown;
            p.knifeRadius = radius;
            p.knifeKnockback = baseKnockback;
            if (buffID > 0)
                p.knifeDebuff = buffID;
        }

       
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is BaseFishingKnife)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].modItem != null && player.armor[i].modItem is BaseFishingKnife)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
