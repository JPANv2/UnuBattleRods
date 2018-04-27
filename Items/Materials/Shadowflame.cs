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
    public class Shadowflame: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowflame");
            Tooltip.SetDefault("Purest flame from the Goblin Summoners");
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
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.FlyingKnife);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.ShadowFlameKnife, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.Hay, 50);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.ShadowFlameHexDoll, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.CobaltRepeater);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.ShadowFlameBow, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.PalladiumRepeater);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.ShadowFlameBow, 1);
            recipe.AddRecipe();
        }
    }
}
