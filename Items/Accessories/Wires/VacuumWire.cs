using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Accessories.Wires
{
    public class VacuumWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vacuum Wire");
            Tooltip.SetDefault("Items in contact with the bobber will be picked up.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0,0,20,0);
            item.rare = 1;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            //sold by the fish lady  
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().bobbersCatchItems = true;
            
        }
    }
}
