using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class GoblinCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("GoblinCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemID.Harpoon, 1);
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.ShadowFlameHexDoll, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.ShadowFlameKnife, 1);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ShadowFlameBow, 1);
                        break;
                }
            }
            if (Main.rand.Next(5) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(mod.ItemType("Shadowflame"), Main.rand.Next(1, 5));
            }
            player.QuickSpawnItem(ItemID.SpikyBall, Main.rand.Next(10,150));
            base.RightClick(player);
        }
    }
}
