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
    public class LifeforceHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lifeforce Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 20\nIncreases Bob Speed and Fishing Damage by 12%");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 8;
            item.defense = 14;
            item.value = Item.sellPrice(0, 8, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 20;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
            pl.bobberDamage += 0.12f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.ShroomiteBar, 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(mod.ItemType<EnergyAmalgamate>(), 5); 
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:ChlorophyteHelmets");
            recipe.AddRecipeGroup("UnuBattleRods:ShroomiteHelmets");
            recipe.AddRecipeGroup("UnuBattleRods:SpectreHelmets");
            recipe.AddIngredient(mod.ItemType<EnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("LifeforceVest") && legs.type == mod.ItemType("LifeforcePants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 50 Max Health and Max Mana.\nNinja Dodge effect.\n5% chance to nullify a projectile.\nProduces a glowing light.\nTruffle Worms won't flee from you.";
            player.blackBelt = true;
            player.statManaMax2 += 50;
            player.statLifeMax2 += 50;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.lifeforceArmorEffect = true;
            pl.projectileDestroyPercentage += 500;
            Lighting.AddLight(player.Center, 0.4f, 0.6f, 1.0f);
        }
    }
}
