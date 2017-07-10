using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles
{
    public class FishProjectileInfo : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true; 
            }
        }
        public bool hasBeenCalculated = false;
        public bool isDodged = false;

    }
}
