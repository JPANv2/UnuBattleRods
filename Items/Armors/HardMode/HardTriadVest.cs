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
    [AutoloadEquip(EquipType.Body)]
    public class HardTriadVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hard Triad Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 15\nIncreases Bob Speed and Fishing Damage by 12%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 5;
            item.defense = 28;
            item.value = Item.sellPrice(0, 2, 75, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 15;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
            pl.bobberDamage += 0.12f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 40);
            recipe.AddIngredient(ItemID.HallowedBar,24);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedPlateMail);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.AncientBattleArmorShirt);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
