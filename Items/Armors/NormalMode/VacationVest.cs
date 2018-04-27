using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Body)]
    public class VacationVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vacation Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 5");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 1;
            item.defense = 3;
            item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Wood, 4);
            recipe.anyWood = true;
            recipe.AddIngredient(ItemID.Cactus, 4);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
