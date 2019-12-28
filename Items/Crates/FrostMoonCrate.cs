using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class FrostMoonCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Moon Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("FrostMoonCrate");

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.ElfHat);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.ElfShirt);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ElfPants);
                        break;
                }
            }
            if (Main.rand.Next(7) == 0)
            {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(ItemID.ChristmasTreeSword);
                            break;
                        case 1:
                            player.QuickSpawnItem(ItemID.Razorpine);
                            break;
                        case 2:
                            player.QuickSpawnItem(ItemID.FestiveWings);
                            break;
                        default:
                            player.QuickSpawnItem(ItemID.ChristmasHook);
                            break;
                    }
                }
            if (Main.rand.Next(10) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(1910);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ChainGun);
                        break;
                }
            }
            if (Main.rand.Next(15) == 0)
                {
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            player.QuickSpawnItem(ItemID.BlizzardStaff);
                            break;
                        case 1:
                            player.QuickSpawnItem(ItemID.NorthPole, (1));
                            break;
                        case 2:
                            player.QuickSpawnItem(ItemID.SnowmanCannon, (1));
                            player.QuickSpawnItem(ItemID.Snowball, Main.rand.Next(25,100));
                        break;
                        case 3:
                            player.QuickSpawnItem(ItemID.BabyGrinchMischiefWhistle, (1));
                            break;
                        default:
                        player.QuickSpawnItem(ItemID.ReindeerBells, (1));
                            break;
                    }
                }

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemID.NaughtyPresent, (1));
                    }

            player.QuickSpawnItem(ItemID.Present, Main.rand.Next(1, 10));

            base.RightClick(player);
            }
        }
    }