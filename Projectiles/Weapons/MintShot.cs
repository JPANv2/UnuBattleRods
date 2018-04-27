using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Weapons
{
    public class MintShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.thrown = false;
            projectile.ranged = true;
            projectile.width = 16;
            projectile.height = 16;
        }
    }
}
