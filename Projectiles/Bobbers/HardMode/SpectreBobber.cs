using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class SpectreBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 50;
            timeReelMax = 35;
            sizeMultiplier = 10.0f;
            speedIncrease = 5.5f;
            vampiricPercent = 0.05f;
            syphonPercent = 0.05f;
        }

        public override float TensileStrength()
        {
            return 1000f;
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
            Lighting.AddLight(projectile.Center, 0.0f, 0.5f, 1.0f);
            if (!isStuck() && projectile.wet && !projectile.honeyWet && !projectile.lavaWet && !hasSpheres())
            {
                spawnSpheres(Main.player[projectile.owner], projectile);                
            }
            base.PostAI();
        }

        private bool hasSpheres()
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == 254 && Main.projectile[i].owner == projectile.owner)
                {
                    return true;
                }
            }
            return false;
        }

        private void spawnSpheres(Player player, Entity npc)
        {
                int proj = 254;
                float kb = 0f;
                int dmg = projectile.damage / 5;

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


        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
        }
    }
}