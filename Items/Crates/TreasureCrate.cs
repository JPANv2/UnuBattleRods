using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class TreasureCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("TreasureCrate");

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(15) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.CoinGun, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.Cutlass, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.GoldRing, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.LuckyCoin, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.PirateStaff, 1);
                        break;
                }
            }
            if (Main.rand.Next(7) == 0)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.EyePatch, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SailorHat, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SailorShirt, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.SailorPants, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.BuccaneerBandana, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.BuccaneerShirt, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.BuccaneerPants, 1);
                        break;
                }
            }
            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(19))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.GoldenBathtub, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.GoldenBed, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.GoldenBookcase, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.GoldenCandelabra, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.GoldenCandle, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.GoldenChair, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.GoldenChest, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.GoldenDoor, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.GoldenDresser, 1);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.GoldenClock, 1);
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.GoldenLamp, 1);
                        break;
                    case 11:
                        player.QuickSpawnItem(ItemID.GoldenLantern, 1);
                        break;
                    case 12:
                        player.QuickSpawnItem(ItemID.GoldenPiano, 1);
                        break;
                    case 13:
                        player.QuickSpawnItem(ItemID.GoldenPlatform, 1);
                        break;
                    case 14:
                        player.QuickSpawnItem(ItemID.GoldenSink, 1);
                        break;
                    case 15:
                        player.QuickSpawnItem(ItemID.GoldenSofa, 1);
                        break;
                    case 16:
                        player.QuickSpawnItem(ItemID.GoldenTable, 1);
                        break;
                    case 17:
                        player.QuickSpawnItem(ItemID.GoldenToilet, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.GoldenWorkbench, 1);
                        break;
                }
            }
            base.RightClick(player);
        }
    }
}
