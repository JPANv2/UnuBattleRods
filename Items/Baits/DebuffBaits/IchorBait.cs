using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class IchorApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Ichor Apprentice Bait");
            Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 15);
            recipe.AddRecipe();
        }

    }

    public class IchorBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Ichor Bait");
            Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

    public class IchorMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Ichor Master Bait");
            Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
