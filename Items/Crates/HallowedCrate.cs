using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class HallowedCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("HallowedCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(ItemID.RainbowGun);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.FlyingKnife);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.CrystalVileShard);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.DaedalusStormbow);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.BlessedApple);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.IlluminantHook);
                        break;
                }
            }

            player.QuickSpawnItem(ItemID.HallowedBar, Main.rand.Next(2, 13));
            
            base.RightClick(player);
        }
    }
}
