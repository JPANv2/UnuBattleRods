using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Projectiles.Minions
{
    public class Buddyfish : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // Main.projPet[projectile.type] = true; // for some reason, with this line, the damage code no longer works...
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Raven);
            projectile.netImportant = true;
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.minionSlots = 0;
            projectile.npcProj = false;
            projectile.trap = false;
            aiType = ProjectileID.Raven;
        }

        private int oldType;
        public override bool PreAI()
        {
            oldType = projectile.type;
            projectile.type = ProjectileID.Raven;
            if (!Main.player[projectile.owner].GetModPlayer<FishPlayer>().buddyfish)
                this.Kill(0);
            return base.PreAI();
        }

        public override void PostAI()
        {
            base.PostAI();
            projectile.type = oldType;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            FishPlayer pl = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
            PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
            if (pl.hasAnyBaitDebuffs())
            {
                target.AddBuff(pbdbf.Type, 120);
                FishGlobalNPC gnpc = target.GetGlobalNPC<FishGlobalNPC>();
                List<Player> players = new List<Player>();
                players.Add(Main.player[projectile.owner]);
                List<int> debuffs = new List<int>();
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(target, gnpc, debuffs);
            }
            base.OnHitNPC(target,damage,knockback, crit);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            FishPlayer pl = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
            PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
            if (pl.hasAnyBaitDebuffs())
            {
                target.AddBuff(pbdbf.Type, 120);
                FishPlayer tpl = target.GetModPlayer<FishPlayer>();
                List<Player> players = new List<Player>();
                players.Add(Main.player[projectile.owner]);
                List<int> debuffs = new List<int>();
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(tpl, debuffs);
            }
            base.OnHitPvp(target, damage, crit);
        }

    }
}
