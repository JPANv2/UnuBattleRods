using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.NormalMode
{
    public class CoolerBobber : Bobber
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 60;
            timeReelMax = 40;
            sizeMultiplier = 5.0f;
            speedIncrease = 5.0f;
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
    }
}