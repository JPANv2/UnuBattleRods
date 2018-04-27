using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class TerraCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("TerraCrate");

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                if (UnuBattleRods.thoriumPresent)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(ItemID.BrokenHeroSword, 1);
                            break;
                        case 1:
                            player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:BrokenHeroScythe"), 1);
                            break;
                        case 2:
                            player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:BrokenHeroStaff"), 1);
                            break;
                        default:
                            player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:BrokenHeroBow"), 1);
                            break;
                    }

                }else
                {

                    player.QuickSpawnItem(ItemID.BrokenHeroSword, 1);
                }

            }
            
            base.RightClick(player);
        }
    }
}
