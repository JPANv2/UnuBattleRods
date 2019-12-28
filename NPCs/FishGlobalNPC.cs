﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.NPCs
{
   public class FishGlobalNPC : GlobalNPC
    {

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool frostFire = false;
        public bool solarFire = false;

        public int isHooked = 0;
        public int isSealed = 0;

        public Vector2 newSpeed = Vector2.Zero;
        public Vector2 newCenter = new Vector2(-10000,-10000);

        public List<int> debuffsPresent = new List<int>();

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
            frostFire = false;
            solarFire = false;
        }

        public override void PostAI(NPC npc)
        {
            if(npc.type == NPCID.TruffleWorm && Main.netMode != 1)
            {
                bool allLifeforced = true;
                for (int num963 = 0; num963 < 255; num963 = num963 + 1)
                {
                    Player player = Main.player[num963];
                    if (player.active && !player.dead && Vector2.Distance(player.Center, npc.Center) <= 160f)
                    {
                        allLifeforced &= player.GetModPlayer<FishPlayer>().lifeforceArmorEffect;
                    }
                }
                if (allLifeforced)
                {
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                }
            }

            if(newCenter.X > -100 && newCenter.Y > -100)
            {
                if (WorldGen.InWorld((int)(newCenter.X / 16.0f), (int)(newCenter.Y / 16.0f)))
                { 
                    npc.Center = new Vector2(newCenter.X, newCenter.Y);
                    for(int i = 0; i <Main.projectile.Length; i++)
                    {
                        if(Main.projectile[i].modProjectile != null)
                        {
                            Bobber b = Main.projectile[i].modProjectile as Bobber;
                            if(b != null && b.npcIndex == npc.whoAmI)
                            {
                                b.projectile.Center = new Vector2(newCenter.X, newCenter.Y);
                            }
                        }
                    }
                }
                // npc.velocity = new Vector2(newSpeed.X, newSpeed.Y);
                newCenter = new Vector2(-10000, -10000);
            }
        }

       

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(projectile.modProjectile != null && (projectile.modProjectile is Bobber))
            {
                FishPlayer p = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                
                if (p != null && p.wormicide && Main.player[projectile.owner].active && !Main.player[projectile.owner].dead)
                {
                    if(npc.realLife >= 0 && npc.realLife != Main.wof)
                    {
                        damage *= 2;
                    }
                }
            }
            base.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if(isSealed > 0)
            {
                 damage = (int)(damage * 0.8f); 
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (frostFire)//npc.FindBuffIndex(mod.BuffType("Frostfire")) >= 0)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (20 +(npc.oiled?48:0));
                if (damage < 5 + (npc.oiled ? 3 : 0))
                {
                    damage = 5 + (npc.oiled ? 3 : 0);
                }
            }
            if (solarFire)//npc.FindBuffIndex(mod.BuffType("Solarfire")) >= 0)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (64 + (npc.oiled ? 128 : 0));
                if (damage < (6 + (npc.oiled ? 6:0)))
                {
                    damage = (6 + (npc.oiled ? 6 : 0));
                }
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.modProjectile != null && (projectile.modProjectile is Bobber))
            {
                FishPlayer p = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                if (p != null && Main.rand.Next(10000) < p.moneyPercent)
                {
                    int itmID = ItemID.CopperCoin;
                    int itmQuant = 1;
                    if (NPC.downedPlantBoss || Main.rand.Next(50) == 0)
                    {
                        itmID = ItemID.GoldCoin;
                        itmQuant = Main.rand.Next(20) + 1;
                    }else if (Main.hardMode || Main.rand.Next(15) == 0)
                    {
                        itmID = ItemID.SilverCoin;
                        itmQuant = Main.rand.Next(50) + 1;
                    }else
                    {
                        itmQuant = Main.rand.Next(30,90);
                    }

                    Item.NewItem(npc.Center, Vector2.Zero, itmID,itmQuant);
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (frostFire) {
                if (Main.rand.Next(4) < 3)
                {
                    int num52 = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 135, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Microsoft.Xna.Framework.Color), 3.5f);
                    Main.dust[num52].noGravity = true;
                    Dust dust = Main.dust[num52];
                    dust.velocity *= 1.8f;
                    Dust dust10 = Main.dust[num52];
                    dust10.velocity.Y = dust10.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[num52].noGravity = false;
                        dust = Main.dust[num52];
                        dust.scale *= 0.5f;
                    }
                }
                Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 0.3f, 0.6f, 1f);
            }
            if (solarFire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int num43 = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4,npc.height + 4, 6, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Microsoft.Xna.Framework.Color), 3.5f);
                    Main.dust[num43].noGravity = true;
                    Dust dust = Main.dust[num43];
                    dust.velocity *= 1.8f;
                    Dust dust4 = Main.dust[num43];
                    dust4.velocity.Y = dust4.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[num43].noGravity = false;
                        dust = Main.dust[num43];
                        dust.scale *= 0.5f;
                    }
                }
                Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
            }

        }

        public override void NPCLoot(NPC npc)
        {
            if(npc.type == NPCID.DD2Betsy)
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("BetsyScales"), Main.rand.Next(3,7));
            }
            if(npc.type == NPCID.GoblinSummoner)
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("Shadowflame"), Main.rand.Next(1, 4));
            }
            if(npc.type == NPCID.QueenBee && Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("BeeBattlerod"));
            }

            if (npc.type == NPCID.DukeFishron && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("FishronBattlerod"));
            }

            if(npc.type == NPCID.SnowFlinx && (!Main.hardMode || Main.rand.Next(8) == 0))
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("FlinxFur"));
            }

            if(Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneGlowshroom && ((!Main.hardMode && Main.rand.Next(3) == 0) || Main.rand.Next(15) == 0))
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("FungalSpores"), Main.rand.Next(1,6));
            }

            if (npc.wet && !npc.lavaWet && !npc.honeyWet && isHooked != 0 && Main.rand.Next(50) == 0)
            {
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("RustyHook"));
            }
            
            for(int i = 0; i< Main.projectile.Length; i++)
            {
                if(Main.projectile[i].active && Main.projectile[i].modProjectile != null)
                {
                    Bobber b = Main.projectile[i].modProjectile as Bobber;
                    if(b != null && b.getStuckEntity() is NPC && b.getStuckEntity().whoAmI == npc.whoAmI)
                    {
                        FishPlayer p = Main.player[b.projectile.owner].GetModPlayer<FishPlayer>();
                        if(Main.rand.Next(10000) < p.cratePercent)
                        {
                            int itmID = ItemID.WoodenCrate;
                            if (Main.rand.Next(6) == 0)
                            {
                                itmID = ItemID.IronCrate;
                            }else if(Main.rand.Next(24) == 0)
                            {
                                itmID = ItemID.GoldenCrate;
                            }
                            if (itmID == ItemID.WoodenCrate && Main.rand.Next(2) == 0)
                            {
                                List<int> possibleCrates = p.replaceWithRodCrate(p.player.inventory[p.player.selectedItem], -1);
                                if (possibleCrates.Count > 0)
                                {
                                    itmID = possibleCrates[Main.rand.Next(possibleCrates.Count)];
                                }
                            }
                            Item.NewItem(npc.Center, Vector2.Zero, itmID);
                        }
                    }
                }
            }
            base.NPCLoot(npc);
        }

        public void UpdateDebuffsByID(NPC npc, ref int id, int time, int buffSlot = -1)
        {
            if (npc.buffImmune[id])
                return;
            ModBuff bf = BuffLoader.GetBuff(id);
            if(bf != null)
            {
                if(buffSlot == -1)
                {
                    for(buffSlot = 0; buffSlot < 5; buffSlot++)
                    {
                        if(npc.buffType[buffSlot] <= 0)
                        {
                            npc.buffTime[buffSlot] = time;
                            break;
                        }
                    }
                    if(buffSlot >= 5)
                    {
                        return;
                    }
                }
                bf.Update(npc, ref buffSlot);
                //npc.buffTime[buffSlot] = 0;
                return;
            }

            if (id == 20)
            {
                npc.poisoned = true;
            }
            if (id == 70)
            {
                npc.venom = true;
            }
            if (id == 24)
            {
                npc.onFire = true;
            }
            if (id == 72)
            {
                npc.midas = true;
            }
            if (id == 69)
            {
                npc.ichor = true;
            }
            if (id == 31)
            {
                npc.confused = true;
            }
            if (id == 39)
            {
                npc.onFire2 = true;
            }
            if (id == 44)
            {
                npc.onFrostBurn = true;
            }
            if (id == 103)
            {
                npc.dripping = true;
            }
            if (id == 137)
            {
                npc.drippingSlime = true;
            }
            if (id == 119)
            {
                npc.loveStruck = true;
            }
            if (id == 120)
            {
                npc.stinky = true;
            }
            if (id == 151)
            {
                npc.soulDrain = true;
            }
            if (id == 153)
            {
                npc.shadowFlame = true;
            }
            if (id == 165)
            {
                npc.dryadWard = true;
            }
            if (id == 169)
            {
                npc.javelined = true;
            }
            if (id == 183)
            {
                npc.celled = true;
            }
            if (id == 186)
            {
                npc.dryadBane = true;
            }
            if (id == 189)
            {
                npc.daybreak = true;
            }
            if (id == 203)
            {
                npc.betsysCurse = true;
            }
            if (id == 204)
            {
                npc.oiled = true;
            }
        }

        public void updateCurrentInflictedBaitDebuffs(NPC npc)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)UnuBattleRods.Message.DebuffUpdate);
                pk.Write(npc.whoAmI);
                pk.Write(debuffsPresent.Count);
                for (int i = 0; i < debuffsPresent.Count; i++)
                {
                    pk.Write(debuffsPresent[i]);
                }
                pk.Send();
            }
        }

        public void applyBaitDebuffs(int[] debuffs)
        {
            for (int i = 0; i < debuffs.Length; i++)
            {
                if (debuffs[i] >= 0 && !debuffsPresent.Contains(debuffs[i]))
                {
                    debuffsPresent.Add(debuffs[i]);
                }
            }
        }
    }
}
