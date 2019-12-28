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
    public class FrostfireApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Fire Apprentice Bait");
            Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait, 50);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }

    public class FrostfireBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Fire Bait");
            Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait, 25);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }
    }

    public class FrostfireMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frost Fire Master Bait");
            Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            BaitRecipe recipe = new BaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait, 10);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
