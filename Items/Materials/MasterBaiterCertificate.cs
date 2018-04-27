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
    public class MasterBaiterCertificate: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Certificate of a Master Baiter");
            Tooltip.SetDefault("Allows you to craft Powered Baits when consumed, permanently.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.useStyle = 4;
            item.useTime = 30;
            item.UseSound = SoundID.Item4;
            item.useAnimation = 30;
            item.rare = 12;
            item.maxStack = 1;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            return !pl.MasterBaiter;
        }

        public override bool UseItem(Player player)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            if (!pl.MasterBaiter)
            {
                pl.MasterBaiter = true;
                return true;
            }else
            {
                return false;
            }
        }
    }
}
