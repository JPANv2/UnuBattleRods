using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class FishronBobber : Bobber
    {
        byte counter = 0;
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 15;
            timeReelMax = 20;
            sizeMultiplier = 5.0f;
            speedIncrease = 3.0f;
        }

        public override float TensileStrength()
        {
            return 150f;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(204, 234, 153, 100));
        }

        public override void PostAI()
        {
            if (!isStuck() && (projectile.wet ||projectile.honeyWet) && !projectile.lavaWet)
            {
                if (timeSinceLastBob <= 0)
                {
                    counter++;
                    timeSinceLastBob = bobTime();
                }
                if (counter >= 6)
                {
                    counter = 0;
                    spawnBubbles(Main.player[projectile.owner], projectile);
                }
                timeSinceLastBob--;
            }
            else if (isStuck())
            {
                if(timeSinceLastBob == bobTime() - 1)
                {
                    counter++;
                    if (counter >= 6)
                    {
                        counter = 0;
                        spawnBubbles(Main.player[projectile.owner], getStuckEntity());
                    }
                }
            }
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnBubbles(Player player, Entity npc)
        {
            int max = Main.rand.Next(1, 4);
            for (int i = 0; i < max; i++)
            {
                int proj = ProjectileID.FlaironBubble;
                float kb = 2f;
                int dmg = projectile.damage*2;

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