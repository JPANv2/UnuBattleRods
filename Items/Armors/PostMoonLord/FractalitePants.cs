using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Materials;

namespace UnuBattleRods.Items.Armors.PostMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    public class FractalitePants: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fractalite Pants");
            Tooltip.SetDefault("Increases Fishing Skill by 30\nIncreases Bob Speed and Fishing Damage by 10%\nIncreases movement speed by 35%");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 10;
            item.defense = 28;
            item.value = Item.sellPrice(0, 20, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 30;
            player.moveSpeed += 0.35f;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.1f;
            pl.bobberDamage += 0.1f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 48);
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient(mod.ItemType<FractaliteBar>(), 4); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarFlareLeggings);
            recipe.AddIngredient(ItemID.VortexLeggings);
            recipe.AddIngredient(ItemID.NebulaLeggings);
            recipe.AddIngredient(ItemID.StardustLeggings);
            recipe.AddIngredient(mod.ItemType<FractaliteBar>(), 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
