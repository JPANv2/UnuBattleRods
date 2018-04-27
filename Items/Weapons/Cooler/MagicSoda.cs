using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Weapons;

namespace UnuBattleRods.Items.Weapons.Cooler
{
    public class MagicSoda: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Magic Soda");
            Tooltip.SetDefault("Shake and Spray! Damage usually decreases with distance.");
        }

        public override void SetDefaults()
        {
            item.damage = 26;
            item.magic = true;
            item.width = 48;
            item.height = 36;
            item.useTime = 3;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 1, 50, 0);
            item.rare = 3;
            item.noMelee = true;
            item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("GrapeSodaSpray");
            item.shootSpeed = 16.5f;
            item.mana = 2;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);
            speed = speed.RotatedBy((Main.rand.NextDouble()-0.5f) * (Math.PI / 12));
            return base.Shoot(player, ref position, ref speed.X, ref speed.Y, ref type, ref damage, ref knockBack);
        }
    }
}
