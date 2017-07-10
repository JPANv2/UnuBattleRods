using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Crates;

namespace UnuBattleRods.NPCs
{
    public class CrateMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crate Mimic");
        }

        public override void SetDefaults()
        {
         
            base.npc.width = 24;
            base.npc.height = 24;
            
            base.npc.rarity = 2;
            base.npc.HitSound = SoundID.NPCHit3;
            base.npc.DeathSound = SoundID.NPCDeath6;
           
           
            this.banner = 16;
            this.bannerItem = ItemID.MimicBanner;
            base.npc.aiStyle = 25;
          
            Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[85];
            this.animationType = 85;

            if(Main.rand == null)
            {
                Main.rand = new Terraria.Utilities.UnifiedRandom();
            }
            if (!Main.hardMode)
            {
                base.npc.damage = 25;
                base.npc.defense = 10;
                base.npc.lifeMax = 300;
                base.npc.value = Main.rand.Next(5000, 30000);
                base.npc.knockBackResist = 0.1f;
            }
            else if (!NPC.downedPlantBoss)
            {
                base.npc.damage = 55;
                base.npc.defense = 35;
                base.npc.lifeMax = 400;
                base.npc.value = Main.rand.Next(10000, 50000);
                base.npc.knockBackResist = 0.1f;
            }
            else
            {
                base.npc.buffImmune[24] = true;
                base.npc.damage = 70;
                base.npc.defense = 45;
                base.npc.lifeMax = 600;
                base.npc.value = Main.rand.Next(30000, 80000);
                base.npc.knockBackResist = 0.1f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (base.npc.life <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 155, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                }
                for (int j = 0; j < 12; j++)
                {
                    Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 6, 0f, -1f, 0, default(Color), 1.2f);
                }
                return;
            }
            int num = 0;
            while ((double)num < damage / (double)base.npc.lifeMax * 50.0)
            {
                Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 155, (float)hitDirection, -1f, 0, default(Color), 1f);
                num++;
            }
        }

        public override void NPCLoot()
        {
            int id = 0; int stack = 0;

            Crate.spawnBait(ref id, ref stack);
            Item.NewItem(npc.Center, Vector2.Zero, id, stack);
            Crate.spawnHealthPotion(ref id, ref stack);
            Item.NewItem(npc.Center, Vector2.Zero, id, stack);

            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.Sextant);
                        break;
                    case 1:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.FishermansGuide);
                        break;
                    case 2:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.WeatherRadio);
                        break;
                    case 3:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.DepthMeter);
                        break;
                    default:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.Compass);
                        break;
                }
            }
            else if(Main.rand.Next(5) == 0)
            {
                if (!Main.hardMode)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 1:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.HighTestFishingLine);
                            break;
                        case 2:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.AnglerEarring);
                            break;
                        default:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.TackleBox);
                            break;
                    }
                    
                }
                else
                {
                    switch (Main.rand.Next(6))
                    {
                        case 1:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.AnglerHat);
                            break;
                        case 2:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.AnglerPants);
                            break;
                        case 3:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.HighTestFishingLine);
                            break;
                        case 4:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.AnglerVest);
                            break;
                        case 5:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.AnglerEarring);
                            break;
                        default:
                            Item.NewItem(npc.Center, Vector2.Zero, ItemID.TackleBox);
                            break;
                    }
                }

            }
            else
            {
                switch (Main.rand.Next(6))
                {
                    case 1:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.StarCloak);
                        break;
                    case 2:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.DualHook);
                        break;
                    case 3:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.MagicDagger);
                        break;
                    case 4:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.PhilosophersStone);
                        break;
                    case 5:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.CrossNecklace);
                        break;
                    default:
                        Item.NewItem(npc.Center, Vector2.Zero, ItemID.TitanGlove);
                        break;
                }
            }
            
        }
    }
}


