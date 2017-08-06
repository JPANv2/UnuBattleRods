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
    public class EvilVestOfDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Evil Vest of Darkness");
            Tooltip.SetDefault("Increases Fishing Skill by 10\nIncreases Bob Speed and Fishing Damage by 8%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 5;
            item.defense = 25;
            item.value = Item.sellPrice(0, 1, 75, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 10;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            pl.bobberDamage += 0.08f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 24);
            recipe.AddIngredient(ItemID.ShadowScale, 55);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier1Body");
            recipe.AddRecipeGroup("UnuBattleRods:HMTier2Body");
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Body");
            recipe.AddIngredient(ItemID.ShadowScale, 55);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
