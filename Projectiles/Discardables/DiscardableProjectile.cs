using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using UnuBattleRods.Configs;
using UnuBattleRods.Projectiles.Bobbers;
using UnuBattleRods.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRods.Projectiles.Discardables
{
    public abstract class DiscardableProjectile : ModProjectile
    {
        public int npcIndex = -1;
        public int trueDamage = 0;

        public override bool PreAI()
        {
            trueDamage = Math.Max(projectile.damage, trueDamage);
            return base.PreAI();
        }

        public override void PostAI()
        {
            projectile.damage = Math.Max(projectile.damage, trueDamage);
        }
        public override void AI()
        {
            if (npcIndex < 0)
            {
                projectile.Kill();
            }
            else
            {
                if (npcIndex < Main.npc.Length)
                {
                    projectile.Center = Main.npc[npcIndex].Center;
                }
                else if (npcIndex - Main.npc.Length < Main.player.Length)
                {
                    projectile.Center = Main.player[npcIndex - Main.npc.Length].Center;
                }
            }
            projectile.damage = Math.Max(projectile.damage, trueDamage);
            projectile.netUpdate = true;
            if (!effectAI())
            {
                base.AI();
            }
        }
        //Returns true if it's not to use the base projectile's AI
        public virtual bool effectAI()
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public static void createAreaDamage(Projectile proj, float range, bool applyfishingDamage = true, bool explosive = false)
        {
            int trueDamage = proj.damage;
            if (applyfishingDamage)
            {
                FishPlayer pl = Main.player[proj.owner].GetModPlayer<FishPlayer>();
                trueDamage = (int)(trueDamage * pl.bobberDamage);
            }

            if (ModContent.GetInstance<UnuServerConfig>().explosivesDamageEveryone && explosive)
            {
                trueDamage *= 2;
                Bobber b = new WoodenBobber();
                Rectangle rangeHitbox = new Rectangle((int)(proj.position.X - (proj.width / 2 + range / 2)), (int)(proj.position.Y - (proj.height / 2 + range / 2)), (int)(proj.width + range), (int)(proj.height + range));
                for (int i = 0; i < 200; i++) //Main.npc.Length
                {
                    if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.npc[i].StrikeNPC(trueDamage, 1, 0);
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.player[i].Hurt(PlayerDeathReason.ByPlayer(proj.owner), trueDamage, 0);
                    }
                }
            }
            else
            {
                Bobber b = new WoodenBobber();
                Rectangle rangeHitbox = new Rectangle((int)(proj.position.X - (proj.width / 2 + range / 2)), (int)(proj.position.Y - (proj.height / 2 + range / 2)), (int)(proj.width + range), (int)(proj.height + range));
                for (int i = 0; i < 200; i++) //Main.npc.Length
                {
                    if (b.canAttatchToNPC(Main.npc[i]))
                    {
                        if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                        {
                            Main.npc[i].StrikeNPC(trueDamage, 1, 0);
                        }
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (b.canAttatchToPlayer(Main.player[i]))
                    {
                        if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                        {
                            Main.player[i].Hurt(PlayerDeathReason.ByPlayer(proj.owner), trueDamage, 0);
                        }
                    }
                }
            }

           

        }
    }
}
