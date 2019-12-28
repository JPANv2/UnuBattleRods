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
    public class MolotovBobbers : Discardable
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
            DisplayName.SetDefault("Molotov Discardable Bobber");
            Tooltip.SetDefault("Will leave a fire behind when breaking the line that spawns fires.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 25);

            discardableProjectileID = ModContent.ProjectileType<DiscardableFireSpawner>();
            damageMod = 0.2f;
            projectileDuration = 306;       
        }
    }
}
