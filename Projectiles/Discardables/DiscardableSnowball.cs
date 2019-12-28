using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using UnuBattleRods.Buffs;
using UnuBattleRods.Projectiles.Bobbers;
using UnuBattleRods.Projectiles.Bobbers.NormalMode;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableSnowball: DiscardableProjectile
    {
        public int range;

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            range = 32;
        }

        public override bool effectAI()
        {
            //projectile.timeLeft--;
            if(projectile.timeLeft <= 0)
            {
                projectile.Kill();
                return true;
            }

            double angle = Main.rand.NextDouble()* Math.PI * 2;
            float rangePos = Main.rand.NextFloat(range);
            Dust.NewDust(Vector2.Add(projectile.Center, new Vector2((float)(rangePos * Math.Cos(angle)), (float)(rangePos * Math.Cos(angle)))), 8, 8, DustID.t_Frozen, 0, -0.5f, 0, default(Color), Main.rand.NextFloat() * 2 + 0.5f);

            Bobber b = new WoodenBobber();
            Rectangle rangeHitbox = new Rectangle((int)(projectile.position.X - (projectile.width / 2 + range / 2)), (int)(projectile.position.Y - (projectile.height / 2 + range / 2)), (int)(projectile.width + range), (int)(projectile.height + range));
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                if (i != npcIndex && b.canAttatchToNPC(Main.npc[i]))
                {
                    if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<EnemyFrozenDebuff>(), 361);
                    }
                }
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (i != npcIndex - Main.npc.Length && b.canAttatchToPlayer(Main.player[i]))
                {
                    if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.player[i].AddBuff(BuffID.Frozen, 360);
                    }
                }
            }

            return true;
        }
    }
}
