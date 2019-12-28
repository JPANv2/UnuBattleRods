using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class BloodCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("BloodCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(10) == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemID.TopHat, 1);
                }
                else
                {
                    player.QuickSpawnItem(3478, 1);
                    player.QuickSpawnItem(3479, 1);
                }
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(ItemID.MoneyTrough, 1);
            }
            if (Main.rand.Next(15) == 0)
            {
                player.QuickSpawnItem(ItemID.SharkToothNecklace, 1);
            }
            if (Main.rand.Next(6) == 0)
            {
                player.QuickSpawnItem(ItemID.Shackle, 1);
            }
            if (Main.rand.Next(12) == 0)
            {
                player.QuickSpawnItem(ItemID.ZombieArm, 1);
            }
            if (Main.hardMode && Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(ItemID.Bananarang, 1);
            }
            if (Main.hardMode && Main.rand.Next(18) == 0)
            { 
                player.QuickSpawnItem(ItemID.SlapHand, 1);
            }
            base.RightClick(player);
        }
    }
}
