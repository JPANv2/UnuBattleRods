using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableDynamite: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Dynamite);
            projectile.friendly = true;
            projectile.penetrate = -1;
            aiType = ProjectileID.Dynamite;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.projectile, 64, true, true);
            this.projectile.type = ProjectileID.Dynamite;
            return true;
        }
    }
}
