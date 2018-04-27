using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class FrostburnApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Burn Apprentice Bait");
            Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.IceTorch);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

    public class FrostburnBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Burn Bait");
            Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait,2);
            recipe.AddIngredient(ItemID.IceTorch, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }

    public class FrostburnMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Burn Master Bait");
            Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.IceTorch, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
