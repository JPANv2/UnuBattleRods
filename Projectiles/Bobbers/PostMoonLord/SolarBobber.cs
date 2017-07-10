using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.PostMoonLord
{
    public class SolarBobber : Bobber
    {
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 30;
            timeReelMax = 20;
            sizeMultiplier = 12.5f;
            speedIncrease = 2.0f;
        }

        public override float TensileStrength()
        {
            return 550f;
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

        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 1.0f, 1.0f, 0.0f);

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
                if(p.active && p.hostile && (p.team == 0 || p.team != Main.player[projectile.owner].team) && Vector2.Distance(p.Center, e.Center) < size && p.whoAmI != projectile.owner)
                {
                    if(!p.buffImmune[mod.BuffType("Solarfire")])
                        p.AddBuff(mod.BuffType("Solarfire"), 120);
                }
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
                    Main.dust[d].rotation = (float)(Main.rand.NextDouble()*Math.PI*2);
                }
            }

        }
    }
}