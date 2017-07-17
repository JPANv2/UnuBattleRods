using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class ObsidianCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Crate");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("ObsidianCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(40) == 0 || (!Main.hardMode && Main.rand.Next(10) == 0))
            {
                player.QuickSpawnItem(ItemID.GuideVoodooDoll);
            }
            

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.QueenStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.StarStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.ObsidianBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.ObsidianBed);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.ObsidianBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.ObsidianCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.ObsidianCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.ObsidianChair);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.ObsidianChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.ObsidianChest);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.ObsidianClock);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.ObsidianDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.ObsidianLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.ObsidianLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.ObsidianPiano);
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.ObsidianSink);
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.ObsidianSofa);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.ObsidianTable);
                    break;
            }

            player.QuickSpawnItem(ItemID.Obsidian, Main.rand.Next(25, 76));
            player.QuickSpawnItem(ItemID.Hellstone, Main.rand.Next(5, 26));
            base.RightClick(player);
        }
    }
}
