using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class HardTriadBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 60;
            timeReelMax = 20;
            sizeMultiplier = 6.0f;
            speedIncrease = 3.0f;
        }

        public override float TensileStrength()
        {
            return 150f;
        }

        public override void PostAI()
        {
            if (npcIndex >= 0 && projectile.lavaWet || projectile.honeyWet)
                return;
            if ( projectile.wet || projectile.velocity.X == 0.0f)
            {
                int tornadoCounter = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == ProjectileID.SandnadoFriendly && Main.projectile[i].owner == projectile.owner)
                    {
                        tornadoCounter++;
                    }
                }
                if (tornadoCounter < 5)
                {
                    int p = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.SandnadoFriendly, projectile.damage / 2,0f, projectile.owner);
                }
            }
        }

        public override void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += (float)(43 * Main.player[base.projectile.owner].direction);
            if (Main.player[base.projectile.owner].direction < 0)
            {
               x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public override Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.boss || (target.realLife >= 0 && Main.npc[target.realLife].boss))
            {
                damage = (int)(Math.Round(damage *1.2f));
                crit |= Main.rand.Next(8) == 0;
            }else if (!target.boss)
            {
                damage *= 2;
                crit |= Main.rand.Next(4) == 0;
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            npc.AddBuff(mod.BuffType("Frostfire"), bobTime());
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            player.AddBuff(mod.BuffType("Frostfire"), bobTime());
            base.applyDamageAndDebuffs(target, player);
        }
    }
}