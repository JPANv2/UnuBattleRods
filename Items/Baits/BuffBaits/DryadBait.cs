using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.BuffBaits
{
    public class DryadApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dryad Apprentice Bait");
            Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.PurificationPowder);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

    public class DryadBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dryad Bait");
            Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait);
            recipe.AddIngredient(ItemID.PurificationPowder, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class DryadMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dryad Master Bait");
            Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.PurificationPowder, 10);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
