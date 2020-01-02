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
     public class Sinker: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sinker");
            Tooltip.SetDefault("Makes bobbers Sink into water when cast. Does not allow for Fishing.\nAlso, 5% Bob speed and Fishing damage increase if the player is underwater.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,0, 50, 0);
            item.rare = 2;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.anyIronBar = true;
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sinkBobber = true;
            if (player.wet)
            {
                player.GetModPlayer<FishPlayer>().bobberDamage += 0.05f;
                player.GetModPlayer<FishPlayer>().bobberSpeed += 0.05f;
            }
        }
    }
}
