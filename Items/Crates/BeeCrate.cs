using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class BeeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Crate");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            LesserReplacement = ItemID.BottledHoney;
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0,1,0,0);
            item.createTile = mod.TileType("BeeCrate");

        }

        public override void RightClick(Player player)
        {

            player.QuickSpawnItem(ItemID.HiveWall, Main.rand.Next(5, 26));

            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ItemID.Beenade, Main.rand.Next(3, 13));
            }

            if (Main.rand.Next(35) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.BeeGun);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.BeeKeeper);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.BeesKnees);
                        break;
                }
                
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(ItemID.HoneyComb);
            }

            if(Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(ItemID.Nectar);
            }
            if (Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(ItemID.HoneyedGoggles);
            }

            base.RightClick(player);
        }
    }
}
