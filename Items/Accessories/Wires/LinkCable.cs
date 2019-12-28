using System;
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
    public class LinkCable: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Link Cable");
            Tooltip.SetDefault("Damage increased by ammount bobbers stuck to different enemies.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,1,0,0);
            item.rare = 2;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.AddRecipeGroup("UnuBattleRods:Tier3Bars", 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateEquip(Player player)
        {
            float ans = 0;
            int bobberCount = 0;
            List<int> uniqueStuck = new List<int>();
            for(int i = 0; i < Main.projectile.Length; i++)
            {
                if(Main.projectile[i].owner == player.whoAmI && Main.projectile[i].modProjectile != null && Main.projectile[i].modProjectile is Bobber)
                {
                    bobberCount++;
                    Bobber b = (Bobber)(Main.projectile[i].modProjectile);
                    if (!uniqueStuck.Contains(b.npcIndex))
                    {
                        uniqueStuck.Add(b.npcIndex);
                    }
                }
            }
            
            if(bobberCount <= 8)
            {
                ans = (uniqueStuck.Count - 1) * 0.05f;
            }
            else
            {
                ans = (uniqueStuck.Count - 1) * 0.02f;
            }

           

            player.GetModPlayer<FishPlayer>().bobberDamage += ans;
            
        }
    }
}
