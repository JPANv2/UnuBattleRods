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
    public class StarMix: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Mix");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 0, 50);
            item.rare = 1;
            item.maxStack = 999;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);

            recipe.AddRecipe();
        }
    }
}
