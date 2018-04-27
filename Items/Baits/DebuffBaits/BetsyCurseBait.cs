using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class BetsyCurseApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Betsy's Curse Apprentice Bait");
            Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(mod, "BetsyScales");
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 15);
            recipe.AddRecipe();
        }

    }

    public class BetsyCurseBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Betsy's Curse Bait");
            Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(mod, "BetsyScales"); 
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

    public class BetsyCurseMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Betsy's Curse Master Bait");
            Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(mod, "BetsyScales"); 
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
