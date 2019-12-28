using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using UnuBattleRods.Projectiles.Discardables;

namespace UnuBattleRods.Items.Discardables
{
    public class SandnadoBobbers : Discardable
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
            DisplayName.SetDefault("Sandnado Discardable Bobber");
            Tooltip.SetDefault("Will leave a Sandnado behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);

            discardableProjectileID = ProjectileID.SandnadoFriendly;
            damageMod = 1.0f;
            projectileDuration = 300;
        }
    }
}
