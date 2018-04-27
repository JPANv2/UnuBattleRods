using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class ShadowflameApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shadowflame Apprentice Bait");
            Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(mod, "Shadowflame");
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 15);
            recipe.AddRecipe();
        }

    }

    public class ShadowflameBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shadowflame Bait");
            Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(mod, "Shadowflame"); 
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

    public class ShadowflameMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shadowflame Master Bait");
            Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(mod, "Shadowflame"); 
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
