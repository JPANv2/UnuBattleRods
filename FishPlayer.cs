using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Rods;
using UnuBattleRods.Projectiles;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods
{
    public class FishPlayer: ModPlayer
    {


        public float bobberDamage = 1.0f;
        public int bobberCrit = 0;
        public float bobberSpeed = 0.0f;
        public float sizeMultiplierMultiplier = 1.0f;
        public float reelSpeed = 0.0f;

        public bool beingReeled = false;
        public int multilineFishing = 0;

        public int isHooked = 0;
        public int isSealed = 0;

        public int stardustCells = 0;

        public bool seals = false;
        public bool wormicide = false;

        public bool escalation = false;
        public float escalationBonus = 0.0f;
        public float escalationMax = 1.0f;
        public bool escalationFromMana = false;
        public float escalationFromManaMax = 1.0f;
        public float escalationFromManaBonus = 0.0f;
        public int escalationManaCost = 0;
        public int escalationTimer = 0;
        
        public bool redirectThorns = false;
        public bool linkDamage = false;
        public float linkedDamage = 0f;

        public float vampiricLinePercent = 0.0f;
        public float syphonLinePercent = 0.0f;

        public bool maxCrate = false;

        public int mimicX;
        public int mimicY;
        public bool mimicToSpawn = false;

        public bool destroyBobber = false;
        public bool aimBobber = false;
        public float bobberShootSpeed = 1.0f;
        public bool manaShield = false;
        public float manaShieldPercentage = 0.0f;
        public int projectileDestroyPercentage = 0;


        /*actually per 10000, meaning 1 = 0.01% chance*/
        public int cratePercent = 0;
        public int moneyPercent = 0;
        public int questPercent = 0;

        public float knifeRadius = 0f;
        public int knifeBaseDamage = 0;
        public float knifeKnockback = 0f;
        public int knifeCooldown = 0;
        public int knifeCooldownCounter = 0;
        public int knifeDebuff = 0;

        public override void ResetEffects()
        {
            bobberDamage = 1.0f;
            bobberSpeed = 0.0f;
            bobberCrit = 0;
            reelSpeed = 0.0f;
            sizeMultiplierMultiplier = 1.0f;
            beingReeled = false;
            multilineFishing = 0;
            seals = false;
            wormicide = false;
            redirectThorns = false;
            linkDamage = false;
            linkedDamage = 0f;
            escalation = false;
            escalationBonus = 0.0f;
            escalationFromManaBonus = 0.0f;
            escalationMax = 1.0f;
            escalationFromManaMax = 1.0f;
            escalationFromMana = false;
            escalationManaCost = 0;
            if (player.FindBuffIndex(BuffID.StardustMinion) < 0)
            {
                stardustCells = 0;
            }
            destroyBobber = false;
            aimBobber = false;
            bobberShootSpeed = 1.0f;
            manaShield = false;
            manaShieldPercentage = 0.0f;
            manaShieldPercentageActual = 0.0f;
            projectileDestroyPercentage = 0;
            vampiricLinePercent = 0.0f;
            syphonLinePercent = 0.0f;

            maxCrate = false;

            cratePercent = 0;
            moneyPercent = 0;
            questPercent = 0;

            knifeRadius = 0f;
            knifeBaseDamage = 0;
            knifeKnockback = 0f;
            knifeCooldown = 0;
            knifeDebuff = 0;
            base.ResetEffects();
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if(isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }

           /* if (target.GetModPlayer<FishPlayer>(mod).linkDamage)
            {
                activateLinkDamage(target, damage, crit);
            }*/

        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }
        }
        
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            FishProjectileInfo info = proj.GetGlobalProjectile<FishProjectileInfo>(mod);
            if (!info.hasBeenCalculated)
            {
                if (hasEscalationBobber())
                {
                    info.isDodged = projectileDestroyPercentage > Main.rand.Next(10000);
                }
                info.hasBeenCalculated = true;
            }
            if(proj.hostile && info.isDodged)
            {
                //proj.damage = 0;
                return false;
            }
            return true;
        }

        
        float manaShieldPercentageActual = 0f;
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            manaShieldDamageReduction(ref damage);
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (manaShield && manaShieldPercentageActual != 0f)
            {
                player.endurance -= manaShieldPercentageActual;
            }
            if (linkDamage)
            {
                activateLinkDamage(player, damage, crit);
            }
            base.PostHurt(pvp, quiet, damage, hitDirection, crit);
        }

        public override bool PreItemCheck()
        {

            return base.PreItemCheck();
        }

        public override void PostItemCheck()
        {
            base.PostItemCheck();
        }

        private void manaShieldDamageReduction(ref int damage)
        {
            if (manaShield && hasEscalationBobber() && manaShieldPercentage != 0f && player.statMana > 0)
            {
                double realDamage = Main.CalculatePlayerDamage(damage, player.statDefense);
                int mana = player.statMana;
                int redirectDamageMax = (int)(realDamage * manaShieldPercentage);

                if (mana < redirectDamageMax)
                {
                    manaShieldPercentageActual = (float)(redirectDamageMax / mana);
                    redirectDamageMax = (int)(realDamage * manaShieldPercentageActual);
                }
                else
                {
                    manaShieldPercentageActual = manaShieldPercentage;
                }
                player.endurance += manaShieldPercentageActual;
                player.statMana -= redirectDamageMax;
            }
        }

        private void activateLinkDamage(Player p, double damage, bool crit)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == p.whoAmI && Main.projectile[i].modProjectile != null && Main.projectile[i].modProjectile is Bobber)
                {
                    Bobber b = Main.projectile[i].modProjectile as Bobber;
                    if (b.npcIndex > 0 && b.npcIndex < Main.npc.Length)
                    {
                        p.ApplyDamageToNPC(Main.npc[i], (int)(damage * linkedDamage), 0, 1, crit);
                    }
                    else if (b.npcIndex > Main.npc.Length && b.npcIndex < Main.npc.Length + Main.player.Length)
                    {
                        Player foe = Main.player[b.npcIndex - Main.npc.Length];
                        foe.Hurt(PlayerDeathReason.ByPlayer(p.whoAmI), (int)(damage * linkedDamage), 0, true, false, crit);
                    }
                }
            }
        }

        public override void GetFishingLevel(Item fishingRod, Item bait, ref int fishingLevel)
        {
            //Bee fishing rod: if fishing in honey, + fishingRod.fishinglevel
            if(fishingRod.type == mod.ItemType("BeeBattlerod"))
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].modProjectile != null
                        && Main.projectile[i].modProjectile is Projectiles.Bobbers.NormalMode.BeeBobber && Main.projectile[i].honeyWet)
                    {
                        fishingLevel += fishingRod.fishingPole;
                        return;
                    }
                }              
            }
        }
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            bool cb = false;
            for(int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                {
                    if (Main.projectile[i].type == mod.ProjectileType("CorruptBobber") & !cb && ((Bobber)(Main.projectile[i].modProjectile)).isStuck())
                    {
                        player.moveSpeed += 0.15f;
                        cb = true;
                    }
                   
                }
            }
            base.UpdateEquips(ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
        }

        public override void PreUpdate()
        {
            if (mimicToSpawn)
            {
                if (Main.netMode != 1)
                {
                    int num8 = NPC.NewNPC(mimicX, mimicY, mod.NPCType("CrateMimic"), 1, 0f, 0f, 0f, 0f, 255);
                    if (num8 < 200)
                    {
                        Main.npc[num8].timeLeft *= 20;
                        Main.npc[num8].whoAmI = num8;
                        NetMessage.SendData(23, -1, -1, null, num8, 0f, 0f, 0f, 0, 0, 0);
                        Main.npc[num8].BigMimicSpawnSmoke();
                    }
                    mimicToSpawn = false;
                }
                else
                {
                    ModPacket p = mod.GetPacket();
                    p.Write((byte)1);
                    p.Write((ushort)player.whoAmI);
                    p.Write(mimicX);
                    p.Write(mimicY);
                    mimicToSpawn = false;
                    p.Send();
                }
            }
        }

        public List<Bobber> getOwnedAttatchedBobbers()
        {
            List<Bobber> ans = new List<Bobber>();
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].modProjectile != null)
                {
                    Bobber b = Main.projectile[i].modProjectile as Bobber;
                    if (b != null && b.npcIndex > -1)
                    {
                        ans.Add(b);
                    }
                }
            }
            return ans;
        }

        public bool hasEscalationBobber()
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if(Main.projectile[i].owner == player.whoAmI && Main.projectile[i].modProjectile != null)
                {
                    Bobber b = Main.projectile[i].modProjectile as Bobber;
                    if(b != null && b.npcIndex > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void PostUpdate()
        {
            updateLinkDamage();
            if (escalationFromMana && hasEscalationBobber())
            {
                escalationTimer++;
                if (player.statMana >= escalationManaCost)
                {
                    escalationBonus += escalationFromManaBonus;
                    escalationMax = Math.Max(escalationMax, escalationFromManaMax);
                }
                if (escalationTimer % 60 == 0)
                {
                    player.statMana = Math.Max(player.statMana - escalationManaCost, 0);
                }
            }
            else
            {
                escalationTimer = 0;
            }
            int slot = getLargestDamageBonus();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (knifeCooldown > 0 && knifeBaseDamage > 0 && knifeRadius > 0)
                {
                    if (knifeCooldownCounter > 0)
                    {
                        knifeCooldownCounter--;
                    }
                    if (knifeCooldownCounter <= 0)
                    {
                        if (findAndDamageNearbyEnemies())
                        {
                            knifeCooldownCounter = knifeCooldown;
                        }
                    }
                }
            }
        }

        private bool findAndDamageNearbyEnemies()
        {
            List<Bobber> lst = getOwnedAttatchedBobbers();
            bool[] npcToIgnore = new bool[Main.npc.Length];
            bool foundEnemy = false;
            //bool[] playersToIgnore = new bool[Main.player.Length]; players not included

            float radiusOffset = knifeRadius + (player.width > player.height ? player.width : player.height) / 2;

            foreach (Bobber b in lst)
            {
                Entity e = b.getStuckEntity();
                if(e is NPC)
                {
                    NPC n = e as NPC;
                    if((!n.friendly || (n.type == NPCID.Guide && player.killGuide) || (n.type == NPCID.Clothier && player.killClothier)) &&
                        !((n.immortal || n.dontTakeDamage) && n.type != NPCID.TargetDummy))
                    {
                        
                        float distance = Vector2.Distance(n.Center, player.Center);

                        if (distance <= radiusOffset)
                        {
                            knifeDamageNpc(n, knifeBaseDamage * 2);
                            if(knifeDebuff > 0)
                            {
                                n.AddBuff(knifeDebuff, knifeCooldown);
                            }
                            npcToIgnore[n.whoAmI] = true;
                            foundEnemy = true;
                        }
                    }
                }
            }

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC n = Main.npc[i];
                if (!npcToIgnore[i] && ((!n.friendly || (n.type == NPCID.Guide && player.killGuide) || (n.type == NPCID.Clothier && player.killClothier)) &&
                        !((n.immortal || n.dontTakeDamage) && n.type != NPCID.TargetDummy)) &&
                        n.catchItem <= 0) { 
                    float distance = Vector2.Distance(n.Center, player.Center);

                    if (distance <= radiusOffset)
                    {
                        knifeDamageNpc(n, knifeBaseDamage);
                        if (knifeDebuff > 0)
                        {
                            n.AddBuff(knifeDebuff, knifeCooldown);
                        }
                        foundEnemy = true;
                    }
                }
            }

            return foundEnemy;      
        }

        private void knifeDamageNpc(NPC npc, int damage)
        {
            int dmg = Main.DamageVar(damage);

            if (dmg < 1)
                dmg = 1;

            int num18 = Item.NPCtoBanner(npc.BannerID());
            if (num18 >= 0)
            {
                player.lastCreatureHit = num18;
                if (Main.netMode != 2 && player.NPCBannerBuff[num18])
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
            float knockback = knifeKnockback;
            bool crit = false;
            int dir = player.position.X > npc.position.X ? -1 : 1;

            player.OnHit(npc.Center.X, npc.Center.Y, npc);

            if (player.armorPenetration > 0)
            {
                dmg += npc.checkArmorPenetration(player.armorPenetration);
            }
            int num25 = (int)npc.StrikeNPC(dmg, knockback, dir, crit, false, false);
            if (Main.netMode == 2)
            {
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)3);
                pk.Write(num25);
                pk.Send(player.whoAmI, -1);
            }
            else
            {
                if (player.accDreamCatcher)
                {
                    player.addDPS(num25);
                }
            }

            if (Main.netMode != 0)
            {
               NetMessage.SendData(28, -1, -1, null, npc.whoAmI, (float)dmg, knockback, (float)dir, 0, 0, 0);
            }
        }

        private void updateLinkDamage()
        {
            if (linkDamage)
            {
                linkedDamage += player.thorns;
                if (player.turtleThorns)
                    linkedDamage += 1f;
                if (redirectThorns)
                {
                    player.turtleThorns = false;
                    player.thorns = 0f;
                }
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.FindBuffIndex(mod.BuffType("Frostfire")) >= 0)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 20;
            }else  if (player.FindBuffIndex(mod.BuffType("Solarfire")) >= 0)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 64;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (player.FindBuffIndex(mod.BuffType("Frostfire")) >= 0)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 135, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.1f;
                g *= 0.2f;
                b *= 0.7f;
                fullBright = true;
            }else if (player.FindBuffIndex(mod.BuffType("Solarfire")) >= 0)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int num41 = Dust.NewDust(new Vector2(drawInfo.position.X - 2f, drawInfo.position.Y - 2f), player.width + 4, player.height + 4, 6, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Microsoft.Xna.Framework.Color), 3f);
                    Main.dust[num41].noGravity = true;
                    Dust dust2 = Main.dust[num41];
                    dust2.velocity *= 1.8f;
                    Dust dust3 = Main.dust[num41];
                    dust3.velocity.Y = dust3.velocity.Y - 0.5f;
                    Main.playerDrawDust.Add(num41);
                }
                g *= 0.6f;
                b *= 0.7f;
                fullBright = true;
            }


        }




        private void updateRodFromSlot(Item item, int slot)
        {
            switch (slot)
            {
                case 0:
                    item.melee = true;
                    item.ranged = false;
                    item.magic = false;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 1:
                    item.melee = false;
                    item.ranged = true;
                    item.magic = false;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 2:
                    item.melee = false;
                    item.ranged = false;
                    item.magic = true;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 3:
                    item.melee = false;
                    item.ranged = false;
                    item.magic = false;
                    item.summon = true;
                    item.thrown = false;
                    break;
                default:
                    item.melee = false;
                    item.ranged = false;
                    item.magic = false;
                    item.summon = false;
                    item.thrown = true;
                    break;
            }
        }

        private int getLargestDamageBonus()
        {
            int ans = 1;
            float dmg = player.rangedDamage;
            int crit = player.rangedCrit;

            
            float dmg2 = player.arrowDamage;
            if(player.bulletDamage > dmg2)
            {
                dmg2 = player.bulletDamage;
            }
            if(player.rocketDamage > dmg2)
            {
                dmg2 = player.rocketDamage;
            }
            dmg += dmg2 - 1.0f;
          
            if (player.meleeDamage > dmg)
            {
                ans = 0;
                dmg = player.meleeDamage;
                crit = player.meleeCrit;
            }

            if ( player.magicDamage > dmg)
            {
                ans = 2;
                dmg = player.magicDamage;
                crit = player.magicCrit;
            }
            if (player.minionDamage > dmg)
            {
                ans = 3;
                dmg = player.minionDamage;
                crit = 0;
            }
            if (player.thrownDamage > dmg)
            {
                ans = 4;
                dmg = player.thrownDamage;
                crit = player.thrownCrit;
            }

            bobberDamage += dmg - 1.0f;
            bobberCrit += crit;
            return ans;
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
                return;

            if(canReplaceFish(caughtType))
            {
                if((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(24) == 0 || (caughtType == ItemID.WoodenCrate && Main.rand.Next(12) == 0))
                {
                    replaceWithRodCrate(fishingRod, liquidType, ref caughtType);
                    return;
                }
                if((maxCrate && Main.rand.Next(6) == 0) || (!Main.hardMode && Main.rand.Next(32) == 0) || Main.rand.Next(64) == 0)
                {
                    caughtType = mod.ItemType("MimicCrate");
                    return;
                }
                if(Main.hardMode && (player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly) && ((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(12) == 0))
                {
                    caughtType = mod.ItemType("SoulCrate");
                    return;
                }
                if(((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(6) == 0) && FishWorld.graniteTiles > 75)
                {
                    caughtType = mod.ItemType("GraniteCrate");
                    return;

                }
                if (((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(6) == 0) && FishWorld.marbleTiles > 75)
                {
                    caughtType = mod.ItemType("MarbleCrate");
                    return;
                }
                if (maxCrate && Main.rand.Next(3) == 0)
                {
                    if(Main.rand.Next(30) == 0)
                    {
                        caughtType = ItemID.GoldenCrate;
                        return;
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            caughtType = ItemID.IronCrate;
                            return;
                        }
                        if (player.ZoneDungeon)
                        {
                            caughtType = ItemID.DungeonFishingCrate;
                            return;
                        }
                        if (player.Center.Y < Main.worldSurface * 0.5)
                        {
                            caughtType = ItemID.FloatingIslandFishingCrate;
                            return;
                        }
                        if (player.ZoneHoly)
                        {
                            if (player.ZoneCrimson)
                            {
                                if (player.ZoneCorrupt)
                                {
                                    switch (Main.rand.Next(3))
                                    {
                                        case 0:
                                            caughtType = ItemID.HallowedFishingCrate;
                                            return;
                                        case 1:
                                            caughtType = ItemID.CorruptFishingCrate;
                                            return;
                                        default:
                                            caughtType = ItemID.CrimsonFishingCrate;
                                            return;
                                    }
                                }
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        caughtType = ItemID.HallowedFishingCrate;
                                        return;
                                    default:
                                        caughtType = ItemID.CrimsonFishingCrate;
                                        return;
                                }
                            }
                            if (player.ZoneCorrupt)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        caughtType = ItemID.HallowedFishingCrate;
                                        return;
                                    default:
                                        caughtType = ItemID.CorruptFishingCrate;
                                        return;
                                }
                            }
                            caughtType = ItemID.HallowedFishingCrate;
                            return;
                        }
                        if (player.ZoneCrimson)
                        {
                            if (player.ZoneCorrupt)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        caughtType = ItemID.CrimsonFishingCrate;
                                        return;
                                    default:
                                        caughtType = ItemID.CorruptFishingCrate;
                                        return;
                                }
                            }
                            caughtType = ItemID.CrimsonFishingCrate;
                            return;
                        }
                        if (player.ZoneCorrupt)
                        {
                            caughtType = ItemID.CorruptFishingCrate;
                            return;
                        }
                        if (player.ZoneJungle)
                        {
                            caughtType = ItemID.JungleFishingCrate;
                            return;
                        }
                    }
                    caughtType = ItemID.WoodenCrate;
                    return;
                }
            }
        }

        public void replaceWithRodCrate(Item fishingRod, int liquid, ref int caughtType)
        {
            if((liquid == 2 || liquid == -1) && fishingRod.type == mod.ItemType("BeeBattlerod"))
            {
                caughtType = mod.ItemType("BeeCrate");
                return;
            }
            if ((liquid == 1 || liquid == -1)&& fishingRod.type == mod.ItemType("HellstoneBattlerod"))
            {
                caughtType = mod.ItemType("ObsidianCrate");
                return;
            }
            if (fishingRod.type == mod.ItemType("VortexBattlerod")|| fishingRod.type == mod.ItemType("SolarBattlerod") || fishingRod.type == mod.ItemType("NebulaBattlerod") || fishingRod.type == mod.ItemType("StardustBattlerod"))
            {
                caughtType = mod.ItemType("LuminiteCrate");
                return;
            }
            if (fishingRod.type == mod.ItemType("MeteorBattlerod"))
            {
                caughtType = mod.ItemType("MeteorCrate");
                return;
            }
            if (fishingRod.type == mod.ItemType("HallowedBattlerod"))
            {
                caughtType = mod.ItemType("HallowedCrate");
                return;
            }
            if(fishingRod.type == mod.ItemType("EvilRodOfDarkness"))
            {
                if(Main.rand.Next(2) == 0)
                {
                    caughtType = mod.ItemType("SoulCrate");
                    return;
                }
                caughtType = mod.ItemType("CorruptCrate");
                return;
            }

            if (fishingRod.type == mod.ItemType("EvilRodOfBlood"))
            {
                if (Main.rand.Next(2) == 0)
                {
                    caughtType = mod.ItemType("SoulCrate");
                    return;
                }
                caughtType = mod.ItemType("CrimsonCrate");
                return;
            }

            if (fishingRod.type == mod.ItemType("CorruptBattlerod"))
            {
                caughtType = mod.ItemType("CorruptCrate");
                return;
            }
            if (fishingRod.type == mod.ItemType("CrimsonBattlerod"))
            {
                caughtType = mod.ItemType("CrimsonCrate");
                return;
            }

            if(fishingRod.type == mod.ItemType("LifeforceBattlerod"))
            {
                switch (Main.rand.Next(3)) { 
                    case 0:
                        caughtType = mod.ItemType("ShroomiteCrate");
                        return;
                    case 1:
                        caughtType = mod.ItemType("ChlorophyteCrate");
                        return;
                    default:
                        caughtType = mod.ItemType("SoulCrate");
                        return;
                }
            }

            if (fishingRod.type == mod.ItemType("ShroomiteBattlerod"))
            {
                caughtType = mod.ItemType("ShroomiteCrate");
                return;
            }
            if (fishingRod.type == mod.ItemType("SpectreBattlerod"))
            {
                caughtType = mod.ItemType("SoulCrate");
                return;
            }

            if (fishingRod.type == mod.ItemType("TerraBattlerod"))
            {
                caughtType = mod.ItemType("TerraCrate");
                return;
            }

            if (fishingRod.type == mod.ItemType("DungeonBattlerod"))
            {
                caughtType = ItemID.DungeonFishingCrate;
                return;
            }

            if (fishingRod.type == mod.ItemType("ChlorophyteBattlerod") || fishingRod.type == mod.ItemType("TurtleBattlerod") || fishingRod.type == mod.ItemType("BeetleBattlerod"))
            {
                caughtType = mod.ItemType("ChlorophyteCrate");
                return;
            }

            if (fishingRod.type == mod.ItemType("SpookyBattlerod"))
            {
                caughtType = mod.ItemType("SpookyCrate");
                return;
            }
        }

        public static bool canReplaceFish(int fishFound)
        {
            return fishFound == ItemID.WoodenCrate || fishFound == ItemID.Bass || fishFound == ItemID.Salmon || fishFound == ItemID.AtlanticCod ||
                fishFound == ItemID.Obsidifish || fishFound == ItemID.NeonTetra || fishFound == ItemID.RedSnapper || fishFound == ItemID.Trout || fishFound == ItemID.Tuna;


        }
    }
}
