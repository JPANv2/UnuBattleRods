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
    public class SnowSlothVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snow Sloth Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 5\nIncreases Bob Speed and Damage by 3%\nMade of real Flinx!");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 1;
            item.defense = 6;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.03f;
            pl.bobberSpeed += 0.03f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FlinxFur", 2);
            recipe.AddIngredient(mod, "FungalSpores", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
