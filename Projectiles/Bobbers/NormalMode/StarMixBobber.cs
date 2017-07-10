using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Bobbers.NormalMode
{
    public class StarMixBobber : Bobber
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            timeBobMax = 90;
            timeReelMax = 40;
            sizeMultiplier = 2.5f;
            speedIncrease = 3.0f;
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

        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 1.0f, 1.0f, 1.0f);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.boss || (target.realLife >=0 && Main.npc[target.realLife].boss))
            {
                damage *= 2;
                crit |= Main.rand.Next(4) == 0;  
            }
            if(target.type == NPCID.Werewolf)
            {
                damage *= 10;
                crit |= Main.rand.Next(4) == 0;
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
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