using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRods.NPCs;
using System.Collections.Generic;
using UnuBattleRods.Buffs;
using System.Linq;

namespace UnuBattleRods.Projectiles.Bobbers
{
    public abstract class Bobber : ModProjectile
    {
        public bool attatchesToEnemies = true;
        public bool attatchesToAllies = false;

        public short npcIndex = -1;
        protected short timeSinceLastBob = 0;

        protected float cummulativeSpeed = 0.0f;
        protected short timeSinceSpeed = 0;

        public short timeBobMax = 30;
        public short timeReelMax = 20;
        public float sizeMultiplier = 2.0f;
        public float speedIncrease = 2.0f;
        public short timeUntilGrab = 0;

        public float vampiricPercent = 0.0f;
        public float syphonPercent = 0.0f;

        public bool updatePos = true;

        int bobsSinceAttatched = 0;

        public int fishAmount = 0;

        public override void SetDefaults()
        {
            base.projectile.CloneDefaults(361);
            timeSinceLastBob = -9999;
        }

        public override bool PreAI()
        {
            if (Main.player[projectile.owner].GetModPlayer<FishPlayer>().sinkBobber)
            {
                this.projectile.ignoreWater = true;
                this.projectile.wet = false;
            }
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                projectile.netUpdate = true;
            }
            return base.PreAI();
        }

        #region AI
        public override void AI()
        {
            validateNpcIndex();
            if(timeSinceLastBob < -9998)
            {
                timeSinceLastBob = (short)Main.rand.Next(1, bobTime() + 1);
            }

            if (Main.player[projectile.owner].GetModPlayer<FishPlayer>().bobbersCatchItems)
            {
                for(int i = 0; i < Main.item.Length; i++)
                {
                    if (projectile.Hitbox.Intersects(Main.item[i].Hitbox))
                    {
                        Main.item[i].Center = Main.player[projectile.owner].Center;
                        // TODO, if needed, update server
                    }
                }
            }

            if (npcIndex == -1 && projectile.ai[0] < 1f && timeUntilGrab <= 0 && Main.player[projectile.owner].GetModPlayer<FishPlayer>().maxBobbersPerEnemy != 0)
            {
                tryAndAttatchBobberToAnything();
            }
            if(timeUntilGrab > 0)
            {
                timeUntilGrab--;
            }

            if(projectile.ai[0] >= 2f) //broken fishing line
            {
                if (npcIndex == -1 && Main.player[projectile.owner].GetModPlayer<FishPlayer>().destroyBobber)
                {
                    projectile.Kill();
                    return;
                }
                else
                {
                    if(npcIndex != -1)
                    {
                        onDiscard(getStuckEntity());
                    }
                    npcIndex = -1;
                    updatePos = true;
                    doBaseAI();
                    return;
                }
            }
            int baitRange = Main.player[projectile.owner].GetModPlayer<FishPlayer>().baitDispersalRange;
            if (baitRange > 0 && Main.player[projectile.owner].GetModPlayer<FishPlayer>().hasAnyBaitDebuffs() && (npcIndex >= 0 || Math.Round(Math.Abs(this.projectile.velocity.Y)) == 0 || this.projectile.wet))
            {
                Rectangle rangeHitbox = new Rectangle((int)(projectile.position.X - (projectile.width / 2 + baitRange / 2)), (int)(projectile.position.Y - (projectile.height / 2 + baitRange / 2)), projectile.width + baitRange, projectile.height + baitRange);
                if (timeSinceLastBob == 1)
                {
                    for(int i = 0; i < 200; i++)//Main.npc.Length
                    {
                            if (i != npcIndex && canAttatchToNPC(Main.npc[i]))
                            {
                                if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                                {
                                    applyBaitToEntity(Main.npc[i], Main.player[projectile.owner]);
                                    int randMax = Main.rand.Next(2, 5);
                                    for (int j = 0; j < randMax; j++)
                                    {
                                        Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default(Color), Main.rand.NextFloat(0.5f, 2f));
                                    }
                                }
                            }
                    }
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        if (i != npcIndex - Main.npc.Length && this.canAttatchToPlayer(Main.player[i]))
                        {
                            if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                            {
                                applyBaitToEntity(Main.player[i], Main.player[projectile.owner]);
                                int randMax = Main.rand.Next(2, 5);
                                for (int j = 0; j < randMax; j++)
                                {
                                    Dust.NewDust(Main.player[i].position, Main.player[i].width, Main.player[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default(Color), Main.rand.NextFloat(0.5f, 2f));
                                }
                            }
                        }
                    }
                    if (npcIndex < 0)
                    {
                        timeSinceLastBob = bobTime();
                    }
                }
                if (npcIndex < 0)
                {
                    timeSinceLastBob--;
                    if(timeSinceLastBob <= 0)
                    {
                        timeSinceLastBob = bobTime();
                    }
               }
                 
            }

            if (npcIndex >= 0) //is stuck on something
            {
                
                if (npcIndex < 200)//is stuck on NPC 200 ~= Main.npc.Length
                {
                    int npc = npcIndex;
                    if (!Main.npc[npc].active || Main.npc[npc].type == 0)
                    {
                        breakFree();
                        doBaseAI();
                        return;
                    }
                    if(projectile.ai[0] == 1) //retracting line
                    {
                        if(Main.netMode != 2 && Main.myPlayer == projectile.owner)
                        {
                            if (Main.mouseRight && timeUntilGrab <= 0)
                            {
                                breakFree();
                                cummulativeSpeed = 0.0f;
                            }
                            else if (Main.mouseLeft && reelTime() > 0)
                            {
                                if (timeSinceLastBob > 0)
                                    timeSinceLastBob--;

                                if (!Main.player[projectile.owner].GetModPlayer<FishPlayer>().beingReeled)
                                {
                                    Main.player[projectile.owner].GetModPlayer<FishPlayer>().beingReeled = true;
                                    tryMoveTarget(Main.npc[npc]);
                                }

                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                               /* if (Main.netMode != NetmodeID.Server)
                                {*/
                                    applyDamageAndDebuffs(Main.npc[npc], Main.player[projectile.owner]);
                                //}
                            }else if(!Main.mouseLeft && !Main.mouseRight)
                            {
                                cummulativeSpeed = 0.0f;
                                projectile.ai[0] = 0;
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[projectile.owner], Main.npc[npc]);
                            }

                        }else
                        {
                            /*if (Main.netMode != NetmodeID.Server)
                            {*/
                                applyDamageAndDebuffs(Main.npc[npc], Main.player[projectile.owner]);
                            //}
                        }
                    }
                    else
                    {
                        checkEntityForMove(Main.player[projectile.owner], Main.npc[npc]);
                    }
                   
                    return;
                }
                else 
                {
                    int player = npcIndex - Main.npc.Length;
                    if (!Main.player[player].active || Main.player[player].dead)
                    {
                        breakFree();
                        doBaseAI();
                        return;
                    }
                    if (projectile.ai[0] == 1) //retracting line
                    {
                        if (Main.netMode != 2 && Main.myPlayer == projectile.owner)
                        {
                            if (Main.mouseRight && timeUntilGrab <= 0)
                            {
                                breakFree();
                                cummulativeSpeed = 0.0f;
                            }else if (Main.mouseLeft && reelTime() > 0)
                            {
                                if (timeSinceLastBob > 0)
                                    timeSinceLastBob--;

                                if (!Main.player[projectile.owner].GetModPlayer<FishPlayer>().beingReeled)
                                {
                                    Main.player[projectile.owner].GetModPlayer<FishPlayer>().beingReeled = true;
                                    tryMoveTarget(Main.player[player]);
                                }
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                              /*  if (Main.netMode != NetmodeID.Server)
                                {*/
                                    applyDamageAndDebuffs(Main.player[player], Main.player[projectile.owner]);
                                //}
                            }else if (!Main.mouseLeft && !Main.mouseRight)
                            {
                                cummulativeSpeed = 0;
                                projectile.ai[0] = 0;
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[projectile.owner], Main.player[player]);
                            }
                        }else
                        {
                           /* if (Main.netMode != NetmodeID.Server)
                            {*/
                                applyDamageAndDebuffs(Main.player[player], Main.player[projectile.owner]);
                            //}
                        }
                    }
                    else
                    {
                        checkEntityForMove(Main.player[projectile.owner], Main.player[player]);
                    }
                    return;
                }
            }
            else
            {
                doBaseAI();
            }

        }
        public void doBaseAI()
        {
            
           // projectile.VanillaAI();
            /*if (Main.player[projectile.owner].GetModPlayer<FishPlayer>().sinkBobber)
            {
                if (projectile.velocity.Y != 0f)
                {
                    if (projectile.wet && projectile.ai[0] >= 0 && projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = 6f;
                    }
                }
            }*/
            
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(npcIndex);
            writer.Write(timeSinceLastBob);
            writer.Write(timeSinceSpeed);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npcIndex = reader.ReadInt16();
            timeSinceLastBob = reader.ReadInt16();
            timeSinceSpeed = reader.ReadInt16();
        }

        #endregion AI

        #region CheckToAttatch

        private void tryAndAttatchBobberToAnything()
        {
            List<Entity> possibleTargets = getAllCollidingEntities(new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height));

            if(possibleTargets.Count <= 0)
            {
                return;
            }
            FishPlayer fOwner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
            if (fOwner.smartBobberDistribution)
            {
                smartAttatch(fOwner, possibleTargets);
            }else
            {
                while (possibleTargets.Count > 0)
                {
                    int targetIdx = Main.rand.Next(possibleTargets.Count);
                    Entity target = possibleTargets[targetIdx];
                    possibleTargets.RemoveAt(targetIdx);

                    if(canAttatchBobber(fOwner, target))
                    {
                        if (target is NPC)
                        {
                            simpleAttatch(fOwner, (NPC)target);
                            return;
                        }
                        if (target is Player)
                        {
                            simpleAttatch(fOwner, (Player)target);
                            return;
                        }
                    }

                }
           }




        }

        public List<Entity> getAllCollidingEntities(Rectangle checkHitbox)
        {
            List<Entity> possibleTargets = new List<Entity>();
            List<Entity> ans = new List<Entity>();
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                if (canAttatchToNPC(Main.npc[i]))
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    if (checkHitbox.Intersects(value))
                    {
                        ans.Add(Main.npc[i]);
                    }
                }
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (canAttatchToPlayer(Main.player[i]))
                {
                    Rectangle value = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                    if (checkHitbox.Intersects(value))
                    {
                        ans.Add(Main.player[i]);
                    }
                }
            }
            possibleTargets.AddRange(ans.OrderBy(x => Vector2.DistanceSquared(x.Center, projectile.Center)));
            return possibleTargets;

        }

        public List<Entity> getAllCollidingEntities(List<Rectangle> checkHitbox)
        {
            List<Entity> possibleTargets = new List<Entity>();
            List<Entity> ans = new List<Entity>();
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
               // UnuBattleRods.debugChat("i = "+ i);
                if (canAttatchToNPC(Main.npc[i]))
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    for (int j = 0; j <checkHitbox.Count; j++)
                    {
                        if (checkHitbox[j].Intersects(value))
                        {
                            ans.Add(Main.npc[i]);
                            j = checkHitbox.Count;
                        }
                    }
                }
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (canAttatchToPlayer(Main.player[i]))
                {
                    Rectangle value = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                    for (int j = 0; j < checkHitbox.Count; j++)
                    {
                        if (checkHitbox[j].Intersects(value))
                        {
                            ans.Add(Main.player[i]);
                            j = checkHitbox.Count;
                        }
                    }
                }
            }
            return ans;

        }



        public bool canAttatchToNPC(NPC npc)
        {
            if (!npc.active || npc.type == 0)
                return false;
            if (npc.immortal || npc.dontTakeDamage)
            {
                if (npc.type != NPCID.TargetDummy)
                    return false;
            }
            if (!attatchesToAllies &&
             (npc.friendly && !(npc.type == NPCID.Guide && Main.player[projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[projectile.owner].killClothier))
             )
                return false;
            if (!npc.friendly && !attatchesToEnemies)
            {
                return false;
            }

            bool? b = NPCLoader.CanBeHitByProjectile(npc, projectile);
            if (b.HasValue && !b.Value)
                return false;
            b = ProjectileLoader.CanHitNPC(projectile, npc);
            if (b.HasValue && !b.Value)
                return false;
            b = PlayerHooks.CanHitNPCWithProj(projectile, npc);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool canAttatchToPlayer(Player p)
        {
            if (p.whoAmI == projectile.owner || !p.active || p.dead)
                return false;

            if(attatchesToAllies && (p.team == Main.player[projectile.owner].team))
            {
                return true;
            }

            if (!attatchesToAllies && (!p.hostile || p.team == Main.player[projectile.owner].team))
                return false;

            if (!attatchesToEnemies && (p.hostile && p.team != Main.player[projectile.owner].team))
                return false;

            bool? b = PlayerHooks.CanHitPvpWithProj(projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            b = ProjectileLoader.CanHitPvp(projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool canAttatchBobber(FishPlayer fOwner, Entity target)
        {

            if (fOwner.maxBobbersPerEnemy < 0)
                return true;
            if (fOwner.maxBobbersPerEnemy == 0)
                return false;
            int totalBobbers = 0;
            foreach (Bobber b in getBobbersAttatchedTo(target))
            {
                if (b.projectile.owner == fOwner.player.whoAmI)
                {
                    totalBobbers++;
                }
            }
            if (totalBobbers < fOwner.maxBobbersPerEnemy)
                return true;
            return false;
        }
        int lastNPCIndex = -1;

        public void simpleAttatch(FishPlayer fOwner, NPC npc)
        {
            npcIndex = (short)npc.whoAmI;
            lastNPCIndex = npcIndex;

            npc.PlayerInteraction(projectile.owner);
            FishGlobalNPC fnpc = npc.GetGlobalNPC<FishGlobalNPC>();
            fnpc.isHooked++;
            if (fOwner.seals)
            {
                fnpc.isSealed++;
            }
            projectile.Center = npc.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        private void simpleAttatch(FishPlayer fOwner, Player p)
        {
            npcIndex = (short)(p.whoAmI + Main.npc.Length);
            p.GetModPlayer<FishPlayer>().isHooked++;
            if (fOwner.seals)
            {
                p.GetModPlayer<FishPlayer>().isSealed++;
            }
            projectile.Center = p.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
            return;
        }

        public bool smartAttatch(FishPlayer fOwner, List<Entity> targets)
        {
            
            if (fOwner.maxBobbersPerEnemy == 0)
                return false; 
            List<Entity> zeroBobbers = targets.FindAll(x => (getNoOfBobbersAttatchedTo(x, fOwner.player.whoAmI) == 0));
            if(zeroBobbers.Count > 0)
            {
                Entity target = zeroBobbers[Main.rand.Next(zeroBobbers.Count)];
                if (target is NPC)
                {
                    simpleAttatch(fOwner, (NPC)target);
                    return true;
                }
                if (target is Player)
                {
                    simpleAttatch(fOwner, (Player)target);
                    return true;
                }
                //shouldn't happen
                return false;
            }
            List<Rectangle> hitboxes = new List<Rectangle>();
            foreach(Entity target in targets)
            {
                hitboxes.Add(new Rectangle((int)(target.position.X - fOwner.smartBobberRange / 2), (int)(target.position.Y - fOwner.smartBobberRange / 2), target.width + fOwner.smartBobberRange, target.height + fOwner.smartBobberRange));
            }
            List<Entity> newTargetList = getAllCollidingEntities(hitboxes);

            if(newTargetList.Count <= targets.Count)
            {
                List<Entity> reducedTargets = findEntityWithLeastBobbers(newTargetList, fOwner.player.whoAmI);
                Entity target = reducedTargets[Main.rand.Next(reducedTargets.Count)];
                if (target is NPC)
                {
                    simpleAttatch(fOwner, (NPC)target);
                    return true;
                }
                if (target is Player)
                {
                    simpleAttatch(fOwner, (Player)target);
                    return true;
                }
                //shouldn't happen
                return false;
            }
            else
            {
                return smartAttatch(fOwner, newTargetList);
            }           
        }

        #endregion CheckToAttatch

        #region movingEntities

        private void tryMoveTarget(Entity target)
        {

            Vector2 vel = Main.player[projectile.owner].position - target.position; //new Vector2(target.velocity.X, target.velocity.Y);
            Vector2 vel2 = target.velocity - Main.player[projectile.owner].velocity;

            if (!Main.player[projectile.owner].accFishingLine && vel2.Length() > TensileStrength())
            {                
                breakFree();
                return;
            }

            vel.Normalize();
            if (reelTime() > 0)
            {
                if (timeSinceSpeed <= 0)
                {
                    timeSinceSpeed = reelTime();
                    cummulativeSpeed += speedIncrease;
                    if (cummulativeSpeed >= TensileStrength())
                    {
                        cummulativeSpeed = TensileStrength() - 0.01f;
                    }
                }
                timeSinceSpeed--;
            }


            vel *= cummulativeSpeed;
            if (target.height * target.width > Main.player[projectile.owner].width * Main.player[projectile.owner].height * sizeMult() || (target is NPC && ((NPC)target).type == NPCID.TargetDummy))
            {

                Main.player[projectile.owner].position -= (Main.player[projectile.owner].velocity + vel);
                Tile t = Main.tile[(int)(Main.player[projectile.owner].position.X / 16), (int)(Main.player[projectile.owner].position.Y / 16)];
                if (t != null && t.active() && (Main.tileSolid[t.type]))
                {
                    Main.player[projectile.owner].position += Main.player[projectile.owner].velocity + vel;
                }
                if (Main.netMode != 0)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)7);
                    pk.Write(projectile.owner);
                    pk.Write(Main.player[projectile.owner].Center.X);
                    pk.Write(Main.player[projectile.owner].Center.Y);
                    pk.Send();
                }
            }
            else
            {
                target.position += vel - target.velocity;
                Tile t = Main.tile[(int)(target.position.X / 16), (int)(target.position.Y / 16)];
                if (t != null && t.active() && (Main.tileSolid[t.type]))
                {
                    target.position -= (vel - target.velocity);
                }
                if (Main.netMode != 0)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)6);
                    pk.Write(npcIndex);
                    pk.Write((float)target.Center.X);
                    pk.Write((float)target.Center.Y);
                    pk.Send();
                }
                else
                {
                    if (target is NPC)
                    {
                        ((NPC)target).GetGlobalNPC<FishGlobalNPC>().newCenter.X = target.Center.X;
                        ((NPC)target).GetGlobalNPC<FishGlobalNPC>().newCenter.Y = target.Center.Y;
                    }
                    else if (target is Player)
                    {
                        ((Player)target).GetModPlayer<FishPlayer>().newCenter.X = target.Center.X;
                        ((Player)target).GetModPlayer<FishPlayer>().newCenter.Y = target.Center.Y;
                    }
                }
            }
            updatePos = false;
            projectile.Center = target.Center;
            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        private void checkEntityForMove(Player player, Entity target)
        {
            updatePos = false;
            projectile.Center = target.Center;

            Vector2 vector = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num5 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector.X;
            float num6 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector.Y;
            float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
            if (num7 > 2500f)
            {
                float num598 = 15.9f / num7;
                num5 *= num598;
                num6 *= num598;
                projectile.velocity.X = (projectile.velocity.X * (float)(num5 - 1) + num6) / 10f;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num5 - 1) + num6) / 10f;

                tryMoveTarget(target);

            }
            if (npcIndex >= 0 )//&& Main.netMode != NetmodeID.Server)
            {
                // Main.NewText("Checking damage - " + (target is NPC) + " " + (target is Player)+ ";");
                if (target is NPC)
                {
                    applyDamageAndDebuffs((NPC)target, Main.player[projectile.owner]);
                }
                else if (target is Player)
                {
                    applyDamageAndDebuffs((Player)target, Main.player[projectile.owner]);
                }
            }
        }

        #endregion movingEntities


        private void damageNPC(NPC npc)
        {
            if (Main.netMode != 1 && projectile.damage > 0 && (!((npc.immortal || npc.dontTakeDamage) && npc.type != NPCID.TargetDummy)))
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                int dmg = (int)Math.Round(projectile.damage * (1 + escalationBonus(owner)));
                dmg = Main.DamageVar(dmg);

                if (dmg < 1)
                    dmg = 1;

                int num18 = Item.NPCtoBanner(npc.BannerID());
                if (num18 >= 0)
                {
                    Main.player[projectile.owner].lastCreatureHit = num18;
                    if (Main.netMode != 2 && Main.player[projectile.owner].NPCBannerBuff[num18])
                    {
                        if (Main.expertMode)
                        {
                            dmg *= 2;
                        }
                        else
                        {
                            dmg = (int)((double)dmg * 1.5);
                        }
                    }
                }
                float knockback = projectile.knockBack;
                bool crit = Main.rand.Next(100) < owner.bobberCrit + Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].crit;
                int dir = 0;
                ProjectileLoader.ModifyHitNPC(projectile, npc, ref dmg, ref knockback, ref crit, ref dir);
                NPCLoader.ModifyHitByProjectile(npc, projectile, ref dmg, ref knockback, ref crit, ref dir);
                PlayerHooks.ModifyHitNPCWithProj(projectile, npc, ref dmg, ref knockback, ref crit, ref dir);

                /*if (crit)
                {
                    dmg *= 2;
                }*/
                Main.player[projectile.owner].OnHit(npc.Center.X, npc.Center.Y, npc);

                if (Main.player[projectile.owner].armorPenetration > 0)
                {
                    dmg += npc.checkArmorPenetration(Main.player[projectile.owner].armorPenetration);
                }
                int num25 = (int)npc.StrikeNPC(dmg, knockback, dir, crit, false, false);
                if (Main.netMode == 2)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)3);
                    pk.Write(num25);
                    pk.Send(projectile.owner,-1);
                } else {
                    if (Main.player[projectile.owner].accDreamCatcher)
                    {
                        Main.player[projectile.owner].addDPS(num25);
                    }
                }

                if (Main.netMode != 0)
                {                    
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, (float)dmg, knockback, (float)dir, crit? 1:0, 0, 0);
                }

                ProjectileLoader.OnHitNPC(projectile, npc, num25, knockback, crit);
                NPCLoader.OnHitByProjectile(npc, projectile, num25, knockback, crit);
                PlayerHooks.OnHitNPCWithProj(projectile, npc, num25, knockback, crit);
                npc.netUpdate = true;
            }
           

        }

       

        private void damagePlayer(Player p)
        {
            if (Main.netMode != 1 && projectile.damage > 0)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                int dmg = (int)Math.Round(projectile.damage * (1 + escalationBonus(owner)));
                dmg = Main.DamageVar(dmg);

                if (dmg < 1)
                    dmg = 1;
                bool crit = Main.rand.Next(100) < owner.bobberCrit + Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].crit;
                ProjectileLoader.ModifyHitPvp(projectile, p, ref dmg, ref crit);
                PlayerHooks.ModifyHitPvpWithProj(projectile, p, ref dmg, ref crit);

                Main.player[projectile.owner].OnHit(p.Center.X, p.Center.Y, p);
                /* if (crit)
                 {
                     dmg *= 2;
                 }*/

                dmg = (int)p.Hurt(PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI), dmg, projectile.direction, true, false, crit, -1);

                if (Main.netMode == 2)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)3);
                    pk.Write(dmg);
                    pk.Send(projectile.owner, -1);
                }
                else
                {
                    if (Main.player[projectile.owner].accDreamCatcher)
                    {
                        Main.player[projectile.owner].addDPS(dmg);
                    }
                }

                ProjectileLoader.OnHitPvp(projectile, p, dmg, crit);
                PlayerHooks.OnHitPvpWithProj(projectile, p, dmg, crit);

                if (Main.netMode != 0)
                {
                    NetMessage.SendPlayerHurt(p.whoAmI, PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI), dmg, projectile.direction, crit, true, 0, -1, -1);
                }
            }
        }

        private float escalationBonus(FishPlayer owner)
        {
            if (owner.escalationBonus != 0)
            {
                float seconds = (timeBobMax * bobsSinceAttatched) / 60f;
                return Math.Min(owner.escalationBonus * seconds, owner.escalationMax);
            }else
            {
                bobsSinceAttatched = 0;
            }
            return 0f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            {
                healthAndManaRecovery(damage);
            }
            base.OnHitNPC(target, damage, knockback, crit);

        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            healthAndManaRecovery(damage);
            base.OnHitPvp(target, damage, crit);
        }

        public void healthAndManaRecovery(int damage) { 
            Player player = Main.player[projectile.owner];
            FishPlayer pl = player.GetModPlayer<FishPlayer>();

            int manaRec = manaFromDamage(pl, damage);
            int healthRec = healthFromDamage(pl, damage);
            if(manaRec > 0 && player.statManaMax2 > player.statMana)
            {
                    player.statMana += manaRec;
                    if (player.statMana > player.statManaMax2)
                    {
                        player.statMana = player.statManaMax2;
                    }
                    if (Main.netMode != 2)
                    {
                        player.ManaEffect(manaRec);
                    }else
                    {
                        ModPacket pk = mod.GetPacket();
                        pk.Write((byte)UnuBattleRods.Message.ManaEffect);
                        pk.Write((ushort)projectile.owner);
                        pk.Write(manaRec);
                        pk.Send();
                    }
            }
            if (healthRec > 0 && !Main.player[projectile.owner].moonLeech && player.statLifeMax2 > player.statLife)
            {
                    player.statLife += healthRec;
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    if (Main.netMode != 2)
                    {
                        player.HealEffect(healthRec);
                    }
                    else
                    {
                        ModPacket pk = mod.GetPacket();
                        pk.Write((byte)UnuBattleRods.Message.HealEffect);
                        pk.Write((ushort)projectile.owner);
                        pk.Write(healthRec);
                        pk.Send();
                    }
            }
            
        }

        private int healthFromDamage(FishPlayer owner, int damage)
        {
            float ans =  damage*vampiricPercent;
            ans += damage * owner.vampiricLinePercent;
            return (int)Math.Round(ans);
        }

        private int manaFromDamage(FishPlayer owner, int damage)
        {
            float ans = damage * syphonPercent;
            ans += damage * owner.syphonLinePercent;
            return (int)Math.Round(ans);
        }

        public void breakFree()
        {
            // Main.NewText("Break free. Break;");

            Entity e = getStuckEntity();
            if(e is NPC)
            {
                ((NPC)e).GetGlobalNPC<FishGlobalNPC>().isHooked--;
            }
            if (e is Player)
            {
                ((Player)e).GetModPlayer<FishPlayer>().isHooked--;
            }

            bobsSinceAttatched = 0;

            if (Main.player[projectile.owner].GetModPlayer<FishPlayer>().fallOnFloor && !Main.player[projectile.owner].GetModPlayer<FishPlayer>().destroyBobber &&
               Main.rand.Next(100) < Main.player[projectile.owner].GetModPlayer<FishPlayer>().fallOnFloorPercentage)
            {
                npcIndex = -1;
                projectile.ai[0] = 0f;
                updatePos = true;
                projectile.velocity.X = 0;
                projectile.velocity.Y = 0;
                projectile.tileCollide = true;
                timeUntilGrab = 60;
                projectile.netUpdate = true;
            }
            else
            {
                if (npcIndex != -1)
                {
                    findSuitableDiscardableAmmo(Main.player[projectile.owner]);
                    onDiscard(getStuckEntity());
                }

                npcIndex = -1;
                projectile.ai[0] = 2f;
                updatePos = true;
            }

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        protected virtual void findSuitableDiscardableAmmo(Player p)
        {
            for (int i = 54; i < 58; i++) //Ammo slots only
            {
                Items.Discardables.Discardable disc = p.inventory[i].modItem as Items.Discardables.Discardable;
                if (disc != null && CanUseDiscardable(p, disc, i))
                {
                    if (p.inventory[i].consumable)
                    {
                        p.inventory[i].stack--;
                        if(p.inventory[i].stack <= 0)
                        {
                            p.inventory[i] = new Item();
                        }
                    }

                    p.GetModPlayer<FishPlayer>().currentDiscard = disc;
                    return;
                }
            }
            p.GetModPlayer<FishPlayer>().currentDiscard = null;
       }

        protected virtual bool CanUseDiscardable(Player p, Items.Discardables.Discardable discardableItem, int slotPosition)
        {
            return true;
        }

        private float sizeMult()
        {
            return sizeMultiplier * Main.player[projectile.owner].GetModPlayer<FishPlayer>().sizeMultiplierMultiplier;
        }

       

        public virtual float TensileStrength()
        {
            return 10f;
        }

        public override bool ShouldUpdatePosition()
        {
            if (npcIndex == -1)
                return true;
            if(npcIndex < 200)
            {
                return Main.npc[npcIndex] == null || Main.npc[npcIndex].type == 0 || !Main.npc[npcIndex].active;
            }
            if (npcIndex - Main.npc.Length >= 0 && npcIndex - Main.npc.Length < Main.player.Length)
            {
                return Main.player[npcIndex - Main.npc.Length] == null || !Main.player[npcIndex - Main.npc.Length].active || Main.player[npcIndex - Main.npc.Length].dead;
            }
            return true;
        }

        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            Player player = Main.player[base.projectile.owner];
            if (base.projectile.bobber && Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].holdStyle > 0)
            {
                float num = player.MountedCenter.X;
                float num2 = player.MountedCenter.Y;
                num2 += Main.player[base.projectile.owner].gfxOffY;
                //int type = Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].type;

                alterCenter(Main.player[base.projectile.owner].gravDir, ref num, ref num2);

                Vector2 value = new Vector2(num, num2);
                value = Main.player[base.projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
                float num3 = base.projectile.position.X + (float)base.projectile.width * 0.5f - value.X;
                float num4 = base.projectile.position.Y + (float)base.projectile.height * 0.5f - value.Y;
                Math.Sqrt((double)(num3 * num3 + num4 * num4));
                float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                bool flag = true;
                if (num3 == 0f && num4 == 0f)
                {
                    flag = false;
                }
                else
                {
                    float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                    num5 = 12f / num5;
                    num3 *= num5;
                    num4 *= num5;
                    value.X -= num3;
                    value.Y -= num4;
                    num3 = base.projectile.position.X + (float)base.projectile.width * 0.5f - value.X;
                    num4 = base.projectile.position.Y + (float)base.projectile.height * 0.5f - value.Y;
                }
                while (flag)
                {
                    float num6 = 12f;
                    float num7 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                    float num8 = num7;
                    if (float.IsNaN(num7) || float.IsNaN(num8))
                    {
                        flag = false;
                    }
                    else
                    {
                        if (num7 < 20f)
                        {
                            num6 = num7 - 8f;
                            flag = false;
                        }
                        num7 = 12f / num7;
                        num3 *= num7;
                        num4 *= num7;
                        value.X += num3;
                        value.Y += num4;
                        num3 = base.projectile.position.X + (float)base.projectile.width * 0.5f - value.X;
                        num4 = base.projectile.position.Y + (float)base.projectile.height * 0.1f - value.Y;
                        if (num8 > 12f)
                        {
                            float num9 = 0.3f;
                            float num10 = Math.Abs(base.projectile.velocity.X) + Math.Abs(base.projectile.velocity.Y);
                            if (num10 > 16f)
                            {
                                num10 = 16f;
                            }
                            num10 = 1f - num10 / 16f;
                            num9 *= num10;
                            num10 = num8 / 80f;
                            if (num10 > 1f)
                            {
                                num10 = 1f;
                            }
                            num9 *= num10;
                            if (num9 < 0f)
                            {
                                num9 = 0f;
                            }
                            num10 = 1f - base.projectile.localAI[0] / 100f;
                            num9 *= num10;
                            if (num4 > 0f)
                            {
                                num4 *= 1f + num9;
                                num3 *= 1f - num9;
                            }
                            else
                            {
                                num10 = Math.Abs(base.projectile.velocity.X) / 3f;
                                if (num10 > 1f)
                                {
                                    num10 = 1f;
                                }
                                num10 -= 0.5f;
                                num9 *= num10;
                                if (num9 > 0f)
                                {
                                    num9 *= 2f;
                                }
                                num4 *= 1f + num9;
                                num3 *= 1f - num9;
                            }
                        }
                        rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                        Color color = getLineColor(value);
                        Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Rectangle?(new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num6)), color, rotation, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            return false;
        }

        

        public virtual void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += (float)(43 * Main.player[base.projectile.owner].direction);
            if (Main.player[base.projectile.owner].direction < 0)
            {
               x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public virtual Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        

        public void applyBaitToEntity(NPC target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.hasAnyBaitDebuffs())
            {
                // ErrorLogger.Log("Adding Powered Bait Debuff from player " + fOwner.player.whoAmI);
                target.AddBuff(pbdbf, 120);
                FishGlobalNPC fnpc = target.GetGlobalNPC<FishGlobalNPC>();
                fnpc.applyBaitDebuffs(fOwner.baitDebuff);
                //fOwner.updateBaits();
                //ErrorLogger.Log("Added debuff");
            }
        }

        public void applyBaitToEntity(Player target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.hasAnyBaitDebuffs())
            {
                // ErrorLogger.Log("Adding Powered Bait Debuff from player " + fOwner.player.whoAmI);
                target.AddBuff(pbdbf, 120);
                FishPlayer fTarget = target.GetModPlayer<FishPlayer>();
                for(int i = 0; i<fOwner.baitDebuff.Length; i++)
                {
                    if(fOwner.baitDebuff[i] >= 0 && !fTarget.debuffsPresent.Contains(fOwner.baitDebuff[i]))
                    {
                        fTarget.debuffsPresent.Add(fOwner.baitDebuff[i]);
                    }
                }
                //fOwner.updateBaits();
                //ErrorLogger.Log("Added debuff");
            }
        }

        public virtual void applyDamageAndDebuffs(NPC npc, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            applyBaitToEntity(npc, player);
            if (bobTime() > 0)
            {
                if (timeSinceLastBob <= 0)
                {
                    timeSinceLastBob = bobTime();
                    damageNPC(npc);
                    bobsSinceAttatched++;
                }
                timeSinceLastBob--;
            }

        }

        public virtual void applyDamageAndDebuffs(Player target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            applyBaitToEntity(target, player);

            if (bobTime() > 0)
            {
                if (timeSinceLastBob <= 0)
                {
                    timeSinceLastBob = bobTime();
                    damagePlayer(target);
                    bobsSinceAttatched++;
                }
                timeSinceLastBob--;
            }
           
        }

        public virtual short bobTime()
        {
            if (timeBobMax == 0)
                return 0;
            if (projectile != null && projectile.owner < 255 && Main.player[projectile.owner] != null &&  Main.player[projectile.owner].active)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                if (owner != null)
                {
                    int ans = (int)Math.Round(timeBobMax  - timeBobMax * owner.bobberSpeed);
                    return (short)(ans <= 5 ? 5 : ans);
                }
            }
            return timeBobMax;
        }
        public virtual short reelTime()
        {
            if(timeReelMax == 0)
                return 0;
            if (projectile != null && projectile.owner < 255 && Main.player[projectile.owner] != null && Main.player[projectile.owner].active)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                if (owner != null)
                {
                    int ans = (int)Math.Round(timeReelMax - timeReelMax * owner.reelSpeed);
                    return (short)(ans <= 5 ? 5 : ans);
                }
            }
            return timeReelMax;
        }

        public override void Kill(int timeLeft)
        {
            if (npcIndex >= 0 && Main.netMode != 0)
            {
                breakFree();
               // NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }


        public bool isStuck()
        {
            return npcIndex > -1;
        }

        public Entity getStuckEntity()
        {
            if (npcIndex < 0 || npcIndex > Main.npc.Length + Main.player.Length)
            {
                return projectile;
            }
            else if (npcIndex < Main.npc.Length)
            {
                return Main.npc[npcIndex];
            }
            else
            {
                return Main.player[npcIndex - Main.npc.Length];
            }
        }

        public static List<Bobber> getBobbersAttatchedTo(Entity stuck)
        {
            List<Bobber> ans = new List<Bobber>();

            for(int i = 0; i< Main.projectile.Length; i++)
            {
                Bobber b = Main.projectile[i].modProjectile as Bobber;
                if (b != null && b.getStuckEntity().GetType() == stuck.GetType() && b.getStuckEntity().whoAmI == stuck.whoAmI)
                    ans.Add(b);
            }
            return ans;
        }


        public static List<Player> getOwnersOfBobbersAttatchedTo(Entity stuck)
        {
            List<Bobber> bb = getBobbersAttatchedTo(stuck);
            bool[] players = new bool[Main.player.Length];
            List<Player> ans = new List<Player>();
            foreach(Bobber b in bb)
            {
                players[b.projectile.owner] = true;
            }

            for(int i = 0; i < players.Length; i++)
            {
                if(players[i])
                    ans.Add(Main.player[i]);
            }
            return ans;
        }

        public int getNoOfBobbersAttatchedTo(Entity target, int owner = 256)
        {
            int totalBobbers = 0;
            foreach (Bobber b in getBobbersAttatchedTo(target))
            {
                if (owner >= 256 || b.projectile.owner == owner)
                {
                    totalBobbers++;
                }
            }

            return totalBobbers;
        }

        public List<Entity> findEntityWithLeastBobbers(List<Entity> targets, int owner = 256)
        {
            List<Entity> ans = new List<Entity>();
            int least = Int32.MaxValue;

            foreach (Entity e in targets)
            {
                int bobs = getNoOfBobbersAttatchedTo(e, owner);
                if (bobs < least)
                {
                    ans.Clear();
                    least = bobs;
                }

                if (bobs == least)
                {
                    ans.Add(e);
                }
            }
            return ans;
        }

        public virtual void onDiscard(Entity stuck)
        {
            Main.player[Main.myPlayer].GetModPlayer<FishPlayer>().onBobKill(this);
        }

        public void validateNpcIndex() {
            if(npcIndex >= 0)
            {
                if (npcIndex < 200) //Main.npc.Length
                {
                    if (!Main.npc[npcIndex].active)
                    {
                        npcIndex = -1;
                        breakFree();
                        return;
                    }
                    return;
                }
                if (npcIndex - Main.npc.Length < Main.player.Length)
                {
                    if (!Main.player[npcIndex - Main.npc.Length].active || !Main.player[npcIndex - Main.npc.Length].dead)
                    {
                        npcIndex = -1;
                        breakFree();
                        return;
                    }
                    return;
                }
                npcIndex = -1;
                return;
            }
        }
    }
}