using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Other
{
    public class FasterFishingKit: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Faster Fishing Kit");
            Tooltip.SetDefault("Allows you to re-cast very fast if bobs are retrieved from an enemy (right-click with rod).\n" +
                "Tries to hit your bobber where your cursor is aiming.\n" +
                "Increases your casting speed by 30 %.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BobAccelerator");
            recipe.AddIngredient(mod, "BobScope");
            recipe.AddIngredient(mod, "DiscardableBobs");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>(mod);
            p.destroyBobber = true;
            p.aimBobber = true;
            p.bobberShootSpeed += 0.3f;
        }
    }
}
