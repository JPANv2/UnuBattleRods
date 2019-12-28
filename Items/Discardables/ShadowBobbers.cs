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
    public class ShadowBobbers : Discardable
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
            DisplayName.SetDefault("Shadow Blade Discardable Bobber");
            Tooltip.SetDefault("Will leave behind spinning blades when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 25);

            discardableProjectileID = ModContent.ProjectileType<DiscardableBlade>();
            damageMod = 0.5f;
            projectileDuration = 300;
        }
    }
}
