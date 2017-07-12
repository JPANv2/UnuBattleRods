using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Knives
{
    class FishingKnife : BaseFishingKnife
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Professional Fishing Knife");
            Tooltip.SetDefault("Attacks enemies who are very near you, once every second.\n" +
                               "40 base Damage.\n" +
                               "Average knockback.\n" +
                               "Double damage to enemies stuck to your bobber.");
        }
        

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.item.rare = 4;
            base.item.value = Item.sellPrice(0, 1, 0, 0);
            baseDamage = 40;
            baseKnockback = 5.0f;
            radius = 32.0f;
            cooldown = 60;
            buffID = -1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "WoodenFishingKnife", 1);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Bars", 10);
            recipe.AddIngredient(mod, "StarMix", 6);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
