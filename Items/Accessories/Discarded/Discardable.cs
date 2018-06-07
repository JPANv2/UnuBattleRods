using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Discarded
{
    public abstract class Discardable : ModItem
    {

        public virtual void onDiscard(Entity target)
        {

        }

    }
}
