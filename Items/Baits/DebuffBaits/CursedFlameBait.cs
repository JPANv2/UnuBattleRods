using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class CursedFlameApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cursed Flame Apprentice Bait");
            Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 15);
            recipe.AddRecipe();
        }

    }

    public class CursedFlameBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cursed Flame Bait");
            Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

    public class CursedFlameMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cursed Flame Master Bait");
            Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
