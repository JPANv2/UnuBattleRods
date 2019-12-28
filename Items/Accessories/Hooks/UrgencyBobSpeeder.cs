using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Accessories.Hooks
{
    public class UrgencyBobSpeeder: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Urgency Bob Speeder");
            Tooltip.SetDefault("The lower the health, the faster the bob moves (up to 75% faster).");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 5;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "StarMix",3);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().bobberSpeed += (1 - ((player.statLife*1.0f) / player.statLifeMax2))*0.75f;
               
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

    }
}
