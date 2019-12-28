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
    [AutoloadEquip(EquipType.Head)]
    public class FractaliteHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fractalite Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 30\nIncreases Bob Speed and Fishing Damage by 15%");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 10;
            item.defense = 25;
            item.value = Item.sellPrice(0, 15, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 30;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.15f;
            pl.bobberDamage += 0.15f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
            //drawAltHair = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 32);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddIngredient(ItemID.FragmentNebula, 10);
            recipe.AddIngredient(ItemID.FragmentStardust, 10);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 3); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.SolarFlareHelmet);
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemID.NebulaHelmet);
            recipe.AddIngredient(ItemID.StardustHelmet);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("FractaliteVest") && legs.type == mod.ItemType("FractalitePants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 200 Max Health and Max Mana.\n10% chance to nullify a projectile.\nDoubles max flight time if you have wings. Otherwise, provides invisible wings.\nProduces a strong glow and night vision.";
            player.statManaMax2 += 200;
            player.statLifeMax2 += 200;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.fractaliteArmorEffect = true;
            pl.projectileDestroyPercentage += 1000;
            Lighting.AddLight(player.Center, 1f, 1f, 1.0f);
            player.nightVision = true;
        }
    }
}
