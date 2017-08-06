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
    [AutoloadEquip(EquipType.Legs)]
    public class VacationPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vacation Pants");
            Tooltip.SetDefault("Increases Fishing Skill by 3");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 1;
            item.defense = 2;
            item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddIngredient(ItemID.Wood, 3);
            recipe.anyWood = true;
            recipe.AddIngredient(ItemID.Cactus, 3);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
