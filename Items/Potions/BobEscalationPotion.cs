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
    class BobEscalationPotion: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bob Escalation Potion");
            Tooltip.SetDefault("Increase damage by 2% per second while the bobber is attatched to the same enemy.");
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
            item.buffType = mod.BuffType("BobEscalation");
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
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(ItemID.Daybloom);
            recipe.AddIngredient(ItemID.Tuna);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
