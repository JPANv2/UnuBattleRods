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
    [AutoloadEquip(EquipType.Head)]
    public class HardTriadHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hard Triad Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 12\nIncreases Bob Speed and Fishing Damage by 8%");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 5;
            item.defense = 10;
            item.value = Item.sellPrice(0, 2, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 12;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            pl.bobberDamage += 0.08f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HMTier3Bars", 20);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:HallowedHelmets");
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.AncientBattleArmorHat);
            recipe.AddIngredient(mod.ItemType<LesserEnergyAmalgamate>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("HardTriadVest") && legs.type == mod.ItemType("HardTriadPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 10% Mana Shyphoning when hooked.";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.syphonLinePercent += 0.1f;
        }
    }
}
