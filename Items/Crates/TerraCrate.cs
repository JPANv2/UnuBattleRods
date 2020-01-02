using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;

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
                List<int> possibleBrokens = new List<int>();
                possibleBrokens.Add(ItemID.BrokenHeroSword);
                if (UnuBattleRods.thoriumPresent)
                {
                    int bhf = UnuBattleRods.getItemTypeFromTag("ThoriumMod:BrokenHeroFragment");
                    possibleBrokens.Add(bhf);
                    possibleBrokens.Add(bhf);
                    possibleBrokens.Add(bhf);
                }
                if (ModLoader.GetMod("ExpandedSentries") != null)
                {
                    int bhs = UnuBattleRods.getItemTypeFromTag("ExpandedSentries:BrokenSentryParts");
                    possibleBrokens.Add(bhs);
                    possibleBrokens.Add(bhs);
                }
                player.QuickSpawnItem(possibleBrokens[Main.rand.Next(possibleBrokens.Count)], 1);                
            }
            
            base.RightClick(player);
        }
    }
}
