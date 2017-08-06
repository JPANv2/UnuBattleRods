using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Materials;

namespace UnuBattleRods.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Body)]
    public class StarmixVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Star Mix Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Fishing Damage by 5%\nGives double defense in Hardmode.");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 3;
            item.defense = 9;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.05f;
            if (Main.hardMode)
            {
                player.statDefense += item.defense;
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Bars", 25);
            recipe.AddIngredient(ItemID.IronBar, 30);
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Bars", 30);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 35);
            recipe.AddIngredient(mod.ItemType<StarMix>(), 5);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Body");
            recipe.AddRecipeGroup("UnuBattleRods:Tier1Body");
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Body");
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Body");
            recipe.AddIngredient(mod.ItemType<StarMix>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
