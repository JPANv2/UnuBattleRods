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
    public class HyperSlowMetronome : Metronome
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyper Slow Metronome");
            Tooltip.SetDefault("Increases fishing damage by 30%, but decreases bob speed by 20%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 3, 00, 0);
            item.rare = 6;
            bobberDamage = 0.30f;
            bobberSpeed = -0.20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(mod,"SuperSlowMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            
        }
    }
}
