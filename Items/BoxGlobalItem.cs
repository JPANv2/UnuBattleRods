using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Rods;

namespace UnuBattleRods.Items
{
    public class BoxGlobalItem : GlobalItem
    {

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.fishingPole > 0 && (item.modItem == null || !(item.modItem is BattleRod)))
            {
                int lures = player.GetModPlayer<FishPlayer>(mod).multilineFishing;
                if (lures > 0)
                {
                    for (int i = 1; i < lures; i++)
                    {
                        Projectile.NewProjectile(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5), speedX + Main.rand.Next(lures) - (lures / 2), speedY + Main.rand.Next(lures) - (lures / 2), type, damage, knockBack, player.whoAmI);
                    }
                }
                return true;
            }
            else
            {
                return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "crate" && arg == ItemID.DungeonFishingCrate)
            {
                if (Main.rand.Next(6) == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("DungeonBattlerod"));
                }
                if (UnuBattleRods.thoriumPresent)
                {
                    if (Main.rand.Next(3) == 0) 
                    {
                        if (NPC.downedPlantBoss)
                        {
                            player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:DarkMatter"), Main.rand.Next(1, 4));
                        }
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        player.QuickSpawnItem(UnuBattleRods.getItemTypeFromTag("ThoriumMod:DarksteelCore"), Main.rand.Next(1, 4));
                    }
                }
            }

        }
    }
}
