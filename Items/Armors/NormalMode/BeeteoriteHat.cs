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
    public class BeeteoriteHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Beeteorite Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 5\nIncreases Bob Speed by 6%");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 3;
            item.defense = 7;
            item.value = Item.sellPrice(0, 0, 50, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.06f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            //drawHair = true;
            drawAltHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("BeeteoriteVest") && legs.type == mod.ItemType("BeeteoritePants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Spawns Fire Bees to help you";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.fireBees = true;
            pl.fireBeesCooldown = 300;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.BeeWax, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
