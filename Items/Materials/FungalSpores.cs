using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Materials
{
    public class FungalSpores: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungal Spores");
            Tooltip.SetDefault("This could be used to prepare fur.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 0, 10);
            item.rare = 1;
            item.maxStack = 999;
        }
    }
}
