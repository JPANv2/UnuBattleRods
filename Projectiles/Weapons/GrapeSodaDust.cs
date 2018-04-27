using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Weapons
{
    public class GrapeSodaDust: ModDust
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            updateType = -1;
        }


        public override bool Update(Dust dust)
        {
            if (dust.fadeIn > 0f && dust.fadeIn < 100f)
            {
                dust.scale += 0.03f;
                if (dust.scale > dust.fadeIn)
                {
                    dust.fadeIn = 0f;
                }
            }
            if (dust.noGravity)
            {
                dust.velocity *= 0.92f;
                if (dust.fadeIn == 0f)
                {
                    dust.scale -= 0.04f;
                }
            }
            if (dust.position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
            {
                dust.active = false;
            }
            float num117 = 0.1f;
            if ((double)Dust.dCount == 0.5)
            {
                dust.scale -= 0.001f;
            }
            if ((double)Dust.dCount == 0.6)
            {
                dust.scale -= 0.0025f;
            }
            if ((double)Dust.dCount == 0.7)
            {
                dust.scale -= 0.005f;
            }
            if ((double)Dust.dCount == 0.8)
            {
                dust.scale -= 0.01f;
            }
            if ((double)Dust.dCount == 0.9)
            {
                dust.scale -= 0.02f;
            }
            if ((double)Dust.dCount == 0.5)
            {
                num117 = 0.11f;
            }
            if ((double)Dust.dCount == 0.6)
            {
                num117 = 0.13f;
            }
            if ((double)Dust.dCount == 0.7)
            {
                num117 = 0.16f;
            }
            if ((double)Dust.dCount == 0.8)
            {
                num117 = 0.22f;
            }
            if ((double)Dust.dCount == 0.9)
            {
                num117 = 0.25f;
            }
            if (dust.scale < num117)
            {
                dust.active = false;
            }
            return false;
        }

    }
}

