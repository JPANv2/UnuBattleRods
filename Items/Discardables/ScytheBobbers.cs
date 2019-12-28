using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using UnuBattleRods.Projectiles.Discardables;

namespace UnuBattleRods.Items.Discardables
{
    public class ScytheBobbers : Discardable
    {
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Scythe Discardable Bobber");
            Tooltip.SetDefault("Will shoot two Demon Scythes when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 25);
        }

        public override void onDiscard(Player p, int damage, Entity target)
        {
            int projID = Projectile.NewProjectile(target.position, new Vector2(-1,0), ProjectileID.DemonScythe, (int)(damage*0.75f), 0, p.whoAmI);
            int projID2 = Projectile.NewProjectile(target.position, new Vector2(1, 0), ProjectileID.DemonScythe, (int)(damage * 0.75f), 0, p.whoAmI);
            if (projID >= 0 && projID < Main.projectile.Length)
            {
                Main.projectile[projID].Center = Vector2.Add(target.Center, new Vector2(-(target.width*0.5f + Main.projectile[projID].width *0.75f), 0));
                Main.projectile[projID].timeLeft = 300;
                Main.projectile[projID].tileCollide = false;
                Main.projectile[projID].netUpdate = true;
            }
            if (projID2 >= 0 && projID2 < Main.projectile.Length)
            {
                Main.projectile[projID2].Center = Vector2.Add(target.Center, new Vector2(target.width*0.5f + Main.projectile[projID].width * 0.75f, 0));
                Main.projectile[projID2].timeLeft = 300;
                Main.projectile[projID2].tileCollide = false;
                Main.projectile[projID2].netUpdate = true;
            }
        }
    }
}
