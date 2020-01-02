using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class CritterCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Critter Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("CritterCrate");

        }

        public override void RightClick(Player player)
        {
            int Crittercount = Main.rand.Next(1, 4);
            for (int i = 0; i < Crittercount; i++)
            {
                BaitSelect(player);
                ButterflySelect(player);
                Critterselect(player);
            }
            base.RightClick(player);
        }

        public void BaitSelect(Player player)
        {
            if(Main.hardMode && Main.rand.Next(15)==0)
            {
                player.QuickSpawnItem(ItemID.TruffleWorm, 1);
            }
            switch (Main.rand.Next(12)) {
                 case 1:
                    player.QuickSpawnItem(ItemID.BlackScorpion, 1);
                    break;
                 case 2:
                    player.QuickSpawnItem(ItemID.Buggy, 1);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.EnchantedNightcrawler, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.Grasshopper, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.GoldGrasshopper, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.Grubby, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.GlowingSnail, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.Scorpion, 1);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.Sluggy, 1);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.Snail, 1);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.Worm, 1);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.GoldWorm, 1);
                    break;
            }
        }

        public void ButterflySelect(Player player)
        {
                switch (Main.rand.Next(11))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.Firefly, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.LightningBug, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.GoldButterfly, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.JuliaButterfly, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.MonarchButterfly, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.PurpleEmperorButterfly, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.RedAdmiralButterfly, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.SulphurButterfly, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.TreeNymphButterfly, 1);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.UlyssesButterfly, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ZebraSwallowtailButterfly, 1);
                        break;
                }
            
        }

        public void Critterselect(Player player)
        {
            switch (Main.rand.Next(17))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.Bird, 1);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.GoldBird, 1);
                    break;
               
                case 2:
                    player.QuickSpawnItem(ItemID.BlueJay, 1);
                    break;
                
                case 3:
                    player.QuickSpawnItem(ItemID.Bunny, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.GoldBunny, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.Cardinal, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.Duck, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.Frog, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.GoldFrog, 1);
                    break;
                
                case 9:
                    player.QuickSpawnItem(ItemID.Goldfish, 1);
                    break;
               
                case 10:
                    player.QuickSpawnItem(ItemID.MallardDuck, 1);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.Mouse, 1);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.GoldMouse, 1);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.Penguin, 1);
                    break;
                case 14:
                    player.QuickSpawnItem(3563, 1);
                    break;
                
                case 15:
                    player.QuickSpawnItem(ItemID.Squirrel, 1);
                    break;
                case 16:
                    player.QuickSpawnItem(ItemID.SquirrelGold, 1);
                    break;
                
                       }
         }
      }
   }
