using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Capes
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class WormCape : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cape of Worms");
            Tooltip.SetDefault("Spawns worms as you walk. Truffle worms will not flee from you.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.height = 16;
            item.width = 16;
            item.accessory = true;
            item.rare = 12;
            item.value = Item.sellPrice(0,2,50,0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wormSpawner = true;
            player.GetModPlayer<FishPlayer>().lifeforceArmorEffect = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BlackThread, 1);
            recipe.AddIngredient(ItemID.Worm, 40);
            recipe.AddIngredient(ItemID.EnchantedNightcrawler, 10);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

    }
}

