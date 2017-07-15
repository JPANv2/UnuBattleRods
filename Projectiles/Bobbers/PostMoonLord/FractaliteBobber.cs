using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.PostMoonLord
{
    public class FractaliteBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 20;
            timeReelMax = 20;
            sizeMultiplier = 12.0f;
            speedIncrease = 3.0f;
        }

        public override float TensileStrength()
        {
            return 1500f;
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

        int counter = 0;
        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 0.0f, 0.9f, 0.6f);

            int size = 64;
            Entity e = getStuckEntity();
            if (!isStuck())
                spawnDust(Main.player[projectile.owner], projectile);

            size += e.width > e.height ? e.width : e.height;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.immortal && !npc.dontTakeDamage &&
                 !(npc.friendly && !(npc.type == NPCID.Guide && Main.player[projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[projectile.owner].killClothier))
                 )
                {
                    if (Vector2.Distance(npc.Center, e.Center) < size && !npc.buffImmune[mod.BuffType("Solarfire")])
                    {
                        npc.AddBuff(mod.BuffType("Solarfire"), 120);
                    }
                }
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player p = Main.player[i];
                if (p.active && p.hostile && (p.team == 0 || p.team != Main.player[projectile.owner].team) && Vector2.Distance(p.Center, e.Center) < size && p.whoAmI != projectile.owner)
                {
                    if (!p.buffImmune[mod.BuffType("Solarfire")])
                        p.AddBuff(mod.BuffType("Solarfire"), 120);
                }
            }

            if (isStuck())
            {
                if (timeSinceLastBob == bobTime() - 1)
                {
                    counter++;
                    if (counter >= 6)
                    {
                        counter = 0;
                        shootBullets(Main.player[projectile.owner], getStuckEntity());
                    }
                }
            }

            if (!isStuck() && (projectile.wet || projectile.lavaWet || projectile.honeyWet))
            {
                if (timeSinceLastBob <= 0)
                {
                    counter++;
                    timeSinceLastBob = bobTime();
                }
                if (counter >= 3)
                {
                    counter = 0;
                    shootBullets(Main.player[projectile.owner], projectile);
                }
                timeSinceLastBob--;
            }
        }



        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            spawnDust(player, npc);
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            spawnDust(player, target);
            base.applyDamageAndDebuffs(target, player);
        }

        private void shootBullets(Player player, Entity npc)
        {
            int bulletCounter = 0;
            for (int i = 0; i< Main.projectile.Length; i++)
            {
                if(Main.projectile[i].active && Main.projectile[i].type == ProjectileID.ChlorophyteBullet && Main.projectile[i].owner == projectile.owner)
                {
                    bulletCounter++;
                }
            }
            if (bulletCounter >= 12)
                return;
            int max = Main.rand.Next(1, 3);
            int proj = ProjectileID.ChlorophyteBullet;
            float kb = 0;
            int dmg = projectile.damage + 10;
            Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
            int size = npc.width > npc.height ? npc.width : npc.height;
            for (int i = 0; i < max; i++)
            {
                double angle = Main.rand.NextDouble() * Math.PI * 2;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
            float maxDist = Single.MaxValue;
            int res = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && !n.immortal && n.life > 5)
                {
                    float num3 = Vector2.DistanceSquared(npc.Center, n.Center);
                    if (num3 < maxDist)
                    {
                        maxDist = num3;
                        res = i;
                    }
                }
            }
            if (res >= 0 && res < Main.npc.Length)
            {

                Vector2 vel = npc.Center - Main.npc[res].Center;
                vel.Normalize();
                vel *= 5;
                newPos = new Vector2(size, 0);
                newPos.RotatedBy(vel.ToRotation());
                newPos += new Vector2(npc.Center.X, npc.Center.Y);

                int p = Projectile.NewProjectile(newPos, vel, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
        }

        private void spawnDust(Player player, Entity npc)
        {
            int max = Main.rand.Next(2, 4);
            for (int i = 0; i < max; i++)
            {
                int dust = 174;


                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                size += Main.rand.Next(0, 64);
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int d = Dust.NewDust(newPos, 8, 8, dust);
                if (d >= 0 && d < Main.dust.Length)
                {
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = Main.rand.NextFloat() + 0.5f;
                    Main.dust[d].rotation = (float)(Main.rand.NextDouble() * Math.PI * 2);
                }
            }
        }
    }
}