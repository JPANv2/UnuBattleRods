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
    public class MaximumManaEscalationReel: ManaEscalationReel
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maximum Mana Escalation Reel");
            Tooltip.SetDefault("Increases bobber damage by 12% each second, but costs 24 mana per second (maximum 300%).");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "StrongerManaEscalationReel", 1);
            recipe.AddIngredient(mod, "FractaliteBar", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>(mod);
            p.escalation = true;
            p.escalationManaCost += 24;
            p.escalationFromMana = true;
            p.escalationFromManaBonus = 0.12f;
            p.escalationFromManaMax = 3.0f;
            
        }
    }
}
