using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace UnuBattleRods.Items.Accessories.Lures
{
    public class TurretBobbers: SelectiveLure
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
            DisplayName.SetDefault("Turret Bobbers");
            Tooltip.SetDefault("Bobbers will not hook to any enemy.\n30% extra Fishing Damage.");
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           item.value = Item.sellPrice(0, 0, 15, 0);
           item.rare = 3;
           maxHooking = -1;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetModPlayer<FishPlayer>().bobberDamage += 0.3f;
        }

        public override void AddRecipes()
        {

        }
    }
}
