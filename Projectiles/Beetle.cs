using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles
{
    public class Beetle: ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GiantBee);
            projectile.width = 16;
            projectile.height= 16;
            Main.projFrames[projectile.type] = 4;
        }
    }
}
