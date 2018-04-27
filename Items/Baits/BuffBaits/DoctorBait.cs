using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.BuffBaits
{
    public class DoctorBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Doctor Bait");
            Tooltip.SetDefault("Heals you just right.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.bait = 100;
            buffID = BuffID.Regeneration;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait,5);
            recipe.AddIngredient(ItemID.GoldDust, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this,2);
            recipe.AddRecipe();
        }
    }
}
