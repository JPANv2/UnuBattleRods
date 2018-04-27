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
    public class IceCreamer : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Ice Creamer");
            Tooltip.SetDefault("Shoots one of four different flavors!");
        }

        public override void SetDefaults()
        {
           item.damage = 22;
           item.crit = 0;
           item.ranged = true;
           item.width = 48;
           item.height = 36;
           item.useTime = 25;
           item.useAnimation = 25;
           item.autoReuse = true;
           item.useStyle = 5;
           item.noMelee = true;
           item.knockBack = 5f;
           item.value = Item.sellPrice(0, 1, 90, 0);
           item.rare = 3;
           item.UseSound = SoundID.Item11;
           item.shoot = mod.ProjectileType<ChocolateShot>();
           item.shootSpeed = 15f;
           item.useAmmo = AmmoID.Snowball;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2?(Vector2.Zero);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int[] types = new int[3];
            for (int i = 0; i < 3; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 1:
                        types[i] = mod.ProjectileType<StrawberryShot>();
                        break;
                    case 2:
                        types[i] = mod.ProjectileType<ChocolateShot>();
                        break;
                    case 3:
                        types[i] = mod.ProjectileType<MintShot>();
                        break;
                    default:
                        types[i] = mod.ProjectileType<VanillaShot>();
                        break;
                }
            }
            Vector2 speed = new Vector2(speedX, speedY);
            speed = speed.RotatedBy(Math.PI / 32);
            Projectile.NewProjectile(position, speed, types[0], damage, knockBack, player.whoAmI);
            speed = new Vector2(speedX, speedY);
            speed = speed.RotatedBy(-Math.PI / 32);
            Projectile.NewProjectile(position, speed, types[1], damage, knockBack, player.whoAmI);

            type = types[2];
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
