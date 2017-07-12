using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;

namespace UnuBattleRods.Items.Accessories.Knives
{
    class BlazingFishingKnife : BaseFishingKnife
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Fishing Knife");
            Tooltip.SetDefault("Attacks enemies who are very near you, once every second.\n" +
                               "80 base Damage.\n" +
                               "Average knockback.\n" +
                               "Inflicts Solar Fire debuff.\n" +
                               "Double damage to enemies stuck to your bobber.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.item.rare = 6;
            base.item.value = Item.sellPrice(0, 5, 0, 0);

            baseDamage = 80;
            baseKnockback = 6.0f;
            radius = 32.0f;
            cooldown = 60;
            buffID = mod.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FishingKnife", 1);
            recipe.AddIngredient(mod, "EnergyAmalgamate", 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
