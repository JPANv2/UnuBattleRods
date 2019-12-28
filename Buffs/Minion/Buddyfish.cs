using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRods.Items.Minions;

namespace UnuBattleRods.Buffs.Minion
{
    public class Buddyfish: ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Buddy Fish");
            Description.SetDefault("This pair will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer modPlayer = player.GetModPlayer<FishPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("Buddyfish")] > 0)
            {
                Buddylure buddyLure = ModContent.GetInstance<Buddylure>();
                int damage = 20;
                buddyLure.GetRealWeaponDamage(player, ref damage);
                if (player.ownedProjectileCounts[mod.ProjectileType("Buddyfish")] < 2)
                {
                    int proj = Projectile.NewProjectile(player.position, Vector2.One, mod.ProjectileType("Buddyfish"), damage, 1);
                    Main.projectile[proj].owner = player.whoAmI;
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("Buddyfish")] > 2)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].owner == player.whoAmI)
                        {
                            if (Main.projectile[i].modProjectile as Projectiles.Minions.Buddyfish != null)
                            {
                                Main.projectile[i].timeLeft = 0;
                                Main.projectile[i].Kill();
                            }
                        }
                    }
                }
                modPlayer.buddyfish = true;
            }
            
            if (!modPlayer.buddyfish)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
