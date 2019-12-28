using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableNuke: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.InfernoFriendlyBlast);
            projectile.friendly = true;
            projectile.penetrate = -1;
            aiType = ProjectileID.InfernoFriendlyBlast;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.projectile, 40, true, false);
            this.projectile.damage /= 4;
            createAreaDamage(this.projectile, 256, true, false);
            this.projectile.type = ProjectileID.InfernoFriendlyBlast;
            return true;
        }
    }
}
