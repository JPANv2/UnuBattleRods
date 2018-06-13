using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Metronomes
{
    public class SlowMetronome : Metronome
    {
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slow Metronome");
            Tooltip.SetDefault("Increases fishing damage by 10%, but decreases bob speed by 5%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();       
            item.value = Item.sellPrice(0,0,80,0);
            item.rare = 2;
            bobberDamage = 0.10f;
            bobberSpeed = -0.05f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 10);
            recipe.AddTile(TileID.Tables);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
