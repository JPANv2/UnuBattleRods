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
    public class SnowSlothPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snow Sloth Pants");
            Tooltip.SetDefault("Increases Fishing Skill by 3\nIncreases Bob Speed and Damage by 2%\nMade of real Flinx!");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 1;
            item.defense = 5;
            item.value = Item.sellPrice(0, 0, 85, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.02f;
            pl.bobberDamage += 0.02f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FlinxFur", 1);
            recipe.AddIngredient(mod, "FungalSpores", 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
