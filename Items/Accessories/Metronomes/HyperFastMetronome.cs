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
    public class HyperFastMetronome : Metronome
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
            DisplayName.SetDefault("Hyper Fast Metronome");
            Tooltip.SetDefault("Increases bob speed by 30%, but decreases fishing damage by 26%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0,3,00,0);
            item.rare = 6;
            bobberDamage = -0.26f;
            bobberSpeed = 0.30f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(mod,"SuperFastMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "HyperSlowMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(mod, "HyperSlowMetronome");
            recipe.AddRecipe();

        }
    }
}
