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
    public class BetsyScales: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Betsy Scales");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 8;
            item.maxStack = 999;
        }
    }
}
