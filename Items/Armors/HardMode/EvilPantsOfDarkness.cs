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
    public class EvilPantsOfDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Evil Pants of Darkness");
            Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Bob Speed and Fishing Damage by 5%");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 5;
            item.defense = 15;
            item.value = Item.sellPrice(0, 1, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            pl.bobberDamage += 0.05f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Bars", 15);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Bars", 15);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 18);
            recipe.AddIngredient(ItemID.ShadowScale, 35);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Legs");
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Legs");
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Legs");
            recipe.AddIngredient(ItemID.ShadowScale, 35);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
