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
    public class FishSlicer: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fish Slicer");
            Tooltip.SetDefault("Turns all caught fish into Fish Steaks (if they can be converted).");
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
           
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().fishSlicer = true;
        }
    }
}
