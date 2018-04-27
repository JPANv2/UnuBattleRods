using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Baits.DebuffBaits
{

    public class OilApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Oil Apprentice Bait");
            Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {
            OilBaitRecipe recipe = new OilBaitRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();

            OilNotBaitRecipe recipe2 = new OilNotBaitRecipe(mod);
            recipe2.AddIngredient(ItemID.ApprenticeBait,10);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(TileID.Bottles);
            recipe2.SetResult(this,10);
            recipe2.AddRecipe();
        }

    }

    public class OilBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Oil Bait");
            Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {
            OilBaitRecipe recipe = new OilBaitRecipe(mod);
            recipe.AddIngredient(ItemID.JourneymanBait);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            OilNotBaitRecipe recipe2 = new OilNotBaitRecipe(mod);
            recipe2.AddIngredient(ItemID.JourneymanBait, 4);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(TileID.Bottles);
            recipe2.SetResult(this,4);
            recipe2.AddRecipe();
        }
    }

    public class OilMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Oil Master Bait");
            Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {
            OilBaitRecipe recipe = new OilBaitRecipe(mod);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();

            OilNotBaitRecipe recipe2 = new OilNotBaitRecipe(mod);
            recipe2.AddIngredient(ItemID.MasterBait,2);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(TileID.Bottles);
            recipe2.SetResult(this,2);
            recipe2.AddRecipe();
        }
    }

    public class OilBaitRecipe : BaitRecipe
    {
        public OilBaitRecipe(Mod mod) : base(mod)
        {
        }

        public override bool RecipeAvailable()
        {
            if (Main.player[Main.myPlayer].setHuntressT2)
                return base.RecipeAvailable();
            else return false;
        }
    }

    public class OilNotBaitRecipe : BaitRecipe
    {
        public OilNotBaitRecipe(Mod mod) : base(mod)
        {
        }

        public override bool RecipeAvailable()
        {
            if (!Main.player[Main.myPlayer].setHuntressT2)
                return base.RecipeAvailable();
            else return false;
        }
    }
}
