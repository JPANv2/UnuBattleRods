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
    [AutoloadEquip(EquipType.Head)]
    public class VacationHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vacation Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 3");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 1;
            item.defense = 1;
            item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("VacationVest") && legs.type == mod.ItemType("VacationPants");
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
            //drawAltHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases Fishing Damage and Bob Speed by 5%";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.05f;
            pl.bobberSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddIngredient(ItemID.Wood, 2);
            recipe.anyWood = true;
            recipe.AddIngredient(ItemID.Cactus, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
