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
    public class BeerPack:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pack of Beer");
            Tooltip.SetDefault("Not so cold anymore.");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.shootSpeed = 6.5f;
            item.shoot = mod.ProjectileType<Beer>();
            item.width = 22;
            item.height = 22;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useAnimation = 25;
            item.useTime = 25;
            //this.noUseGraphic = true;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 1, 10, 0);
            item.damage = 35;
            item.knockBack = 7f;
            item.thrown = true;
            item.rare = 3;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(BuffID.Tipsy, 3600);
                return true;
            }else
            {
                return base.UseItem(player);
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(8) == 0)
            {
                Vector2 speed = new Vector2(speedX, speedY);
                speed = speed.RotatedBy(Math.PI / 32);
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
                speed = new Vector2(speedX, speedY);
                speed = speed.RotatedBy(-Math.PI / 32);
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
            }else if (Main.rand.Next(2) == 0)
            {
                Vector2 speed = new Vector2(speedX, speedY);
                speed = speed.RotatedBy(Math.PI / 32);
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
