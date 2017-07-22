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
    public class StrongerManaEscalationReel: ManaEscalationReel
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stronger Mana Escalation Reel");
            Tooltip.SetDefault("Increases bobber damage by 7% each second, but costs 16 mana per second.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ManaEscalationReel", 1);
            recipe.AddIngredient(mod, "EnergyAmalgamate", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>(mod);
            p.escalation = true;
            p.escalationManaCost += 16;
            p.escalationFromMana = true;
            p.escalationFromManaBonus = 0.07f;
            //p.escalationFromManaMax = 1.0f;
            
        }
    }
}
