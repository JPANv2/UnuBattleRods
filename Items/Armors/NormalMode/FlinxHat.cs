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
    public class FlinxHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flinx Hat");
            Tooltip.SetDefault("Increases Fishing Skill by 3\nIncreases Bob Speed and Damage by 3%\nMade out of real Flinx!");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 2;
            item.defense = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.03f;
            pl.bobberDamage += 0.03f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
            //drawAltHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("SnowSlothVest") && legs.type == mod.ItemType("SnowSlothPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Immunity to Chilled and Frozen debuffs." + "\nIncreased Health Regen and defense while on the Snow and Glowing Mushroom biomes.";
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            if(player.ZoneGlowshroom || player.ZoneSnow)
            {
                if (Main.hardMode)
                {
                    player.lifeRegen += 10;
                    player.statDefense += 20;
                }else
                {
                    player.lifeRegen += 2;
                    player.statDefense += 4;
                }
            }
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FlinxFur", 2);
            recipe.AddIngredient(mod, "FungalSpores", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    
    }
}
