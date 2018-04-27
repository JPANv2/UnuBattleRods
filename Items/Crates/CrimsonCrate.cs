using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class CrimsonCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("CrimsonCrate");

        }

        public override void RightClick(Player player)
        {
            if(Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(ItemID.VampireKnives);
            }

            if(Main.rand.Next(50)==0)
            {
                player.QuickSpawnItem(ItemID.MeatGrinder, 1);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.DartPistol);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.FetidBaghnakhs);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.SoulDrain);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.FleshKnuckles);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.TendonHook);
                        break;
                }
            }

            if(Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.PanicNecklace);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.TheUndertaker);
                        player.QuickSpawnItem(ItemID.MusketBall, 100);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.CrimsonRod);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.TheRottedFork);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.CrimsonHeart);
                        break;
                }
            }

            if(Main.hardMode && Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.Ichor, Main.rand.Next(2, 8));
            }

            player.QuickSpawnItem(ItemID.CrimtaneOre, Main.rand.Next(5, 26));
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.TissueSample, Main.rand.Next(2, 9));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemID.Vertebrae, Main.rand.Next(10, 31));
            }
            base.RightClick(player);
        }
    }
}
