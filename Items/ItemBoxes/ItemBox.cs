using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Items.ItemBoxes
{
    public abstract class ItemBox : ModItem
    {
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        protected int boxedItemMin;
        protected int boxedItemMax;
        protected int boxedItemID;

        public override void SetDefaults()
        {
            base.item.maxStack = 999;
            base.item.consumable = true;
            base.item.width = 16;
            base.item.height = 16;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {           
            if (boxedItemMin != boxedItemMax)
            {
               player.QuickSpawnItem(boxedItemID, Main.rand.Next(boxedItemMin, boxedItemMax + 1));
            }
            else
            {
                player.QuickSpawnItem(boxedItemID, boxedItemMax);
            }            
        }
    }
}
