using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace UnuBattleRods.Projectiles.Bobbers.PostMoonLord
{
    public class RodContainmentUnitBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 10;
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
            counter++;
            if (counter >= 30)
            {
                Lighting.AddLight(projectile.Center, 0.9f, 0.9f, 0.9f);

                int size = 128;
                Entity e = getStuckEntity();
                if (!isStuck())
                    spawnDust(Main.player[projectile.owner], projectile);

                size += e.width > e.height ? e.width : e.height;
                for (int i = 0; i < 200; i++) //Main.npc.Length
                {
                    NPC npc = Main.npc[i];
                    if (Vector2.Distance(npc.Center, projectile.Center) < size)
                    {
                        if (npc.active && !npc.immortal && !npc.dontTakeDamage &&
                         !(npc.friendly && !(npc.type == NPCID.Guide && Main.player[projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[projectile.owner].killClothier)) 
                         && npc.catchItem <= 0
                         )
                        {
                            npc.StrikeNPC(npc.boss ? projectile.damage : projectile.damage / 2, 10, npc.Center.X < projectile.Center.X ? -1 : 1);
                        }
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player p = Main.player[i];
                    if (Vector2.Distance(p.Center, projectile.Center) < size)
                    {
                        if (p.active && p.hostile && (p.team == 0 || p.team != Main.player[projectile.owner].team) && Vector2.Distance(p.Center, e.Center) < size && p.whoAmI != projectile.owner)
                        {
                            p.Hurt(PlayerDeathReason.ByProjectile(p.whoAmI, projectile.whoAmI), projectile.damage, p.Center.X < projectile.Center.X ? -1 : 1);
                        }
                    }
                }
                counter = 0;
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


        private void spawnDust(Player player, Entity npc)
        {
            int max = Main.rand.Next(2, 4);
            for (int i = 0; i < max; i++)
            {
                int dust = 110;


                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                size += Main.rand.Next(0, 128);
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