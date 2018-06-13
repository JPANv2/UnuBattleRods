using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Knives
{
    class WoodenFishingKnife : BaseFishingKnife
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
            DisplayName.SetDefault("Wooden Fishing Knife");
            Tooltip.SetDefault("Attacks enemies who are almost touching you, once every 2 seconds.\n"+
                               "15 base Damage.\n"+
                               "Weak knockback.\n"+
                               "Double damage to enemies stuck to your bobber.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.item.rare = 1;
            base.item.value = Item.sellPrice(0, 0, 25, 0);
            baseDamage = 15;
            baseKnockback = 2.0f;
            radius = 24.0f;
            cooldown = 120;
            buffID = -1;
        }
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.anyWood = true;
            recipe.AddIngredient(ItemID.IronBar, 18);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
