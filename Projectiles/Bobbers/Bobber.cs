using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Projectiles.Bobbers
{
    public abstract class Bobber : ModProjectile
    {
        public short npcIndex = -1;
        protected short timeSinceLastBob = 0;

        protected float cummulativeSpeed = 0.0f;
        protected short timeSinceSpeed = 0;

        public short timeBobMax = 30;
        public short timeReelMax = 20;
        public float sizeMultiplier = 2.0f;
        public float speedIncrease = 2.0f;

        public float vampiricPercent = 0.0f;
        public float syphonPercent = 0.0f;

        bool updatePos = true;

        int bobsSinceAttatched = 0;

        public override void SetDefaults()
        {
            base.projectile.CloneDefaults(361);
            timeSinceLastBob = -9999;
        }

        public override void AI()
        {
            if(timeSinceLastBob < -9998)
            {
                timeSinceLastBob = (short)Main.rand.Next(1, bobTime() + 1);
            }

            if (npcIndex == -1 && projectile.ai[0] < 1f)
            {
                checkIfCollideWithNPCOrPlayer();
            }
            if(projectile.ai[0] >= 2f) //broken fishing line
            {
                if (npcIndex == -1 && Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).destroyBobber)
                {
                    projectile.Kill();
                    return;
                }
                else
                {
                    npcIndex = -1;
                    updatePos = true;
                    base.AI();
                    return;
                }
            }
            if (npcIndex >= 0) //is stuck on something
            {
                if (npcIndex < Main.npc.Length)//is stuck on NPC
                {
                    int npc = npcIndex;
                    if (!Main.npc[npc].active)
                    {
                        Main.npc[npc].GetGlobalNPC<FishGlobalNPC>(mod).isHooked = 0;
                        Main.npc[npc].GetGlobalNPC<FishGlobalNPC>(mod).isSealed = 0;
                        npcIndex = -1;
                        bobsSinceAttatched = 0;
                        projectile.ai[0] = 2f;
                        updatePos = true;
                        base.AI();
                        return;
                    }
                    if(projectile.ai[0] == 1) //retracting line
                    {
                        if(Main.netMode != 2 && Main.myPlayer == projectile.owner)
                        {
                            if (Main.mouseLeft && reelTime() > 0)
                            {
                                if (timeSinceLastBob > 0)
                                    timeSinceLastBob--;

                                if (!Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).beingReeled)
                                {
                                    Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).beingReeled = true;
                                    tryMoveTarget(Main.npc[npc]);
                                }

                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                applyDamageAndDebuffs(Main.npc[npc], Main.player[projectile.owner]);
                            }
                            else
                            {
                                cummulativeSpeed = 0.0f;
                            }
                            if (Main.mouseRight)
                            {
                                breakFree();
                                if (Main.netMode != 0)
                                {
                                    /*
                                    ModPacket pk = mod.GetPacket();
                                    pk.Write((byte)2);
                                    pk.Write((ushort)(projectile.whoAmI));
                                    pk.Write(npcIndex);
                                    pk.Write(projectile.ai[0]);
                                    pk.Send();*/
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            
                            if(!Main.mouseLeft && !Main.mouseRight)
                            {
                                //Main.NewText("Reset to 0, no mouseLeft;");
                                projectile.ai[0] = 0;
                                if (Main.netMode != 0)
                                {
                                    /*
                                    ModPacket pk = mod.GetPacket();
                                    pk.Write((byte)2);
                                    pk.Write((ushort)(projectile.whoAmI));
                                    pk.Write(npcIndex);
                                    pk.Write(projectile.ai[0]);
                                    pk.Send();*/
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[projectile.owner], Main.npc[npc]);
                            }

                        }else
                        {
                            applyDamageAndDebuffs(Main.npc[npc], Main.player[projectile.owner]);
                        }
                        /*
                        else
                        {
                            projectile.ai[0] = 0;
                            checkEntityForMove(Main.player[projectile.owner], Main.npc[npc]);

                        }*/

                    }
                    else
                    {
                        checkEntityForMove(Main.player[projectile.owner], Main.npc[npc]);
                    }
                   
                    return;
                    //projectile.velocity = Main.npc[npc].velocity;
                }
                else //is stuck on player
                {
                    int player = npcIndex - Main.npc.Length;
                    if (!Main.player[player].active || Main.player[player].dead)
                    {
                        Main.player[player].GetModPlayer<FishPlayer>(mod).isHooked = 0;
                        Main.player[player].GetModPlayer<FishPlayer>(mod).isSealed = 0;
                        bobsSinceAttatched = 0;
                        npcIndex = -1;
                        projectile.ai[0] = 2f;
                        updatePos = true;
                        base.AI();
                        return;
                    }
                    if (projectile.ai[0] == 1) //retracting line
                    {
                        if (Main.netMode != 2 && Main.myPlayer == projectile.owner)
                        {
                            if (Main.mouseLeft && reelTime() > 0)
                            {
                                if (timeSinceLastBob > 0)
                                    timeSinceLastBob--;

                                if (!Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).beingReeled)
                                {
                                    Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).beingReeled = true;
                                    tryMoveTarget(Main.player[player]);
                                }
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                applyDamageAndDebuffs(Main.player[player], Main.player[projectile.owner]);
                            }
                            else
                            {
                                cummulativeSpeed = 0.0f;
                            }
                            if (Main.mouseRight)
                            {
                                breakFree();
                                if (Main.netMode != 0)
                                {
                                    /*
                                    ModPacket pk = mod.GetPacket();
                                    pk.Write((byte)2);
                                    pk.Write((ushort)(projectile.whoAmI));
                                    pk.Write(npcIndex);
                                    pk.Write(projectile.ai[0]);
                                    pk.Send();*/
                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            if (!Main.mouseLeft && !Main.mouseRight)
                            {
                                //Main.NewText("Reset to 0, no mouseLeft;");
                                projectile.ai[0] = 0;
                                //ModPacket pk = mod.GetPacket();
                                if (Main.netMode != 0)
                                {
                                    /*  pk.Write((byte)2);
                                      pk.Write((ushort)(projectile.whoAmI));
                                      pk.Write(npcIndex);
                                      pk.Write(projectile.ai[0]);
                                      pk.Send();*/

                                    NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[projectile.owner], Main.player[player]);
                            }
                        }else
                        {
                            applyDamageAndDebuffs(Main.player[player], Main.player[projectile.owner]);
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
                base.AI();
            }

        }

        public bool isStuck()
        {
            return npcIndex > -1;
        }

        public Entity getStuckEntity()
        {
            if(npcIndex < 0 || npcIndex > Main.npc.Length + Main.player.Length)
            {
                return projectile;
            }else if (npcIndex < Main.npc.Length)
            {
                return Main.npc[npcIndex];
            }else 
            {
                return Main.player[npcIndex - Main.npc.Length];
            }
        }

        private void checkIfCollideWithNPCOrPlayer()
        {
            Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active)
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    if (rectangle.Intersects(value))
                    {
                        
                       if(hitAndAttachProjectile(Main.npc[i]))
                            return;
                    }
                }
            }
            if (!Main.player[projectile.owner].hostile)
                return;

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (i != projectile.owner && Main.player[i].active && Main.player[i].hostile && (Main.player[i].team != Main.player[projectile.owner].team || Main.player[i].team == 0))
                {
                    Rectangle value = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                    if (rectangle.Intersects(value))
                    {
                        if(hitAndAttachProjectile(Main.player[i]))
                            return;
                    }
                }
            }
        }

        private bool hitAndAttachProjectile(NPC npc)
        {
            
            if (!npc.active || npc.immortal || npc.dontTakeDamage || 
                (npc.friendly && !(npc.type == NPCID.Guide && Main.player[projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[projectile.owner].killClothier))
                )
            { 
                if(npc.type != NPCID.TargetDummy)
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
            
            npcIndex = (short)npc.whoAmI;
            npc.GetGlobalNPC<FishGlobalNPC>(mod).isHooked++;
            if (Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).seals)
            {
                npc.GetGlobalNPC<FishGlobalNPC>(mod).isSealed++;
            }
            projectile.Center = npc.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                /*
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)2);
                pk.Write((ushort)(projectile.whoAmI));
                pk.Write(npcIndex);
                pk.Write(projectile.ai[0]);
                pk.Send();*/
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
            /*  if(projectile.damage > 0)
              {
                  //Main.NewText("Entered npc " + npcIndex + " into bobber " + projectile.whoAmI + " from player " + projectile.owner + ";");
                  damageNPC(npc);
              }*/
            return true;
        }

        private void damageNPC(NPC npc)
        {
            if (Main.netMode != 1 && projectile.damage > 0 && (!((npc.immortal || npc.dontTakeDamage) && npc.type != NPCID.TargetDummy)))
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod);
                int dmg = (int)Math.Round(projectile.damage * owner.bobberDamage);
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
            }
           

        }

        private bool hitAndAttachProjectile(Player p)
        {
            bool? b = PlayerHooks.CanHitPvpWithProj(projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            b = ProjectileLoader.CanHitPvp(projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            npcIndex = (short)(p.whoAmI + Main.npc.Length);
            p.GetModPlayer<FishPlayer>(mod).isHooked++;
            if (Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).seals)
            {
                p.GetModPlayer<FishPlayer>(mod).isSealed++;
            }
            projectile.Center = p.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                /*
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)2);
                pk.Write((ushort)(projectile.whoAmI));
                pk.Write(npcIndex);
                pk.Write(projectile.ai[0]);
                pk.Send();*/
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
            /* if(projectile.damage > 0)
             {
                 damagePlayer(p);
             }*/
            return true;
        }

        private void damagePlayer(Player p)
        {
            if (Main.netMode != 1 && projectile.damage > 0)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod);
                int dmg = (int)Math.Round(projectile.damage * (owner.bobberDamage + escalationBonus(owner)));
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
            FishPlayer pl = player.GetModPlayer<FishPlayer>(mod);

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
                ((NPC)e).GetGlobalNPC<FishGlobalNPC>(mod).isHooked--;
            }
            if (e is Player)
            {
                ((Player)e).GetModPlayer<FishPlayer>(mod).isHooked--;
            }

            bobsSinceAttatched = 0;

            if (Main.rand.Next(10) == 0 && !Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).destroyBobber)
            {
                npcIndex = -1;
                projectile.ai[0] = 1f;
                updatePos = true;
            }
            else
            {
                npcIndex = -1;
                projectile.ai[0] = 2f;
                updatePos = true;
            }
        }

        private void tryMoveTarget(Entity target)
        {
            
            Vector2 vel = Main.player[projectile.owner].position - target.position; //new Vector2(target.velocity.X, target.velocity.Y);
            Vector2 vel2 = target.velocity - Main.player[projectile.owner].velocity;
            
            if(!Main.player[projectile.owner].accFishingLine && vel2.Length() > TensileStrength())
            {
                // Main.NewText("MoveTarget: no move;");
                breakFree();
            }
           // Main.NewText("MoveTarget: actually move;");

            vel.Normalize();
            if(reelTime() > 0) {
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
            if (target.height * target.width > Main.player[projectile.owner].width * Main.player[projectile.owner].height*sizeMult() || (target is NPC && ((NPC)target).type == NPCID.TargetDummy))
            {
                
                Main.player[projectile.owner].position -= (Main.player[projectile.owner].velocity + vel);
                Tile t = Main.tile[(int)(Main.player[projectile.owner].position.X / 16), (int)(Main.player[projectile.owner].position.Y / 16)];
                if (t != null && t.active() && (Main.tileSolid[t.type]))
                {
                    Main.player[projectile.owner].position += Main.player[projectile.owner].velocity +vel;
                }
                if (Main.netMode != 0)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)7);
                    pk.Write(projectile.owner);
                    pk.Write(Main.player[projectile.owner].Center.X);
                    pk.Write(Main.player[projectile.owner].Center.Y);
                 //   pk.Write((float)Main.player[projectile.owner].velocity.X);
                 //   pk.Write((float)Main.player[projectile.owner].velocity.Y);
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
                if(Main.netMode != 0)
                {
                    ModPacket pk = mod.GetPacket();
                    pk.Write((byte)6);
                    pk.Write(npcIndex);
                    pk.Write((float)target.Center.X);
                    pk.Write((float)target.Center.Y);
                   // pk.Write((float)target.velocity.X);
                   // pk.Write((float)target.velocity.Y);
                    pk.Send();
                }
            }
            updatePos = false;
            projectile.Center = target.Center;
            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        private float sizeMult()
        {
            return sizeMultiplier * Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod).sizeMultiplierMultiplier;
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
            if(npcIndex >= 0) {
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

        public virtual float TensileStrength()
        {
            return 10f;
        }

        public override bool ShouldUpdatePosition()
        {
            return updatePos;
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
                      /*      Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
                        if (type == 2294)
                        {
                            color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(100, 180, 230, 100));
                        }
                        if (type == 2295)
                        {
                            color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(250, 90, 70, 100));
                        }
                        if (type == 2293)
                        {
                            color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(203, 190, 210, 100));
                        }
                        if (type == 2421)
                        {
                            color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(183, 77, 112, 100));
                        }
                        if (type == 2422)
                        {
                            color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(255, 226, 116, 100));
                        }*/
                        Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Rectangle?(new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num6)), color, rotation, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            return false;
        }
        /*
        public override bool? CanHitNPC(NPC target)
        {
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            npcIndex = target.whoAmI;
            projectile.Center = target.Center;
            updatePos = false;
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            npcIndex = target.whoAmI + Main.npc.Length;
            projectile.Center = target.Center;
            updatePos = false;
        }*/

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

        

        public virtual void applyDamageAndDebuffs(NPC npc, Player player)
        {
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
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod);
                if (owner != null)
                {
                    int ans = (int)Math.Round(timeBobMax  - timeBobMax * owner.bobberSpeed);
                    return (short)(ans <= 1 ? 1 : ans);
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
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>(mod);
                if (owner != null)
                {
                    int ans = (int)Math.Round(timeReelMax - timeReelMax * owner.reelSpeed);
                    return (short)(ans <= 1 ? 1 : ans);
                }
            }
            return timeReelMax;
        }
    }
}