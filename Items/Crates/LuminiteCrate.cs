using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;

namespace UnuBattleRods.Items.Crates
{
    public class LuminiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminite Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("LuminiteCrate");

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(9))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.Meowmere);
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.Terrarian);
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.StarWrath);
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.LastPrism);
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.LunarFlareBook);
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.SDMG);
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.FireworksLauncher);
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.MoonlordTurretStaff);
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.RainbowCrystalStaff);
                        break;
                }
            }

            if (Main.rand.Next(3) == 0)
            {
                if(Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemID.MoonlordBullet, Main.rand.Next(10, 51));
                }
                else
                {
                    player.QuickSpawnItem(ItemID.MoonlordArrow, Main.rand.Next(10, 51));
                }
            }

                player.QuickSpawnItem(ItemID.LunarOre, Main.rand.Next(4, 25));

            List<int> possibleFragments = new List<int>();
            possibleFragments.Add(ItemID.FragmentSolar);
            possibleFragments.Add(ItemID.FragmentNebula);
            possibleFragments.Add(ItemID.FragmentStardust);
            possibleFragments.Add(ItemID.FragmentVortex);

            if (UnuBattleRods.thoriumPresent)
            {
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("ThoriumMod:CelestialFragment"));
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("ThoriumMod:WhiteDwarfFragment"));
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("ThoriumMod:CometFragment"));
            }
            if(ModLoader.GetMod("DBZMOD") != null)
            {
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("DBZMOD:RadiantFragment"));
            }
            if (ModLoader.GetMod("SacredTools") != null)
            {
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("SacredTools:FragmentNova"));
            }
            if (ModLoader.GetMod("ExpandedSentries") != null)
            {
                possibleFragments.Add(UnuBattleRods.getItemTypeFromTag("ExpandedSentries:EclipseFragment"));
            }

            player.QuickSpawnItem(possibleFragments[Main.rand.Next(possibleFragments.Count)] , Main.rand.Next(2, 21));

            base.RightClick(player);
        }
    }
}
