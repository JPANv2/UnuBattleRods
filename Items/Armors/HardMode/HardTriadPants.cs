using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Materials;

namespace UnuBattleRods.Items.Armors.HardMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class HardTriadPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hard Triad Pants");
            Tooltip.SetDefault("Increases Fishing Skill by 13\nIncreases Bob Speed and Fishing Damage by 8%");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 5;
            item.defense = 18;
            item.value = Item.sellPrice(0, 2, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 13;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            pl.bobberDamage += 0.08f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 32);
            recipe.AddIngredient(ItemID.HallowedBar,18);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedGreaves);
            recipe.AddIngredient(ItemID.FrostLeggings);
            recipe.AddIngredient(ItemID.AncientBattleArmorPants);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
