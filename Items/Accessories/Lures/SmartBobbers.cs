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
    public class SmartBobbers: SelectiveLure
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
            DisplayName.SetDefault("Smart Bobbers");
            Tooltip.SetDefault("Bobbers will try to distribute themselves equaly on targeted enemies, on a two block range.");
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           item.value = Item.sellPrice(0, 1, 0, 0);
           item.rare = 6;
           maxHooking = 0;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetModPlayer<FishPlayer>().smartBobberDistribution = true;
        }

        public override void AddRecipes()
        {

        }
    }
}
