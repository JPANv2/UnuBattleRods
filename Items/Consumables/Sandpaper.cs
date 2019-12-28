using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Consumables
{
    class Sandpaper: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandpaper");
            Tooltip.SetDefault("Smooths things, including Bobbers, allowing them to mostly fall to the floor after killing or being jerked free from an enemy.");
        }

        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item1;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 99;
            item.consumable = true;
            item.buffType = mod.BuffType("SandpaperBuff");
            item.buffTime = 3600;

            item.width = 24;
            item.height = 24;
            item.value = Terraria.Item.buyPrice(0, 0, 25, 0);
            item.rare = 1;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood);
            recipe.anyWood=true;
            recipe.AddIngredient(ItemID.SandBlock,2);
            recipe.anySand = true;
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
