using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs.Minion;

namespace UnuBattleRods.Items.Minions
{
    public class Buddylure : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Buddy Lure");
            Tooltip.SetDefault("Summons two buddy fish to fight for you.");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.summon = true;
            item.mana = 1;
            item.width = 26;
            item.height = 28;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("Buddyfish");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("Buddyfish"); 
            item.buffTime = 3600;               
        }

        public virtual void GetRealWeaponDamage(Player player, ref int damage)
        {
            damage = NPC.downedMoonlord ? 85 : (Main.hardMode ? 50 : 20);
            damage = (int)Math.Round((double)damage * (player.GetModPlayer<FishPlayer>().bobberDamage)/ player.minionDamage);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            int idx = tooltips.FindIndex(x => x.Name == "Damage");
            if (idx >= 0)
            {
                tooltips.RemoveAt(idx);
                int dmg = 0;
                GetRealWeaponDamage(Main.player[Main.myPlayer], ref dmg);
                tooltips.Insert(idx, new TooltipLine(mod, "Damage", dmg + " Fishing damage"));
            }
        }
    }

}
