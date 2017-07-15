using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Projectiles.Pets
{
    class CratePetProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crate Pet");
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CrimsonHeart);
            aiType = ProjectileID.CrimsonHeart;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.crimsonHeart = false;
            return true;
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if(player.FindBuffIndex(mod.BuffType("CratePetBuff")) < 0)
            {
                projectile.Kill();
                return;
            }
            projectile.timeLeft = 18000;
            player.GetModPlayer<FishPlayer>(mod).maxCrate = true;
            player.sonarPotion = true;
        }
    }
}
