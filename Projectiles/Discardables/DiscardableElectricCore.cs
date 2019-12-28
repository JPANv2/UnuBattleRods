using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableElectricCore: DiscardableProjectile
    {
        public int originalDamage = 0;
        public int timeToStrike = 60;

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.penetrate = -1;
            aiType = ProjectileID.InfernoFriendlyBlast;
        }

        public override bool PreKill(int timeLeft)
        {
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            timeToStrike--;
            if(timeToStrike <= 0)
            {
                timeToStrike = 60;
            }
            return true;
        }
    }
}
