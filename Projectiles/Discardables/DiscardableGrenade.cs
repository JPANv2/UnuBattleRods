using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableGrenade: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Grenade);
            projectile.friendly = true;
            projectile.penetrate = -1;
            aiType = ProjectileID.Grenade;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.projectile, 32, true, true);
            this.projectile.type = ProjectileID.Grenade;
            return true;
        }
    }
}
