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
    [AutoloadEquip(EquipType.Body)]
    public class FractaliteVest: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fractalite Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 50\nIncreases Bob Speed and Fishing Damage by 20%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 10;
            item.defense = 38;
            item.value = Item.sellPrice(0, 25, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 50;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.2f;
            pl.bobberDamage += 0.2f;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 64);
            recipe.AddIngredient(ItemID.FragmentSolar, 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(ItemID.FragmentNebula, 20);
            recipe.AddIngredient(ItemID.FragmentStardust, 20);
            recipe.AddIngredient(mod.ItemType<FractaliteBar>(), 5); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemID.NebulaBreastplate);
            recipe.AddIngredient(ItemID.StardustBreastplate);
            recipe.AddIngredient(mod.ItemType<FractaliteBar>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
