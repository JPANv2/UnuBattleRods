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
    class FractaliteFishingKnife : BaseFishingKnife
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
            DisplayName.SetDefault("Fractalite Fishing Knife");
            Tooltip.SetDefault("Attacks enemies who are near you, twice per second.\n" +
                               "120 base Damage.\n" +
                               "Strong knockback.\n" +
                               "Inflicts Frost Fire debuff.\n" +
                               "Double damage to enemies stuck to your bobber.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.item.rare = 10;
            base.item.value = Item.sellPrice(0, 8, 0, 0);
            baseDamage = 120;
            baseKnockback = 9.0f;
            radius = 64.0f;
            cooldown = 30;
            buffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BlazingFishingKnife", 1);
            recipe.AddIngredient(mod, "FractaliteBar", 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
