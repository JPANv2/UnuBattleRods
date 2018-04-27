using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;

namespace UnuBattleRods.Items.Crates
{
    public class SoulCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crate");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("SoulCrate");

        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.SoulofLight, Main.rand.Next(3, 16));
            player.QuickSpawnItem(ItemID.SoulofNight, Main.rand.Next(3, 16));

            if(Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemID.SoulofFlight, Main.rand.Next(3, 16));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss1)
            {
                player.QuickSpawnItem(ItemID.SoulofSight, Main.rand.Next(1, 9));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss2)
            {
                player.QuickSpawnItem(ItemID.SoulofFright, Main.rand.Next(1, 9));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss3)
            {
                player.QuickSpawnItem(ItemID.SoulofMight, Main.rand.Next(1, 9));
            }

            if (FindSpectreRod(player))
            {
                player.QuickSpawnItem(ItemID.Ectoplasm, Main.rand.Next(1, 5));
            }
           

            base.RightClick(player);
        }

        private bool FindSpectreRod(Player player)
        {
           for(int i = 0; i< 50; i++)
            {
                if(player.inventory[i].type == mod.ItemType("SpectreBattlerod") || player.inventory[i].type == mod.ItemType("LifeforceBattlerod") || player.inventory[i].type == mod.ItemType("RodContainmentUnit"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
