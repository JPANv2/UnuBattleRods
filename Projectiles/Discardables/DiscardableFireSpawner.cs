using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableFireSpawner: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.MolotovFire);
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            aiType = -1;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override bool effectAI()
        {
            Entity target = npcIndex < 0 ? null : (npcIndex < Main.npc.Length ? (Entity)Main.npc[npcIndex] : (npcIndex - Main.npc.Length < Main.player.Length ? (Entity)Main.player[npcIndex - Main.npc.Length] : null));
            if(projectile.timeLeft % 30 == 3)
            {
                int proj = Projectile.NewProjectile(target != null ? target.Bottom : projectile.Center, Vector2.Zero, ProjectileID.MolotovFire, trueDamage*5, 0, projectile.owner);
                if(proj >= 0 && proj < Main.projectile.Length)
                {

                }
            }
            return true;
        }

        public override bool PreKill(int timeLeft)
        {
            return true;
        }
    }
}
