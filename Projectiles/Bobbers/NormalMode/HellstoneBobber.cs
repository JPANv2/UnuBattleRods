using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.NormalMode
{
    public class HellstoneBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 120;
            timeReelMax = 35;
            sizeMultiplier = 4.0f;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(249, 218, 0, 100));
        }

        public override void PostAI()
        {

            Lighting.AddLight(projectile.Center, 1.0f, 1.0f, 0.0f);
            
            if (isStuck() || !(projectile.wet || projectile.lavaWet))
                return;

            if(timeSinceLastBob <= 0)
            {
                timeSinceLastBob = bobTime();
                int cnt = 0;
                int bobCnt = 0;
                for(int i = 0; i < Main.projectile.Length; i++)
                {
                    if(Main.projectile[i].active && Main.projectile[i].owner == projectile.owner)
                    {
                        if(Main.projectile[i].type == projectile.type)
                        {
                            bobCnt++;
                        }
                        if(Main.projectile[i].type == 15)
                        {
                            cnt++;
                        }
                    }
                }
                if ((cnt / bobCnt) >= 6)
                    return;

                double angle = Math.PI / 4 + (Main.rand.NextDouble()-0.5f)* Math.PI / 8;

                Projectile.NewProjectile(projectile.Center, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 15, 15, projectile.damage, 2, projectile.owner);
                Projectile.NewProjectile(projectile.Center, new Vector2((float)-Math.Cos(angle), (float)Math.Sin(angle)) * 15, 15, projectile.damage, 2, projectile.owner);
            }
            timeSinceLastBob--;
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);
            if (!npc.buffImmune[BuffID.OnFire])
            {
                npc.AddBuff(BuffID.OnFire, 60);
            }
            
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
            if (!player.buffImmune[BuffID.OnFire])
            {
                player.AddBuff(BuffID.OnFire, 60);
            }
        }
    }
}