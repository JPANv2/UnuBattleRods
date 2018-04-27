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

            switch (Main.rand.Next(5))
            {
                case 0:
                case 1:
                    player.QuickSpawnItem(ItemID.CrystalShard, Main.rand.Next(4,13));
                    break;
                case 2:
                case 3:
                    player.QuickSpawnItem(ItemID.PixieDust, Main.rand.Next(4, 13));
                    break;
                default:
                    player.QuickSpawnItem(ItemID.UnicornHorn, Main.rand.Next(2, 6));
                    break;
            }

            if (NPC.downedMechBossAny)
            {
                player.QuickSpawnItem(ItemID.HallowedBar, Main.rand.Next(2, 13));
                if (UnuBattleRods.thoriumPresent && Main.rand.Next(10) == 1)
                {
                    if(Main.rand.Next(2) == 1)
                    {
                        player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:StrangePlating"), Main.rand.Next(2, 7));
                    }
                    else
                    {
                        player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:LifeCell"), Main.rand.Next(1, 4));
                    }
                }
            }
            
            base.RightClick(player);
        }
    }
}
