using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class SandstormCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("SandstormCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.AntlionMandible, Main.rand.Next (1,6));
            }
            if (Main.rand.Next(13) == 0)
            {
                player.QuickSpawnItem(3772, 1);
            }
            if (Main.rand.Next(4) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.SharkFin, Main.rand.Next(1, 3));
            }
            if (Main.rand.Next(5) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(3783, Main.rand.Next(1, 6));
            }
            player.QuickSpawnItem(ItemID.SandBlock, Main.rand.Next(10, 50));
            player.QuickSpawnItem(ItemID.Cactus, Main.rand.Next(5, 40));
            base.RightClick(player);
        }
    }
}
