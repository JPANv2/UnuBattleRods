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
    public class BeeteoriteVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Beeteorite Vest");
            Tooltip.SetDefault("Increases Fishing Skill by 5\nIncreases Fishing Damage by 8%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.rare = 3;
            item.defense = 9;
            item.value = Item.sellPrice(0, 1, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.08f;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddIngredient(ItemID.BeeWax, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
