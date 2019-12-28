using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using UnuBattleRods.Projectiles.Discardables;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Discardables
{
    public class ExplosiveBobbers : Discardable
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
            DisplayName.SetDefault("Explosive Discardable Bobber");
            Tooltip.SetDefault("Will leave a grenade behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 25);

            discardableProjectileID = ModContent.ProjectileType<DiscardableGrenade>();
            damageMod = 0;
            damageAdd = 30;
        }
    }
}
