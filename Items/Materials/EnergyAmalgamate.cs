using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Materials
{
    class EnergyAmalgamate : ModItem
    {

         public override void SetStaticDefaults()
         {
             DisplayName.SetDefault("Energy Amalgamate");
             Tooltip.SetDefault("Concentrated Souls");
         }


        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofFlight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = refItem.maxStack;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 10;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofLight, 25);
            recipe.AddIngredient(ItemID.SoulofNight, 25);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.Ectoplasm, 2);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
