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
    [AutoloadEquip(EquipType.Legs)]
    public class StarmixPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Star Mix Pants");
            Tooltip.SetDefault("Increases Fishing Skill by 6\nIncreases Bob Speed by 5%\nGives double defense in Hardmode.");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 3;
            item.defense = 8;
            item.value = Item.sellPrice(0, 0, 40, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 6;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            if (Main.hardMode)
            {
                player.statDefense += item.defense;
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Bars", 20);
            recipe.AddIngredient(ItemID.IronBar, 25);
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Bars", 25);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 30);
            recipe.AddIngredient(mod.ItemType<StarMix>(), 4);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Legs");
            recipe.AddRecipeGroup("UnuBattleRods:Tier1Legs");
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Legs");
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Legs");
            recipe.AddIngredient(mod.ItemType<StarMix>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
