using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class MidasApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midas Apprentice Bait");
            Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 5);
            recipe.AddIngredient(ItemID.GoldDust);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }

    }

    public class MidasBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midas Bait");
            Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 5);
            recipe.AddIngredient(ItemID.GoldDust, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }

    public class MidasMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midas Master Bait");
            Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.GoldDust);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
