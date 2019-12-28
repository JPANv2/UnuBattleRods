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
    public class WerewolfHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Werewolf Hat");
            Tooltip.SetDefault("Increases fishing skill, damage and bob speed.\nNight and Moon phases alters stats.\nMoon events too.\nWerewolf friendly.");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 7;
            item.rare = 3;
            item.defense = 6;
            item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            item.defense = 6;
            float statMultiplier = 1.0f;
            if (Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
            {
                statMultiplier = 5.0f;
            }else if (Main.bloodMoon)
            {
                if (Main.hardMode)
                {
                    statMultiplier = 2.0f;
                }else
                {
                    statMultiplier = 1.4f;
                }
            }else if (!Main.dayTime)
            {
                switch (Main.moonPhase)
                {
                    case 0:
                        statMultiplier = 1.2f;
                        break;
                    case 1:
                    case 7:
                        statMultiplier = 1.1f;
                        break;
                    case 3:
                    case 5:
                        statMultiplier = 0.9f;
                        break;
                    case 4:
                        statMultiplier = 0.8f;
                        break;
                    default: statMultiplier = 1.0f;
                        break;
                }
            }
            player.fishingSkill += (int)Math.Round(5*statMultiplier);
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f * statMultiplier;
            pl.bobberDamage += 0.05f * statMultiplier;
            item.defense = (int)Math.Round(item.defense * statMultiplier);
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
            //drawAltHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.item.type && body.type == mod.ItemType("WerewolfVest") && legs.type == mod.ItemType("WerewolfPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases Fishing Damage and Bob Speed by 5%. Werewolves like you.";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberDamage += 0.05f;
            pl.bobberSpeed += 0.05f;
            player.npcTypeNoAggro[NPCID.Werewolf] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:EvilDrop", 30);
            recipe.AddRecipeGroup("UnuBattleRods:EvilScale", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
