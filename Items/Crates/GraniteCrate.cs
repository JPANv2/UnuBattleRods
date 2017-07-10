using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class GraniteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("GraniteCrate");

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(ItemID.NightVisionHelmet);
            }
            

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.GraniteGolemStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.WomanStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.GraniteBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.GraniteBed);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.GraniteBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.GraniteCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.GraniteCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.GraniteChair);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.GraniteChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.GraniteChest);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.GraniteClock);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.GraniteDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.GraniteLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.GraniteLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.GranitePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.GraniteSink);
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.GraniteSofa);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.GraniteTable);
                    break;
            }

            player.QuickSpawnItem(ItemID.Granite, Main.rand.Next(25, 76));
           
            base.RightClick(player);
        }
    }
}
