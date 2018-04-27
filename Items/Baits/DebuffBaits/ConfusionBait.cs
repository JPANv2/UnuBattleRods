using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class ConfusionApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Confusion Apprentice Bait");
            Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 5);
            recipe.AddIngredient(ItemID.Nanites);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

            recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 3);
            recipe.AddIngredient(mod,"FungalSpores");
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }

    }

    public class ConfusionBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Confusion Bait");
            Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 5);
            recipe.AddIngredient(ItemID.Nanites, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

            recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 3);
            recipe.AddIngredient(mod, "FungalSpores",2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }

    public class ConfusionMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Confusion Master Bait");
            Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.Nanites);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(mod, "FungalSpores",3);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
