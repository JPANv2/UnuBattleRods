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
    public class CrustyStar: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crusty Star");
            Tooltip.SetDefault("A fallen star, crusty with obsidian.\nTo be used in the Extractinator.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
            item.maxStack = 999;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 10;
            ItemID.Sets.ExtractinatorMode[item.type] = item.type;
            item.autoReuse = true;
        }
        public override void ExtractinatorUse(ref int resultType, ref int resultStack)
        {
            resultType = ItemID.FallenStar;
            resultStack = Main.rand.Next(1, 3);
        }
    }
}
