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
    public class SuperFastMetronome : Metronome
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
            DisplayName.SetDefault("Super Fast Metronome");
            Tooltip.SetDefault("Increases bob speed by 20%, but decreases fishing damage by 18%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0,1,50,0);
            item.rare = 4;
            bobberDamage = -0.18f;
            bobberSpeed = 0.20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Bars", 15);
            recipe.AddIngredient(mod, "FastMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "SuperSlowMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(mod, "SuperSlowMetronome");
            recipe.AddRecipe();

        }
    }
}
