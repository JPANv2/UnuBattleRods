using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.HardMode
{
    public class FrostBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 90;
            timeReelMax = 35;
            sizeMultiplier = 8.0f;
            speedIncrease = 4.0f;
        }

        public override float TensileStrength()
        {
            return 100f;
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
            npc.AddBuff(mod.BuffType("Frostfire"), bobTime());
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            player.AddBuff(mod.BuffType("Frostfire"), bobTime());
            base.applyDamageAndDebuffs(target, player);
        }
    }
}