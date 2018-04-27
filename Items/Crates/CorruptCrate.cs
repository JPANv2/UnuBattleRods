using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class CorruptCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("CorruptCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(ItemID.ScourgeoftheCorruptor);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.DartRifle);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.ChainGuillotines);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.ClingerStaff);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.PutridScent);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.WormHook);
                        break;
                }
            }

            if (Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.BandofStarpower);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.Musket);
                        player.QuickSpawnItem(ItemID.MusketBall, 100);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.Vilethorn);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.BallOHurt);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ShadowOrb);
                        break;
                }
            }

            if (Main.hardMode && Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.CursedFlame, Main.rand.Next(2, 8));
            }

            player.QuickSpawnItem(ItemID.DemoniteOre, Main.rand.Next(5, 26));
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.ShadowScale, Main.rand.Next(2, 9));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemID.RottenChunk, Main.rand.Next(10, 31));
            }
            base.RightClick(player);
        }
    }
}
