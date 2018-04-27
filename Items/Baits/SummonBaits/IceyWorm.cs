using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Items.Baits.SummonBaits
{
    public class IceyWorm : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Worm");
            Tooltip.SetDefault("I wonder what I could catch with this...");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Worm);
            item.maxStack = 99;
        }
        /*
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<CoolerBoss>());
        }

        public override bool UseItem(Player player)
        {
            if (CanUseItem(player))
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<CoolerBoss>());
                return true;
            }
            return false;
        }*/

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Worm);
            recipe.AddIngredient(ItemID.IceBlock, 50);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
