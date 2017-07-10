using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Metronomes
{
    public class FastMetronome : Metronome
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fast Metronome");
            Tooltip.SetDefault("Increases bob speed by 10%, but decreases fishing damage by 8%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0,0,80,0);
            item.rare = 2;
            bobberDamage = -0.08f;
            bobberSpeed = 0.10f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 10);
            recipe.AddTile(TileID.Tables);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "SlowMetronome");
            recipe.AddTile(TileID.Tables);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.Tables);
            recipe.SetResult(mod, "SlowMetronome");
            recipe.AddRecipe();

        }
    }
}
