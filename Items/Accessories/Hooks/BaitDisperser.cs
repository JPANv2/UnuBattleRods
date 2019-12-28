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
    public class BaitDisperser: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bait Disperser");
            Tooltip.SetDefault("Inflicts used bait Debuffs to all surrounding enemies in a 8 block radius.");
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
            //sold by the fishlady
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().baitDispersalRange = 96; 
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (!base.CanEquipAccessory(player, slot))
                return false;

            int[] dispersers = { ModContent.ItemType<BaitDisperser>(), ModContent.ItemType<SuperiorBaitDisperser>() };
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == dispersers[0])
                {
                    return false;
                }
                if (player.armor[i].type == dispersers[1])
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == dispersers[0])
                {
                    return false;
                }
                if (player.armor[i].type == dispersers[1])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
