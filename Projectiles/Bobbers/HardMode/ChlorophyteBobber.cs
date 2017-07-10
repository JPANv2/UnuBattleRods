using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class ChlorophyteBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 60;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(50, 50, 50, 100));
        }

        public override void PostAI()
        {
            if(isStuck()|| ((projectile.wet || projectile.honeyWet) && !projectile.lavaWet))
            {
                if (timeSinceLastBob <= 0)
                {
                    sporeSac();
                    if (!isStuck())
                    {
                        timeSinceLastBob = bobTime();
                    }
                }
                if (!isStuck())
                {
                    timeSinceLastBob--;
                }
            }           
            base.PostAI();
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
        }


        private void sporeSac()
        {
            int damage = 70;
            float knockBack = 1.5f;
            if (Main.rand.Next(15) == 0)
            {
                int num = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == projectile.owner && (Main.projectile[i].type == 567 || Main.projectile[i].type == 568))
                    {
                        num++;
                    }
                }
                if (Main.rand.Next(15) >= num && num < 10)
                {
                    int num2 = 50;
                    int num3 = 24;
                    int num4 = 90;
                    for (int j = 0; j < num2; j++)
                    {
                        int num5 = Main.rand.Next(200 - j * 2, 200 + j * 2);
                        Vector2 center = projectile.Center;
                        center.X += (float)Main.rand.Next(-num5, num5 + 1);
                        center.Y += (float)Main.rand.Next(-num5, num5 + 1);
                        if (!Collision.SolidCollision(center, num3, num3) && !Collision.WetCollision(center, num3, num3))
                        {
                            center.X += (float)(num3 / 2);
                            center.Y += (float)(num3 / 2);
                            if (Collision.CanHit(new Vector2(projectile.Center.X, projectile.position.Y), 1, 1, center, 1, 1) || Collision.CanHit(new Vector2(projectile.Center.X, projectile.position.Y - 50f), 1, 1, center, 1, 1))
                            {
                                int num6 = (int)center.X / 16;
                                int num7 = (int)center.Y / 16;
                                bool flag = false;
                                if (Main.rand.Next(3) == 0 && Main.tile[num6, num7] != null && Main.tile[num6, num7].wall > 0)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    center.X -= (float)(num4 / 2);
                                    center.Y -= (float)(num4 / 2);
                                    if (Collision.SolidCollision(center, num4, num4))
                                    {
                                        center.X += (float)(num4 / 2);
                                        center.Y += (float)(num4 / 2);
                                        flag = true;
                                    }
                                }
                                if (flag)
                                {
                                    for (int k = 0; k < 1000; k++)
                                    {
                                        if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].aiStyle == 105 && (center - Main.projectile[k].Center).Length() < 48f)
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                    if (flag && Main.myPlayer == projectile.owner)
                                    {
                                        Projectile.NewProjectile(center.X, center.Y, 0f, 0f, 567 + Main.rand.Next(2), damage, knockBack, projectile.owner, 0f, 0f);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
}