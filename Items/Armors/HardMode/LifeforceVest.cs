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
    public class LifeforceVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lifeforce Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 25\nIncreases Bob Speed and Fishing Damage by 16%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 8;
            item.defense = 28;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 25;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.16f;
            pl.bobberDamage += 0.16f;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 24);
            recipe.AddIngredient(ItemID.ShroomiteBar, 24);
            recipe.AddIngredient(ItemID.SpectreBar, 24);
            recipe.AddIngredient(mod.ItemType<EnergyAmalgamate>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophytePlateMail);
            recipe.AddIngredient(ItemID.ShroomiteBreastplate);
            recipe.AddIngredient(ItemID.SpectreRobe);
            recipe.AddIngredient(mod.ItemType<EnergyAmalgamate>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
