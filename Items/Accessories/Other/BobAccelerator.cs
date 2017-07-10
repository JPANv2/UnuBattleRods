using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Other
{
    public class BobAccelerator: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Bob Accelerator");
            Tooltip.SetDefault("Increases your casting speed by 30%");
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
            base.AddRecipes();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>(mod).bobberShootSpeed += 0.30f;
        }
    }
}
