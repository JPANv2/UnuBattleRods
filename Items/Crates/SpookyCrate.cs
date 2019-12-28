using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class SpookyCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("SpookyCrate");

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                switch (Main.rand.Next(12))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.SpookyTwig);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SpookyHook);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.CursedSapling);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.NecromanticScroll);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.StakeLauncher);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.TheHorsemansBlade);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.BatScepter);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.BlackFairyDust);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.SpiderEgg);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.RavenStaff);
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.CandyCornRifle);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.JackOLanternLauncher);
                        break;
                }
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.Stake, Main.rand.Next(30, 61));

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.CandyCorn, Main.rand.Next(50, 101));

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.ExplosiveJackOLantern, Main.rand.Next(25, 51));

            }

            if (Main.rand.Next(2) == 0)
            {
                switch (Main.rand.Next(16))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.SpookyBathtub);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SpookyBed);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SpookyBookcase);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.SpookyCandelabra);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.SpookyCandle);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.SpookyChair);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.SpookyChandelier);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.SpookyChest);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.SpookyClock);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.SpookyDresser);
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.SpookyLamp);
                        break;
                    case 11:
                        player.QuickSpawnItem(ItemID.SpookyLantern);
                        break;
                    case 12:
                        player.QuickSpawnItem(ItemID.SpookyPiano);
                        break;
                    case 13:
                        player.QuickSpawnItem(ItemID.SpookySink);
                        break;
                    case 14:
                        player.QuickSpawnItem(ItemID.SpookySofa);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.SpookyTable);
                        break;
                }
            }else
            {
                switch (Main.rand.Next(16))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.PumpkinBathtub);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.PumpkinBed);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.PumpkinBookcase);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.PumpkinCandelabra);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.PumpkinCandle);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.PumpkinChair);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.PumpkinChandelier);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.PumpkinChest);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.PumpkinClock);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.PumpkinDresser);
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.PumpkinLamp);
                        break;
                    case 11:
                        player.QuickSpawnItem(ItemID.PumpkinLantern);
                        break;
                    case 12:
                        player.QuickSpawnItem(ItemID.PumpkinPiano);
                        break;
                    case 13:
                        player.QuickSpawnItem(ItemID.PumpkinSink);
                        break;
                    case 14:
                        player.QuickSpawnItem(ItemID.PumpkinSofa);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.PumpkinTable);
                        break;
                }
            }

            player.QuickSpawnItem(ItemID.SpookyWood, Main.rand.Next(25, 76));
            player.QuickSpawnItem(ItemID.Pumpkin, Main.rand.Next(25, 76));
            player.QuickSpawnItem(ItemID.GoodieBag, Main.rand.Next(1, 5));
            base.RightClick(player);
        }
    }
}
