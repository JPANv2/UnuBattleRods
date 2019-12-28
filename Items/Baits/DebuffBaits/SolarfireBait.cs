using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using UnuBattleRods.Buffs;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{
    public class SolarfireApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Solar Fire Apprentice Bait");
            Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 25);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }

    }

    public class SolarfireBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Solar Fire Bait");
            Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 25);
            recipe.AddIngredient(ItemID.FragmentSolar,2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }
    }

    public class SolarfireMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Solar Fire Master Bait");
            Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
