using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using UnuBattleRods.Items.Accessories.Wires;
using UnuBattleRods.Items.Discardables;
using UnuBattleRods.Items.ItemBoxes.Discardables;

namespace UnuBattleRods.NPCs.TownNPC
{
    [AutoloadHead]
    public class Fishylady : ModNPC
    {
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Fishy Lady");
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active)
                {
                    for (int j = 0; j < player.inventory.Length; j++)
                    {
                        if (player.inventory[j].type == mod.ItemType("FishSteaks"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(5))
            {
                case 0:
                    return "Carmine";
                case 1:
                    return "Catherine";
                case 2:
                    return "Borana";
                case 3:
                    return "Margareth";
                default:
                    return "Joyce";
            }
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public override string GetChat()
        {
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            if (angler >= 0 && Main.rand.Next(4) == 0)
            {
                return "I never liked that " + Main.npc[angler].GivenName + " fella.";
            }

            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Yes, I trade items for fish steaks.";
                case 1:
                    return "Coming to get rid of your useless fish?";
                default:
                    return "I realize the irony of a fishmonger requesting fish in exchange for other items. It should really be the other way around.";
            }
        }


        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            //TODO: Sell items for FishSteaks
            shop.item[nextSlot].SetDefaults(mod.ItemType("Buddylure"));
            shop.item[nextSlot].shopCustomPrice = new int?(100);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("BaitDisperser"));
            shop.item[nextSlot].shopCustomPrice = new int?(80);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<VacuumWire>());
            shop.item[nextSlot].shopCustomPrice = new int?(20);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("ApprenticeBaitBox"));
            shop.item[nextSlot].shopCustomPrice = new int?(1);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("JourneymanBaitBox"));
            shop.item[nextSlot].shopCustomPrice = new int?(5);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("MasterBaitBox"));
            shop.item[nextSlot].shopCustomPrice = new int?(10);
            shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
            nextSlot++;
            if (!Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SnowyBobbers>());
                shop.item[nextSlot].shopCustomPrice = new int?(1);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExplosiveBobbers>());
                shop.item[nextSlot].shopCustomPrice = new int?(3);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MolotovBobbers>());
                shop.item[nextSlot].shopCustomPrice = new int?(3);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DynamiteBobbers>());
                shop.item[nextSlot].shopCustomPrice = new int?(8);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SnowyBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(1);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExplosiveBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(3);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MolotovBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(3);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DynamiteBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(8);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
            }

            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScytheBobbers>());
                shop.item[nextSlot].shopCustomPrice = new int?(6);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;

                if (NPC.downedGoblins)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShadowBobbers>());
                    shop.item[nextSlot].shopCustomPrice = new int?(8);
                    shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                    nextSlot++;
                }

                if (NPC.downedMechBossAny)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SandnadoBobbers>());
                    shop.item[nextSlot].shopCustomPrice = new int?(12);
                    shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<NuclearBobbers>());
                    shop.item[nextSlot].shopCustomPrice = new int?(12);
                    shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                    nextSlot++;
                }
                if (DD2Event.DownedInvasionT3)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<BetsyBobbers>());
                    shop.item[nextSlot].shopCustomPrice = new int?(6);
                    shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                    nextSlot++;
                }

            }
            else if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScytheBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(6);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShadowBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(8);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SandnadoBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(12);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<NuclearBobbersBox>());
                shop.item[nextSlot].shopCustomPrice = new int?(12);
                shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                nextSlot++;
                if (DD2Event.DownedInvasionT3)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<BetsyBobbersBox>());
                    shop.item[nextSlot].shopCustomPrice = new int?(6);
                    shop.item[nextSlot].shopSpecialCurrency = UnuBattleRods.fishSteaksCurrencyID;
                    nextSlot++;
                }
            }
            

        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 80;
            knockback = 8f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Main.rand.Next(2) == 0? mod.ProjectileType("FishyladyCleaver") : mod.ProjectileType("FishyladyKnife");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}

