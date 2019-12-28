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
    public class NuclearBobbers : Discardable
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
            DisplayName.SetDefault("Nuclear Discardable Bobber");
            Tooltip.SetDefault("Will leave an Inferno blast behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);

            discardableProjectileID = ModContent.ProjectileType<DiscardableNuke>();
            damageMod = 0;
            damageAdd = 300;
        }
    }
}
