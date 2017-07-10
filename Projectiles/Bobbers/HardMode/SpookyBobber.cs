using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class SpookyBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 120;
            timeReelMax = 40;
            sizeMultiplier = 2.0f;
            speedIncrease = 2.0f;
        }

        public override float TensileStrength()
        {
            return 50f;
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


        public override void PostAI()
        {
            if (!isStuck() && (projectile.honeyWet || (projectile.wet && !projectile.lavaWet)))
            {
                if (timeSinceLastBob <= 0)
                {
                    spawnBats(Main.player[projectile.owner], projectile);
                    timeSinceLastBob = bobTime();
                }
                timeSinceLastBob--;
            }
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (timeSinceLastBob <= 0)
            {
                spawnBats(player, npc);
            }
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            if (timeSinceLastBob <= 0)
            {
                spawnBats(player, target);
            }
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnBats(Player player, Entity npc)
        {
           int max = Main.rand.Next(1, 3);
                for (int i = 0; i < max; i++)
                {
                    int proj = ProjectileID.Bat;
                    float kb = 2.0f;
                    int dmg = projectile.damage;

                    double angle = Main.rand.NextDouble() * Math.PI * 2;
                    Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                    int size = npc.width > npc.height ? npc.width : npc.height;
                    newPos.X += (float)(Math.Cos(angle) * size);
                    newPos.Y += (float)(Math.Sin(angle) * size);
                    int p = Projectile.NewProjectile(newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                    if (p >= 0 && p < Main.projectile.Length)
                    {
                        Main.projectile[p].owner = player.whoAmI;
                    }
                }
            }
        }
}