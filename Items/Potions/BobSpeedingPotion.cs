using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Potions
{
    class BobSpeedingPotion: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bob Speeding Potion");
            Tooltip.SetDefault("Increases your bob speed by 25%");
        }

        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.buffType = mod.BuffType("BobTimeReduction");
            item.buffTime = 28800;

            item.width = 24;
            item.height = 24;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            //item.accessory = true;
            //item.vanity = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Waterleaf,2);
            recipe.AddIngredient(ItemID.PalmWood,5);
            recipe.AddIngredient(ItemID.Coral);
            recipe.AddTile(13);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}