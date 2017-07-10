using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public abstract class Crate : ModItem
    {

        protected int LesserReplacement = ItemID.LesserHealingPotion;
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenCrate);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int id=0; int stack=0;
            if(Main.rand.Next(4) == 0)
            {
                spawnPotion(ref id, ref stack);
                player.QuickSpawnItem(id, stack);
            }
            if(Main.rand.Next(8) == 0)
            {
                spawnOres(ref id, ref stack);
                player.QuickSpawnItem(id, stack);
            }
            if(Main.hardMode && Main.rand.Next(8) == 0)
            {
                spawnHardmodeOres(ref id, ref stack);
                player.QuickSpawnItem(id, stack);
            }
            spawnHealthPotion(ref id, ref stack);
            if (id == ItemID.LesserHealingPotion && LesserReplacement > 0)
                id = LesserReplacement;
            player.QuickSpawnItem(id,stack);
            spawnCoins(ref id, ref stack);
            player.QuickSpawnItem(id, stack);
            spawnBait(ref id, ref stack);
            player.QuickSpawnItem(id, stack);
        }


        public static void spawnHealthPotion(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
                id = ItemID.LesserHealingPotion; stack = Main.rand.Next(3, 20);
            }
            else if (!NPC.downedPlantBoss)
            {
                if (Main.rand.Next(4) == 0)
                {
                    id = ItemID.GreaterHealingPotion; stack = Main.rand.Next(1, 8);
                }
                else
                {
                    id = ItemID.HealingPotion; stack = Main.rand.Next(3, 20);
                }
            }
            else
            {
                if (Main.rand.Next(4) == 0)
                {
                    id = ItemID.SuperHealingPotion; stack = Main.rand.Next(1, 8);
                }
                else
                {
                    id = ItemID.GreaterHealingPotion; stack = Main.rand.Next(3, 20);
                }
            }
        }

        public static void spawnCoins(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
               
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.GoldCoin; stack = Main.rand.Next(1, 5);
                } else {
                    id = ItemID.SilverCoin; stack = Main.rand.Next(30,91);
                }
            }
            else if (!NPC.downedPlantBoss)
            {
                id = ItemID.GoldCoin; stack = Main.rand.Next(3, 12);
            }
            else
            {
                id = ItemID.GoldCoin; stack = Main.rand.Next(5, 25);
            }
        }

        public static void spawnBait(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            { 
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.ApprenticeBait; stack = Main.rand.Next(5, 16);
                }
                else
                {
                    id = ItemID.JourneymanBait; stack = Main.rand.Next(2, 9);
                }
            }
            else if (!NPC.downedPlantBoss)
            {
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.JourneymanBait; stack = Main.rand.Next(5, 16);
                }
                else
                {
                    id = ItemID.MasterBait; stack = Main.rand.Next(2, 9);
                }
            }
            else
            {
                id = ItemID.MasterBait; stack =  Main.rand.Next(5, 16);
            }
        }
        public static void spawnPotion(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        id = ItemID.MiningPotion;
                        break;
                    case 1:
                        id = ItemID.IronskinPotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;
                        
                }
                stack = Main.rand.Next(2, 7);
            }
            else if (!NPC.downedPlantBoss)
            {
                switch (Main.rand.Next(11))
                {
                    case 0:
                        id = ItemID.MiningPotion;
                        break;
                    case 1:
                        id = ItemID.EndurancePotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    case 5:
                        id = ItemID.WrathPotion;
                        break;
                    case 6:
                        id = ItemID.RagePotion;
                        break;
                    case 7:
                        id = ItemID.GillsPotion;
                        break;
                    case 8:
                        id = ItemID.SwiftnessPotion;
                        break;
                    case 9:
                        id = ItemID.HeartreachPotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;

                }
                stack = Main.rand.Next(2, 7);
            }
            else
            {
                switch (Main.rand.Next(11))
                {
                    case 0:
                        id = ItemID.InfernoPotion;
                        break;
                    case 1:
                        id = ItemID.EndurancePotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    case 5:
                        id = ItemID.WrathPotion;
                        break;
                    case 6:
                        id = ItemID.RagePotion;
                        break;
                    case 7:
                        id = ItemID.GillsPotion;
                        break;
                    case 8:
                        id = ItemID.BattlePotion;
                        break;
                    case 9:
                        id = ItemID.HeartreachPotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;

                }
                stack = Main.rand.Next(3, 12);
            }
        }

        public static void spawnOres(ref int id, ref int stack)
        {
            switch (Main.rand.Next(8))
            {
                case 0:
                    id = ItemID.CopperOre;
                    break;
                case 1:
                    id = ItemID.TinOre;
                    break;
                case 2:
                    id = ItemID.IronOre;
                    break;
                case 3:
                    id = ItemID.LeadOre;
                    break;
                case 4:
                    id = ItemID.SilverOre;
                    break;
                case 5:
                    id = ItemID.TungstenOre;
                    break;
                case 6:
                    id = ItemID.GoldOre;
                    break;
                default:
                    id = ItemID.PlatinumOre;
                    break;
            }
            stack = Main.rand.Next(12, 33);
        }

        public static void spawnHardmodeOres(ref int id, ref int stack)
        {
            switch (Main.rand.Next(6))
            {
                case 0:
                    id = ItemID.CobaltOre;
                    break;
                case 1:
                    id = ItemID.PalladiumOre;
                    break;
                case 2:
                    id = ItemID.MythrilOre;
                    break;
                case 3:
                    id = ItemID.OrichalcumOre;
                    break;
                case 4:
                    id = ItemID.AdamantiteOre;
                    break;
                default:
                    id = ItemID.TitaniumOre;
                    break;
            }
            stack = Main.rand.Next(12, 33);
        }

    }
}
