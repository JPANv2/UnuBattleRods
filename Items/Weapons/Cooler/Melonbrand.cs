using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Weapons;

namespace UnuBattleRods.Items.Weapons.Cooler
{
    public class Melonbrand : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Melonbrand");
            base.Tooltip.SetDefault("Throws melon slices on swing.");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.knockBack = 7f;
            item.value = Item.sellPrice(0, 1, 10, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<MelonProjectile>();
            item.shootSpeed = 5.0f;
        }

    }
}
