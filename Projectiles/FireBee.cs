using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Projectiles
{
    public class FireBee: ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GiantBee);
            projectile.width = 16;
            projectile.height= 16;
            Main.projFrames[projectile.type] = 4;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.oiled)
            {
                damage *= 2;
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
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
                List<int> debuffs = new List<int>(new int[] { BuffID.OnFire });
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(target, gnpc, debuffs);
            }
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
                List<int> debuffs = new List<int>(new int[] { BuffID.OnFire });
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(tpl, debuffs);
            }
        }
    }
}
