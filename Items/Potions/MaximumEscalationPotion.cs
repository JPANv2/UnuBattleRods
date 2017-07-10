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
    class MaximumEscalationPotion: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maximum Escalation Potion");
            Tooltip.SetDefault("Increase damage by 8% per second while the bobber is attatched to the same enemy. Maximum of 100 times base damage.");
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
            item.buffType = mod.BuffType("MaximumEscalationBuff");
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
            recipe.AddIngredient(mod, "BobEscalationPotion");
            recipe.AddIngredient(mod, "FasterEscalationPotion");
            recipe.AddIngredient(mod, "FurtherEscalationPotion");
            recipe.AddIngredient(ItemID.GoldenCarp, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
