using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class BeetleBobber : Bobber
    {
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 40;
            timeReelMax = 20;
            sizeMultiplier = 7.0f;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(180, 209, 215, 100));
        }

        int counter = 0;
        public override void PostAI()
        {
            if (npcIndex == -1 && (projectile.honeyWet || !projectile.lavaWet)) { 
                if (timeSinceLastBob <= 0)
                {
                    counter++;
                    if (counter >= 3)
                    {
                        spawnBeetles(Main.player[projectile.owner], projectile);
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
                    spawnBeetles(Main.player[projectile.owner], projectile);
                    counter = 0;
                }
               // AddBeetleBuffs(player);
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
                    spawnBeetles(player, target);
                }
               // AddBeetleBuffs(player);
            }
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnBeetles(Player player, Entity npc)
        {
           int max = Main.rand.Next(1, 4);
            for (int i = 0; i < max; i++)
            {
                int proj = mod.ProjectileType("Beetle");
                float kb = 4.0f;
                int dmg = (int)(projectile.damage * 1.5f);

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

        private void AddBeetleBuffs(Player player)
        {
            bool done = false;
            for (int i = 0; i < 22 && !done; i++)
            {
                if (player.buffType[i] == BuffID.BeetleEndurance1)
                {
                    player.buffTime[i] = bobTime() + 1;
                    player.buffType[i] = BuffID.BeetleEndurance2;
                    done = true;
                }
                else if (player.buffType[i] == BuffID.BeetleEndurance2)
                {
                    player.buffTime[i] = bobTime() + 1;
                    player.buffType[i] = BuffID.BeetleEndurance3;
                    done = true;
                }
                else if (player.buffType[i] == BuffID.BeetleEndurance3)
                {
                    player.buffTime[i] = bobTime() + 1;
                    done = true;
                }
                else if (player.buffType[i] == BuffID.BeetleMight1)
                {
                    player.buffTime[i] = bobTime() + 1;
                    player.buffType[i] = BuffID.BeetleMight2;
                    done = true;
                }
                else if (player.buffType[i] == BuffID.BeetleMight2)
                {
                    player.buffTime[i] = bobTime() + 1;
                    player.buffType[i] = BuffID.BeetleMight3;
                    done = true;
                }
                else if (player.buffType[i] == BuffID.BeetleMight3)
                {
                    player.buffTime[i] = bobTime() + 1;
                    done = true;
                }
            }
            if (!done)
            {
                int buff = Main.rand.Next(2) == 0 ? BuffID.BeetleMight1 : BuffID.BeetleEndurance1;
                player.AddBuff(buff, bobTime() + 1);
                if(buff == BuffID.BeetleMight1)
                {
                    player.beetleBuff = true;
                    player.beetleOffense = true;
                }else
                {
                    player.beetleBuff = true;
                    player.beetleDefense = true;

                }
            }
            player.beetleOrbs = 3;
        }
    }
}