using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class EclipseCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eclipse Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("EclipseCrate");

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.ButchersChainsaw ,1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.NeptunesShell, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.DeadlySphereStaff, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.ToxicFlask, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.NailGun, 1);
                player.QuickSpawnItem(ItemID.Nail, Main.rand.Next (25,76));
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.DeathSickle, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.BrokenBatWing, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(ItemID.MoonStone, 1);
            }
            if (Main.rand.Next(20) == 0 && NPC.downedGolemBoss)
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
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(ItemID.MothronWings, 1);
                }
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(ItemID.TheEyeOfCthulhu, 1);
                }

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.Nail, Main.rand.Next(25, 76));
            }
            base.RightClick(player);
        }
    }
}
