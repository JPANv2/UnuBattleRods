﻿using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using UnuBattleRods.Configs;
using Terraria.ModLoader.Config;
using System.Collections.Generic;

namespace UnuBattleRods.Items.Currency
{
        public class FishSteaks : ModItem
        {
            public override void SetStaticDefaults()
            {
            DisplayName.SetDefault("Fish Steaks");
            Tooltip.SetDefault("Made from Fish, used as Currency.");
            }

            public override void SetDefaults()
            {
                item.width = 20;
                item.height = 20;
                item.value = Item.sellPrice(0,0,1,0);
                item.rare = 1;
                item.maxStack = 999;
            }

            public override void AddRecipes()
            {
                Dictionary<ItemDefinition,int> fishRecipes = ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes;
                foreach (ItemDefinition itm in ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes.Keys)
                {
                    ModRecipe recipe = new ModRecipe(mod);
                    recipe.AddIngredient(itm.Type);
                    recipe.SetResult(this, fishRecipes[itm]);
                    recipe.AddRecipe();
                }
            }
        }
    }

    public class FishCurrency : CustomCurrencySingleCoin
    {
        public Color ExampleCustomCurrencyTextColor = Color.Coral;

        public FishCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        {
        }

        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color = ExampleCustomCurrencyTextColor * ((float)Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
                {
                    color.R,
                    color.G,
                    color.B,
                    Language.GetTextValue("LegacyTooltip.50"),
                    price,
                    "Fish Steaks"
                });
        }
    }