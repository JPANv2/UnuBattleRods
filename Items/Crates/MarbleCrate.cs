using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class MarbleCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marble Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("MarbleCrate");

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(ItemID.PocketMirror);
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(ItemID.MedusaHead);
            }

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.HopliteStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.MedusaStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.MarbleBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.MarbleBed);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.MarbleBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.MarbleCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.MarbleCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.MarbleChair);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.MarbleChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.MarbleChest);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.MarbleClock);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.MarbleDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.MarbleLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.MarbleLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.MarblePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.MarbleSink);
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.MarbleSofa);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.MarbleTable);
                    break;
            }

            player.QuickSpawnItem(ItemID.Marble, Main.rand.Next(25, 76));
           
            base.RightClick(player);
        }
    }
}
