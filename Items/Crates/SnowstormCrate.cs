using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class SnowstormCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowstorm Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("SnowstormCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(4) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(1136, 1);
                        break;
                    default:
                        player.QuickSpawnItem(1135, 1);
                        break;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ItemID.UmbrellaHat, 1);
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ItemID.NimbusRod, 1);
            }
            if (Main.rand.Next(9) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.RainbowBrick, Main.rand.Next(10,30));
            }
            if (Main.rand.Next(13) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.IceFeather, 1);
            }
            if (Main.rand.Next(4) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.FrostCore, Main.rand.Next(1, 6));
            }
            if (Main.rand.Next(7) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.FrostStaff, 1);
            }
            player.QuickSpawnItem(ItemID.SnowBlock, Main.rand.Next(10, 50));
            player.QuickSpawnItem(ItemID.Snowball, Main.rand.Next(5, 40));
            base.RightClick(player);
        }
    }
}
