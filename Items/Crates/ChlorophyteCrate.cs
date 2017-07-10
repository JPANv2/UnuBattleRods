using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class ChlorophyteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("ChlorophyteCrate");

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(ItemID.PiranhaGun);
            }

            if(Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(ItemID.Seedling);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.Seedler);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.ThornHook);
                        break;
                }
            }

            player.QuickSpawnItem(ItemID.ChlorophyteOre, Main.rand.Next(5, 26));
           
            base.RightClick(player);
        }
    }
}
