using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class OldOnesCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old One's Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("OldOnesCrate");

        }

        public override void RightClick(Player player)
        {
            if (NPC.downedGolemBoss)
            {
                player.QuickSpawnItem(mod.ItemType("BetsyScales"), Main.rand.Next(1,4));
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.WarTable, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.WarTableBanner, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.DD2PetDragon, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.DD2PetGato, 1);
                        break;
                }
            }
            if (Main.rand.Next(10) == 0 && NPC.downedMechBossAny)
            {
                switch (Main.rand.Next(10))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.ApprenticeScarf, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SquireShield, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.HuntressBuckler, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.MonkBelt, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(3852, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.DD2PhoenixBow, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(3823, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(3835, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(3836, 1);
                        break;
                    default:
                        player.QuickSpawnItem(3856, 1);
                        break;
                }
            }
            if (Main.rand.Next(10) == 0 && NPC.downedGolemBoss)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        player.QuickSpawnItem(3827, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(3858, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(3859, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(3870, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.BetsyWings, 1);
                        break;
                }

            }
            base.RightClick(player);
        }
    }
}
