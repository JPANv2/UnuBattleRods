using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class ShroomiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("ShroomiteCrate");

        }

        public override void RightClick(Player player)
        {

            player.QuickSpawnItem(ItemID.ShroomiteBar, Main.rand.Next(3, 16));
            
            base.RightClick(player);
        }
    }
}
