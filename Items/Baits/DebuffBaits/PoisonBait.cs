using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class PoisonApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poison Apprentice Bait");
            Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.Stinger);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

    public class PoisonBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poison Bait");
            Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait,2);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }

    public class PoisonMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poison Master Bait");
            Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
