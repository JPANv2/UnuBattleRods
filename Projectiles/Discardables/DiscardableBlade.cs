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

namespace UnuBattleRods.Projectiles.Discardables
{
    public class DiscardableBlade: DiscardableProjectile
    {
        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Crimtane Scythe");
        }

        public override void SetDefaults()
        {
            base.projectile.width = 100;
            base.projectile.height = 100;
            base.projectile.aiStyle = 0;
            base.projectile.penetrate = 100;
            base.projectile.light = 0.2f;
            base.projectile.friendly = true;
            base.projectile.tileCollide = false;
            base.projectile.ownerHitCheck = true;
            base.projectile.ignoreWater = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.Width += 16;
            projHitbox.Height += 16;
            return new bool?(projHitbox.Intersects(targetHitbox));
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[base.projectile.owner];
            if (target.Center.X < player.Center.X)
            {
                hitDirection = -1;
                return;
            }
            hitDirection = 1;
        }

        public override bool effectAI()
        {
            
            NPC targetN = npcIndex < Main.npc.Length ? Main.npc[npcIndex] : null;
            Player targetP = (targetN == null && (npcIndex - Main.npc.Length < Main.player.Length)) ? Main.player[npcIndex - Main.npc.Length] : null;
            if (projectile.width == 100 || projectile.height == 100)
            {
                if (targetP != null)
                {
                    projectile.width = Math.Max(targetP.width + 32, targetP.height + 32);
                    projectile.height = Math.Max(targetP.width + 32, targetP.height + 32);
                
                }
                else if(targetN != null)
                {
                    projectile.width = Math.Max(targetN.width + 32, targetN.height + 32);
                    projectile.height = Math.Max(targetN.width + 32, targetN.height + 32);
                    
                }
                projectile.scale = projectile.width * 0.01f;
            }

          
            if ((targetP == null && targetN == null) || (targetP != null && targetP.dead) || (targetN != null && !targetN.active))
            {
                base.projectile.Kill();
                return true;
            }
            if(targetP != null)
            {
                if (targetP.direction > 0)
                {
                    base.projectile.rotation += 0.25f;
                    base.projectile.spriteDirection = 1;
                }
                else
                {
                    base.projectile.rotation -= 0.25f;
                    base.projectile.spriteDirection = -1;
                }
            }
            if (targetN != null)
            {
                if (targetN.direction > 0)
                {
                    base.projectile.rotation += 0.25f;
                    base.projectile.spriteDirection = 1;
                }
                else
                {
                    base.projectile.rotation -= 0.25f;
                    base.projectile.spriteDirection = -1;
                }
            }
            return true;
        }
    }
}
