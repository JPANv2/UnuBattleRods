using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class BeeteoriteBobber : Bobber
    {
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 75;
            timeReelMax = 40;
            sizeMultiplier = 3.5f;
            speedIncrease = 3.0f;
        }

        public override float TensileStrength()
        {
            return 75f;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(180, 209, 215, 100));
        }
        int counter = 0;
        public override void PostAI()
        {
            if (npcIndex == -1 && !projectile.lavaWet) { 
                if (timeSinceLastBob <= 0)
                {
                    counter++;
                    if (counter >= 3)
                    {
                        spawnBees(Main.player[projectile.owner], projectile);
                        Main.player[projectile.owner].GetModPlayer<FishPlayer>().decreaseBaitTimer(6);
                        counter = 0;
                    }
                    timeSinceLastBob = bobTime();
                }
                timeSinceLastBob--;
            }
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (timeSinceLastBob <= 0)
            {
                counter++;
                if (counter >= 3)
                {
                    spawnBees(player, npc);
                    counter = 0;
                }
            }
            base.applyDamageAndDebuffs(npc, player);   
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            if (timeSinceLastBob <= 0)
            {
                counter++;
                if (counter >= 3)
                {
                    spawnBees(player, target);
                    counter = 0;
                }
            }
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnBees(Player player, Entity npc)
        {
            int max = Main.rand.Next(2, 5);
            for (int i = 0; i < max; i++)
            {
                int proj = mod.ProjectileType<FireBee>();
                float kb = player.beeKB(4.0f);
                int dmg = player.beeDamage(projectile.damage*2/max);

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