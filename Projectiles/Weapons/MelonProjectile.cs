using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Weapons
{
    public class MelonProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SeedlerNut);
            projectile.thrown = false;
            projectile.melee = true;
            projectile.width = 16;
            projectile.height = 16;
        }

        public override void PostAI()
        {
            if (projectile.velocity.X == 0)
                projectile.Kill();
        }
    }
}
