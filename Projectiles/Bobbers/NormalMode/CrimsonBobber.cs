using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.NormalMode
{
    public class CrimsonBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 120;
            timeReelMax = 40;
            sizeMultiplier = 2.5f;
            speedIncrease = 3.0f;
            vampiricPercent = 0.05f;
        }

        public override float TensileStrength()
        {
            return 65f;
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

        

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);   
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                int recover = (int)Math.Round(damage * 0.05f);
                Player player = Main.player[projectile.owner];
                if (player.statLifeMax2 > player.statLife)
                {
                    player.statLife += recover;
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    if (Main.netMode != 2)
                    {
                        player.HealEffect(recover);
                    }
                    else
                    {
                        /*
                        NetMessage.SendData(16, -1, player.whoAmI, "", player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                        NetMessage.SendData(81, -1, -1, "" + recover, (int)CombatText.HealLife.PackedValue,
                            player.Center.X + (float)Main.rand.Next(-(int)((double)player.width * 0.5), (int)((double)player.width * 0.5) + 1),
                            player.position.Y + player.height * 0.25f + (float)Main.rand.Next(-(int)((double)player.height * 0.5), (int)((double)player.height * 0.5) + 1),
                            0f, 0, 0, 0);*/

                        ModPacket pk = mod.GetPacket();
                        pk.Write((byte)UnuBattleRods.Message.HealEffect);
                        pk.Write((ushort)projectile.owner);
                        pk.Write(recover);
                        pk.Send();
                    }
                }
                /*
                if (Main.player[projectile.owner].lifeSteal >= 0)
                {
                    Main.player[projectile.owner].lifeSteal -= damage * 0.05f;
                    if (Main.netMode != 2)
                    {
                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, projectile.owner, (float)(projectile.owner), damage * 0.05f);
                        Main.projectile[p].netUpdate = true;
                        NetMessage.SendData(27, -1, -1, "", p, 0f, 0f, 0f, 0, 0, 0);
                    }
                }*/
            }
            base.OnHitPvp(target, damage, crit);
        }
    }
}