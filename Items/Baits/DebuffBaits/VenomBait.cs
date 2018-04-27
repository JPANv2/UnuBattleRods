using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class VenomApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Venom Apprentice Bait");
            Tooltip.SetDefault("Venomous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Venom;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 5);
            recipe.AddIngredient(ItemID.VialofVenom);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }

    }

    public class VenomBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Venom Bait");
            Tooltip.SetDefault("Venomous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Venom;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait,5);
            recipe.AddIngredient(ItemID.VialofVenom,2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }

    public class VenomMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Venom Master Bait");
            Tooltip.SetDefault("Venomous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Venom;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.VialofVenom);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
