using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class MeteorCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("MeteorCrate");

        }

        public override void RightClick(Player player)
        {

            /*if(Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem();
            }*/
            

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.KingStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.HeartStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.MeteoriteBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.MeteoriteBed);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.MeteoriteBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.MeteoriteCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.MeteoriteCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.MeteoriteChair);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.MeteoriteChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.MeteoriteChest);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.MeteoriteClock);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.MeteoriteDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.MeteoriteLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.MeteoriteLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.MeteoritePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.MeteoriteSink);
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.MeteoriteSofa);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.MeteoriteTable);
                    break;
            }

            player.QuickSpawnItem(ItemID.Meteorite, Main.rand.Next(15, 36));
            base.RightClick(player);
        }
    }
}
