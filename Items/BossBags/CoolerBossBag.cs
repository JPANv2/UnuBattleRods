using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Materials;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.Items.Weapons.Cooler;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Items.BossBags
{
    class CoolerBossBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Treasure Bag");
            base.Tooltip.SetDefault("Right click to open");
        }

        public override void SetDefaults()
        {
            base.item.maxStack = 999;
            base.item.consumable = true;
            base.item.width = 24;
            base.item.height = 24;
            base.item.rare = 9;
            base.item.expert = true;
            this.bossBagNPC = base.mod.NPCType("CoolerBoss");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ItemID.Hook, Main.rand.Next(1, 4));
            if (Main.rand.Next(3) == 0)
                player.QuickSpawnItem(mod.ItemType<CoolerBattlerod>());
            else
            {
                switch (Main.rand.Next(4))
                {
                    case 1:
                        player.QuickSpawnItem(mod.ItemType<Melonbrand>());
                        break;
                    case 2:
                        player.QuickSpawnItem(mod.ItemType<MagicSoda>());
                        break;
                    case 3:
                        player.QuickSpawnItem(mod.ItemType<BeerPack>());
                        break;
                    default:
                        player.QuickSpawnItem(mod.ItemType<IceCreamer>());
                        break;
                }
            }
            if (CoolerBoss.doesItDropCertificate())
            {
                player.QuickSpawnItem(mod.ItemType<MasterBaiterCertificate>(), 1);
            }
        }

    }
}
