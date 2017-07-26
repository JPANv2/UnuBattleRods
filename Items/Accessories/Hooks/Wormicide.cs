using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRods;

namespace UnuBattleRods.Items.Accessories.Hooks
{
    public class Wormicide : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormicide");
            Tooltip.SetDefault("Doubles bobber damage when stuck on a worm-like enemy.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,0,25,0);
            item.rare = 3;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ItemID.FlaskofPoison, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateEquip(Player player)
        {
			player.GetModPlayer<FishPlayer>(mod).wormicide = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

        /*public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }*/

       /* public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ExtraLine", "+1 Fishing Line");
            line.overrideColor = Color.LimeGreen;
            tooltips.Add(line);
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.LimeGreen;
                }
            }
        }*/

    }
}
