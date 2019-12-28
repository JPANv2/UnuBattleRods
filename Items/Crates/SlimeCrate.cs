using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class SlimeCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("SlimeCrate");

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(10) == 0 && NPC.downedSlimeKing)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.Solidifier, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SlimySaddle, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.NinjaHood, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.NinjaShirt, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.NinjaPants, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.SlimeHook, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.SlimeGun, 1);
                        break;
                }
            }
            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(18))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.SlimePlatform, Main.rand.Next (5,25) );
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SlimeWorkBench, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SlimeBathtub, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.SlimeBed, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.SlimeBookcase, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.SlimeCandelabra, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.SlimeCandle, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.SlimeChair, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.SlimeChandelier, 1);
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.SlimeChest, 1);
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.SlimeClock, 1);
                        break;
                    case 11:
                        player.QuickSpawnItem(ItemID.SlimeDoor, 1);
                        break;
                    case 12:
                        player.QuickSpawnItem(ItemID.SlimeDresser, 1);
                        break;
                    case 13:
                        player.QuickSpawnItem(ItemID.SlimeLamp, 1);
                        break;
                    case 14:
                        player.QuickSpawnItem(ItemID.SlimeLantern, 1);
                        break;
                    case 15:
                        player.QuickSpawnItem(ItemID.SlimePiano, 1);
                        break;
                    case 16:
                        player.QuickSpawnItem(ItemID.SlimeSofa, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.SlimeTable, 1);
                        break;
                }

            }

            if (Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(ItemID.SlimeStaff, 1);
            }
            if (Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem(ItemID.SlimeStatue, 1);
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.BlendOMatic, 1);
            }
            if (Main.rand.Next(9) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.AsphaltBlock, Main.rand.Next(5, 50));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ItemID.PinkGel, Main.rand.Next(5, 50));
            }
            if (Main.rand.Next(8) == 0)
            {
                player.QuickSpawnItem(ItemID.SlimeCrown, 1);
            }
            player.QuickSpawnItem(ItemID.Gel, Main.rand.Next(20, 300));
            base.RightClick(player);
        }
    }
}
