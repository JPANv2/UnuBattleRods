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
    [AutoloadEquip(EquipType.Head)]
    public class StarmixHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Star Mix Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 6\nIncreases Bob Speed by 5%\nGives double defense in Hardmode.");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 3;
            item.defense = 8;
            item.value = Item.sellPrice(0, 0, 25, 0);
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("StarmixVest") && legs.type == mod.ItemType("StarmixPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases Fishing Damage and Bob Speed by 10%";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.1f;
            pl.bobberSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Bars", 15);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 25);
            recipe.AddIngredient(mod.ItemType<StarMix>(), 3);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:Tier0Helmets");
            recipe.AddRecipeGroup("UnuBattleRods:Tier1Helmets");
            recipe.AddRecipeGroup("UnuBattleRods:Tier2Helmets");
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Helmets");
            recipe.AddIngredient(mod.ItemType<StarMix>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
