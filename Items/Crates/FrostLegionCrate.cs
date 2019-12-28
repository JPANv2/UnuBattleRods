using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class FrostLegionCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Legion Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("FrostLegionCrate");

        }

        public override void RightClick(Player player)
        {
         player.QuickSpawnItem(ItemID.SnowBlock, Main.rand.Next(1, 1000));
         player.QuickSpawnItem(ItemID.SnowGlobe, 1);
         player.QuickSpawnItem(1869, Main.rand.Next(1, 4));

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemID.Snowball, Main.rand.Next(1, 1000));
            }
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemID.IceBlock, Main.rand.Next(1, 1000));
            }
            base.RightClick(player);
        }
    }
}
