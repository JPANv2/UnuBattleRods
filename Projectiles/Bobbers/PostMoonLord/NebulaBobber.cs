using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.PostMoonLord
{
    public class NebulaBobber : Bobber
    {
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 60;
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

        int counter = 0;
        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.3f, 0.7f);

            if (projectile.honeyWet || (projectile.wet && !projectile.lavaWet)) { 
                
                if (timeSinceLastBob <= 0)
                {
                    counter++;
                    if (counter >= 8)
                    {
                        spawnNebulas(Main.player[projectile.owner], projectile);
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
                if (counter >= 8)
                {
                    spawnNebulas(player, npc);
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
                if (counter >= 8)
                {
                    spawnNebulas(player, target);
                    counter = 0;
                }
            }
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnNebulas(Player player, Entity npc)
        {
            int itm = 3453 + Main.rand.Next(3);
            double angle = Main.rand.NextDouble() * Math.PI * 2;
            Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
            int size = npc.width > npc.height ? npc.width : npc.height;
            newPos.X += (float)(Math.Cos(angle) * size);
            newPos.Y += (float)(Math.Sin(angle) * size);
            int i = Item.NewItem(newPos, Vector2.Zero, itm);
            if (i >= 0 && i < Main.item.Length)
            {
                Main.item[i].newAndShiny = true;
            }
            
        }
    }
}