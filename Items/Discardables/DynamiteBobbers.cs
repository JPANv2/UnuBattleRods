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
    public class DynamiteBobbers : Discardable
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
            DisplayName.SetDefault("Dynamite Discardable Bobber");
            Tooltip.SetDefault("Will leave a Dynamite behind when breaking the line. Not tile friendly.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 20, 0);

            discardableProjectileID = ModContent.ProjectileType<DiscardableDynamite>();
            damageMod = 0;
            damageAdd = 125;
        }
    }
}
