using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class AlienCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alien Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("AlienCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem(ItemID.BrainScrambler, 1);
            }
            if (Main.rand.Next(12) == 0)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.Xenopopper, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.XenoStaff, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.LaserMachinegun, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.LaserDrill, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.ElectrosphereLauncher, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.ChargedBlasterCannon, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.InfluxWaver, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.CosmicCarKey, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.AntiGravityHook, 1);
                        break;
                }
            }
            if (Main.rand.Next(6) == 0)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.MartianCostumeMask, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.MartianCostumeShirt, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.MartianCostumePants, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.MartianUniformHelmet, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.MartianUniformTorso, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.MartianUniformPants, 1);
                        break;
                }
            }
            if (Main.rand.Next(6) == 0)

                player.QuickSpawnItem(ItemID.MartianConduitPlating, Main.rand.Next(10,36));
            base.RightClick(player);
        }
    }
}
