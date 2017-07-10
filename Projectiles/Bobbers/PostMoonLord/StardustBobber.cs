using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.PostMoonLord
{
    public class StardustBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 40;
            timeReelMax = 20;
            sizeMultiplier = 12.0f;
            speedIncrease = 4.0f;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 0.0f, 0.5f, 1.0f);
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