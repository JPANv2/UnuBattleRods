﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Accessories.Wires
{
    public class SyphoningWire: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Syphoning Wire");
            Tooltip.SetDefault("Provides 10% mana Syphon on damage dealt with the bobber.");
        }

        public override void SetDefaults()
        {           
            item.width = 16;
            item.height = 16;           
            item.value = Item.sellPrice(0,5,0,0);
            item.rare = 10;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HighTestFishingLine, 1);
            recipe.AddIngredient(mod, "EnergyAmalgamate", 1);
            recipe.AddIngredient(mod, "FractaliteBar", 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FishPlayer>().syphonLinePercent += 0.10f;  
        }
    }
}
