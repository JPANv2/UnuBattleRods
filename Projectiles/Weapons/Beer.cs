using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Weapons
{
    public class Beer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Ale);
            aiType = ProjectileID.Ale;
        }
    }
}
