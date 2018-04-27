using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Weapons
{
	public class GrapeSodaSpray : ModProjectile
	{

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.thrown = false;
            projectile.magic = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = -1;
        }

        public override void PostAI()
        {
            if (Main.rand.Next(2) == 0)
            {
                projectile.damage -= 1;
            }
            if(Main.rand.Next(1000) == 0)
            {
                projectile.damage = 200;
            }

        }
    }

}

