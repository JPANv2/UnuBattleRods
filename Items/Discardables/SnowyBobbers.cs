using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using UnuBattleRods.Projectiles.Discardables;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Discardables
{
    public class SnowyBobbers : Discardable
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
            DisplayName.SetDefault("Snowy Discardable Bobber");
            Tooltip.SetDefault("Will leave a freezing core behind that will freeze enemies in place (except the one it's attatched to).");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 25);

            discardableProjectileID = ModContent.ProjectileType<DiscardableSnowball>();
            damageMod = 0;
            damageAdd = 60;
        }
    }
}
