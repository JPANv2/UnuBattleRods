using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class OrichalcumBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 60;
            timeReelMax = 35;
            sizeMultiplier = 6.0f;
            speedIncrease = 3.5f;
        }

        public override float TensileStrength()
        {
            return 95f;
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

        public override void PostAI()
        {

            if (Main.rand.Next(30) == 0 && (projectile.wet ||projectile.honeyWet || isStuck()))
            {
                Dust.NewDust(projectile.Center - new Vector2(-64, -64), 128, 128, 166, 3f, -1f, 0, default(Color), 1f);
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
    }
}