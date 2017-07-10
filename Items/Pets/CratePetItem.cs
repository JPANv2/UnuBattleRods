using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Pets
{
    public class CratePetItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crate Charm");
            Tooltip.SetDefault("Summons a Crate Pet that greatly helps you fish crates.");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.CrimsonHeart);
            item.shoot = mod.ProjectileType("CratePetProjectile");
            item.buffType = mod.BuffType("CratePetBuff");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MimicCrate", 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }

}

