using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class WerewolfPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Werewolf Pants");
            Tooltip.SetDefault("Increases fishing skill, damage and bob speed.\nNight and Moon phases alters stats.\nMoon events too.\nWerewolf friendly.");
        }

        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 9;
            item.rare = 1;
            item.defense = 6;
            item.value = Item.sellPrice(0, 0, 60, 0);
        }

        public override void UpdateEquip(Player player)
        {
            item.defense = 6;
            float statMultiplier = 1.0f;
            if (Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
            {
                statMultiplier = 5.0f;
            }
            else if (Main.bloodMoon)
            {
                if (Main.hardMode)
                {
                    statMultiplier = 2.0f;
                }
                else
                {
                    statMultiplier = 1.4f;
                }
            }
            else if (!Main.dayTime)
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
                    default:
                        statMultiplier = 1.0f;
                        break;
                }
            }
            player.fishingSkill += (int)Math.Round(4 * statMultiplier);
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.04f * statMultiplier;
            pl.bobberDamage += 0.04f * statMultiplier;
            item.defense = (int)Math.Round(item.defense * statMultiplier);
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("UnuBattleRods:EvilDrop", 40);
            recipe.AddRecipeGroup("UnuBattleRods:EvilScale", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
