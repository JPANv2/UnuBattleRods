using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Materials
{
    public class SeaweedStar: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seaweed Star");
            Tooltip.SetDefault("A fallen star, covered in damp (sea)weed.\nBurn off the weed on the Furnace.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
            item.maxStack = 999;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(this,1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(ItemID.FallenStar, 1);

            recipe.AddRecipe();
        }
    }
}
