using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class AnkhCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ankh Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("AnkhCrate");

        }

        public override void RightClick(Player player)
        {

            switch (Main.rand.Next(11))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.ObsidianSkull, 1);
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.CobaltShield, 1);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.TrifoldMap, 1);
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.FastClock, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.Vitamins, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(886, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.Blindfold, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.Nazar, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.Megaphone, 1);
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.Bezoar, 1);
                    break;
                default:
                    player.QuickSpawnItem(ItemID.AdhesiveBandage, 1);
                    break;
                }
                    base.RightClick(player);
            }
        }
    }
