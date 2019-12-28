using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.BossBags;
using UnuBattleRods.Items.Materials;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.Items.Weapons.Cooler;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.NPCs
{
    public class CoolerBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Cooler");
            Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[473];
        }

        public override void SetDefaults()
        {
            base.npc.width = 28;
            base.npc.height = 44;
            base.npc.boss = true;
            base.npc.HitSound = SoundID.NPCHit4;
            base.npc.DeathSound = SoundID.NPCDeath6;
            base.npc.rarity = 5;
            base.npc.value = 30000f;
            base.npc.knockBackResist = 0.1f;
            base.npc.aiStyle = 87;
            base.npc.scale = 1.1f;
            this.animationType = 473;

            if (Main.hardMode)
            {
                base.npc.lifeMax = 12000;
                base.npc.damage = 150;
                base.npc.defense = 40;
            }
            else
            {
                base.npc.lifeMax = 6000;
                base.npc.damage = 60;
                base.npc.defense = 30;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
                return 0f;
        }

        public override void AI()
        {
            hitCounter++;
            if((base.npc.lifeMax/4)*quarter > npc.life)
            {
                quarter--;
                bool broke = false;
                for(int i = 0; i < Main.projectile.Length; i++)
                {
                    Bobber b = Main.projectile[i].modProjectile as Bobber;
                    if(b != null && b.npcIndex == npc.whoAmI)
                    {
                        b.breakFree();
                        broke = true;
                    }
                }
                if (broke)
                {
                    npc.BigMimicSpawnSmoke();
                }
            }
            if(hitCounter > 1200 && npc.ai[0] < 2f)
            {
                npc.ai[0] = 4f;
                npc.noTileCollide = true;
                npc.velocity.Y = -12f; //was -8
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
                npc.ai[3] = 0f;
                hitCounter = 0; 
            }
            if(npc.ai[0] == 4.1f)
            {
                if (!notSpawn)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 1:
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.CrimsonGoldfish);
                                break;
                            case 2:
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.CorruptGoldfish);
                                break;
                            default:
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Piranha);
                                break;
                        }
                    }
                    notSpawn = true;
                }
            }else
            {
                notSpawn = false;
            }

        }
        int hitCounter = 0;
        byte quarter = 4;
        Boolean notSpawn = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(quarter);
            writer.Write(notSpawn);
            writer.Write(hitCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            quarter = reader.ReadByte();
            notSpawn = reader.ReadBoolean();
            hitCounter = reader.ReadInt32();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            hitCounter = 0;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (base.npc.life <= 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 6, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 2.4f);
                }
                for (int j = 0; j < 8; j++)
                {
                    Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 55, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1.4f);
                }
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 55, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1.2f);
                }
                return;
            }
            int num = 0;
            while ((double)num < damage / (double)base.npc.lifeMax * 50.0)
            {
                Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 6, (float)hitDirection, -1f, 0, default(Color), 0.9f);
                num++;
            }
        }

        public override void NPCLoot()
        {
            FishWorld.downedCooler = true;
            if (!Main.expertMode)
            {
                Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ItemID.Hook, Main.rand.Next(1, 4), false, 0, false, false);
                if (Main.hardMode)
                {
                    if (doesItDropCertificate())
                    {
                        Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<MasterBaiterCertificate>(), 1, false, 0, false, false);
                    }
                }
                if(Main.rand.Next(2) == 0)
                    Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CoolerBattlerod>(), 1, false, 0, false, false);
                else
                {
                    switch (Main.rand.Next(4))
                    {
                        case 1:
                            Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Melonbrand>(), 1, false, 0, false, false);
                            break;
                        case 2:
                            Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<MagicSoda>(), 1, false, 0, false, false);
                            break;
                        case 3:
                            Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<BeerPack>(), 1, false, 0, false, false);
                            break;
                        default:
                            Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<IceCreamer>(), 1, false, 0, false, false);
                            break;
                    }
                }

            
            }else
            {
                Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CoolerBossBag>(), 1, false, 0, false, false);
            }
        }

        public static bool doesItDropCertificate()
        {
            foreach (Player p in Main.player)
            {
                if (p.active && !p.GetModPlayer<FishPlayer>().MasterBaiter)
                    return true;
            }
            return false;
        }
    }
}
