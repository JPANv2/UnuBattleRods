using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class WingCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wing Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("WingCrate");

        }

        public override void RightClick(Player player)
        {
            int wingcount = Main.rand.Next(1, 4);
            for (int i = 0; i < wingcount; i++)
            {
                wingselect(player);
            }
            base.RightClick(player);
        }
        public void wingselect(Player player)
        {
            switch (Main.rand.Next(41))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.AngelWings, 1);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.DemonWings, 1);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.FinWings, 1);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.Jetpack, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.BeeWings, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.ButterflyWings, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.FairyWings, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.BatWings, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.HarpyWings, 1);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.BoneWings, 1);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.WillsWings, 1);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.CrownosWings, 1);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.DTownsWings, 1);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.CenxsWings, 1);
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.BoneWings, 1);
                    break;
                case 15:
                    player.QuickSpawnItem(3228, 1);
                    break;
                case 16:
                    player.QuickSpawnItem(ItemID.Yoraiz0rWings, 1);
                    break;
                case 17:
                    player.QuickSpawnItem(ItemID.LokisWings, 1);
                    break;
                case 18:
                    player.QuickSpawnItem(ItemID.JimsWings, 1);
                    break;
                case 19:
                    player.QuickSpawnItem(ItemID.SkiphsWings, 1);
                    break;
                case 20:
                    player.QuickSpawnItem(ItemID.RedsWings, 1);
                    break;
                case 21:
                    player.QuickSpawnItem(ItemID.ArkhalisWings, 1);
                    break;
                case 22:
                    player.QuickSpawnItem(ItemID.LeinforsWings, 1);
                    break;
                case 23:
                    player.QuickSpawnItem(ItemID.MothronWings, 1);
                    break;
                case 24:
                    player.QuickSpawnItem(ItemID.LeafWings, 1);
                    break;
                case 25:
                    player.QuickSpawnItem(ItemID.FrozenWings, 1);
                    break;
                case 26:
                    player.QuickSpawnItem(ItemID.FlameWings, 1);
                    break;
                case 27:
                    player.QuickSpawnItem(823, 1);
                    break;
                case 28:
                    player.QuickSpawnItem(ItemID.BeetleWings, 1);
                    break;
                case 29:
                    player.QuickSpawnItem(ItemID.Hoverboard, 1);
                    break;
                case 30:
                    player.QuickSpawnItem(ItemID.FestiveWings, 1);
                    break;
                case 31:
                    player.QuickSpawnItem(ItemID.SpookyWings, 1);
                    break;
                case 32:
                    player.QuickSpawnItem(ItemID.TatteredFairyWings, 1);
                    break;
                case 33:
                    player.QuickSpawnItem(ItemID.BetsyWings, 1);
                    break;
                case 34:
                    player.QuickSpawnItem(ItemID.SteampunkWings, 1);
                    break;
                case 35:
                    player.QuickSpawnItem(ItemID.FishronWings, 1);
                    break;
                case 36:
                    player.QuickSpawnItem(ItemID.WingsNebula, 1);
                    break;
                case 37:
                    player.QuickSpawnItem(ItemID.WingsVortex, 1);
                    break;
                case 38:
                    player.QuickSpawnItem(ItemID.WingsStardust, 1);
                    break;
                case 39:
                    player.QuickSpawnItem(ItemID.FlyingCarpet, 1);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.WingsSolar, 1);
                    break;
            }
         }
      }
   }
