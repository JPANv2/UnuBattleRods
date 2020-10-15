using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using UnuBattleRods.Buffs;
using UnuBattleRods.Items.Discardables;
using UnuBattleRods.Items.Armors.NormalMode;
using UnuBattleRods.Items.Baits;
using UnuBattleRods.Items.Baits.BuffBaits;
using UnuBattleRods.Items.Baits.DebuffBaits;
using UnuBattleRods.Items.Baits.SummonBaits;
using UnuBattleRods.Items.Crates;
using UnuBattleRods.Items.Materials;
using UnuBattleRods.Items.Rods;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.NPCs;
using UnuBattleRods.Projectiles;
using UnuBattleRods.Projectiles.Bobbers;
using UnuBattleRods.Buffs.Minion;
using UnuBattleRods.Items.Currency;
using UnuBattleRods.Configs;

namespace UnuBattleRods
{
    public class FishPlayer: ModPlayer
    {

        public bool MasterBaiter = false;


        public float bobberDamage = 1.0f;
        public float perBobberDamage = 1.0f;
        public int bobberCrit = 0;
        public float bobberSpeed = 0.0f;
        public float sizeMultiplierMultiplier = 1.0f;
        public float reelSpeed = 0.0f;

        public bool beingReeled = false;
        public int multilineFishing = 0;
        public bool sinkBobber = false;

        public int isHooked = 0;
        public int isSealed = 0;

        public int stardustCells = 0;
        public bool frostFire = false;
        public bool solarFire = false;

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

        public bool fireBees = false;
        public int fireBeesCooldown = 0;
        public int fireBeesCooldownCounter = 0;

        public float knifeRadius = 0f;
        public int knifeBaseDamage = 0;
        public float knifeKnockback = 0f;
        public int knifeCooldown = 0;
        public int knifeCooldownCounter = 0;
        public int knifeDebuff = 0;

        public bool lifeforceArmorEffect = false;
        public bool fractaliteArmorEffect = false;
        public bool wormSpawner = false;

        public int[] baitBuff = null;
        public int[] baitDebuff = null;       
        public int baitTimer = 0;
        public List<int> debuffsPresent = new List<int>();

        public Discardable currentDiscard = null;
        public int maxBobbersPerEnemy = -1;
        public bool smartBobberDistribution = false;
        public int smartBobberRange = 64;

        public int baitDispersalRange = 0;

        public bool fallOnFloor = true;
        public int fallOnFloorPercentage = 10;

        public bool bobbersCatchItems = false;

        public bool buddyfish = false;

        public bool fishSlicer = false;
        public bool sellGate = false;


        public int fishedAmount = 0;

        public override void ResetEffects()
        {
            buddyfish = false;
            if (baitBuff == null)
            {
                baitBuff = new int[] { -1, -1, -1, -1 };
            }
            if (baitDebuff == null)
            {
                baitDebuff = new int[] { -1, -1, -1, -1 };
            }

            if(player.FindBuffIndex(ModContent.BuffType<PoweredBaitBuff>()) < 0)
            {
                baitDebuff = new int[] { -1, -1, -1, -1 };
                baitBuff = new int[] { -1, -1, -1, -1 };
            }
            if (player.FindBuffIndex(ModContent.BuffType<PoweredBaitDebuff>()) < 0)
            {
                debuffsPresent.Clear();
            }

            bobberDamage = 1.0f;
            perBobberDamage = 1.0f;
            bobberSpeed = 0.0f;
            bobberCrit = 0;
            reelSpeed = 0.0f;
            sizeMultiplierMultiplier = 1.0f;
            beingReeled = false;
            multilineFishing = 0;
            sinkBobber = false;
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
            if (player.FindBuffIndex(ModContent.BuffType<Buddyfish>()) < 0 && !buddyfish)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == player.whoAmI)
                    {
                        if (Main.projectile[i].modProjectile as Projectiles.Minions.Buddyfish != null)
                        {
                            Main.projectile[i].timeLeft = 0;
                            Main.projectile[i].Kill();
                        }
                    }
                 }
                buddyfish = false;
            }
            frostFire = false;
            solarFire = false;

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

            fireBees = false;
            fireBeesCooldown = 0;

            knifeRadius = 0f;
            knifeBaseDamage = 0;
            knifeKnockback = 0f;
            knifeCooldown = 0;
            knifeDebuff = 0;

            lifeforceArmorEffect = false;
            fractaliteArmorEffect = false;
            wormSpawner = false;

            currentDiscard = null;
            maxBobbersPerEnemy = -1;
            smartBobberDistribution = false;
            smartBobberRange = 64;
            fallOnFloor = !ModContent.GetInstance<UnuServerConfig>().dontFallOnFloor;
            fallOnFloorPercentage = 10;
            baitDispersalRange = 0;
            bobbersCatchItems = false;

            fishSlicer = false;
            sellGate = false;
            if(player.HeldItem.modItem != null && player.HeldItem.modItem is BattleRod)
            {
                player.HeldItem.autoReuse = false;
            }
            base.ResetEffects();
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumCoreDeath)
        {
            if (ModContent.GetInstance<UnuPlayerConfig>().startWithRod)
            {
                Item rod = new Item();
                rod.SetDefaults(ModContent.ItemType<WoodenBattlerod>());
                items.Add(rod);
            }
            if (ModContent.GetInstance<UnuPlayerConfig>().startWithBait)
            {
                Item bait = new Item();
                bait.SetDefaults(ModContent.ItemType<PoisonApprenticeBait>());
                bait.stack = 10;
                items.Add(bait);
            }
        }

        public override TagCompound Save()
        {
            TagCompound ans = base.Save();
            if (ans == null)
                ans = new TagCompound();
            if(MasterBaiter)
                ans["baiter"] = MasterBaiter;
            return ans;
        }

        public override void Load(TagCompound tag)
        {
            if (tag.ContainsKey("baiter"))
            {
                MasterBaiter = true;
            }
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if(isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }

           /* if (target.GetModPlayer<FishPlayer>().linkDamage)
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
            if (player.armor[0].type == ModContent.ItemType<FlinxHat>())
            {
                if (Main.hardMode)
                {
                    knockback *= 10;
                }
                else
                {
                    knockback *= 2;
                }
            }

        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (isSealed > 0)
            {
                damage = (int)(damage * 0.8f);
            }
            if (player.armor[0].type == ModContent.ItemType<FlinxHat>())
            {
                if (Main.hardMode)
                {
                    knockback *= 10;
                }
                else
                {
                    knockback *= 2;
                }
            }
        }
        
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            FishProjectileInfo info = proj.GetGlobalProjectile<FishProjectileInfo>();
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
                if(redirectDamageMax > 1)
                {
                    player.statMana -= redirectDamageMax;
                    player.manaRegenDelay = (int)player.maxRegenDelay;
                }
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

        public override void PostUpdateEquips()
        {
            int slot = getLargestDamageBonus();
            updateLinkDamage();
            if (escalationFromMana && hasEscalationBobber())
            {
                escalationTimer++;
                if (player.statMana >= (int)((float)escalationManaCost * player.manaCost))
                {
                    escalationBonus += escalationFromManaBonus;
                    escalationMax = Math.Max(escalationMax, escalationFromManaMax);
                    if (escalationTimer % 60 == 0)
                    {
                        player.manaRegenDelay = (int)player.maxRegenDelay;
                        player.statMana -= (int)((float)escalationManaCost * player.manaCost);
                    }
                }
                else
                {
                    escalationTimer = 0;
                }
            }
            else
            {
                escalationTimer = 0;
            }

            if (fractaliteArmorEffect)
            {
                if (player.wingsLogic == 0)
                {
                    //player.wings = 29;
                    player.wingsLogic = 29;
                    player.wingTimeMax += 180;
                }else
                {
                    player.wingTimeMax *= 2;
                }
            }
        }

        public Vector2 newSpeed = Vector2.Zero;
        public Vector2 newCenter = new Vector2(-10000, -10000);


        public override void PostUpdate()
        {
            int pbtype = ModContent.BuffType<PoweredBaitBuff>();
            int baitBuffIdx = player.FindBuffIndex(pbtype);
            if (baitBuffIdx < 0)
            {
                baitTimer = 0;
                
                for (int i = 0; i < baitBuff.Length; i++)
                {
                   baitBuff[i] = -1;
                }
            }
            

            if (newCenter.X > -10000 && newCenter.Y > -10000)
            {
                //Main.NewText("Position: " + newCenter.X +" : " + newCenter.Y + " ;");
                if (WorldGen.InWorld((int)(newCenter.X / 16.0f), (int)(newCenter.Y / 16.0f))){
                    player.Center = new Vector2(newCenter.X, newCenter.Y);
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].modProjectile != null)
                        {
                            Bobber b = Main.projectile[i].modProjectile as Bobber;
                            if (b != null && ((b.npcIndex - Main.npc.Length) == player.whoAmI))
                            {
                                b.projectile.Center = new Vector2(newCenter.X, newCenter.Y);
                            }
                        }
                    }
                }
               // player.velocity = new Vector2(newSpeed.X, newSpeed.Y);
                newCenter = new Vector2(-10000, -10000);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (fireBees && fireBeesCooldown > 0)
                {
                    if (fireBeesCooldownCounter > 0)
                        fireBeesCooldownCounter--;
                    if (fireBeesCooldownCounter <= 0)
                    {
                        fireFireBees();
                        fireBeesCooldownCounter = fireBeesCooldown - Main.rand.Next(120);
                    }
                }

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
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                checkForMimicLikeSpawns();
            }
        }

        private void fireFireBees()
        {
            int max = Main.rand.Next(1, 3);
            for (int i = 0; i < max; i++)
            {
                int proj = ModContent.ProjectileType<FireBee>();
                float kb = player.beeKB(4.0f);
                int dmg = player.beeDamage(player.HeldItem.damage > 0 ? player.HeldItem.damage : 20);

                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(player.Center.X, player.Center.Y);
                int size = player.height;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
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
                            n.PlayerInteraction(player.whoAmI);                            
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

            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                NPC n = Main.npc[i];
                if (!npcToIgnore[i] && ((!n.friendly || (n.type == NPCID.Guide && player.killGuide) || (n.type == NPCID.Clothier && player.killClothier)) &&
                        !((n.immortal || n.dontTakeDamage) && n.type != NPCID.TargetDummy)) &&
                        n.catchItem <= 0) { 
                    float distance = Vector2.Distance(n.Center, player.Center);

                    if (distance <= radiusOffset)
                    {
                        n.PlayerInteraction(player.whoAmI);
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
            if (frostFire)//player.FindBuffIndex(mod.BuffType("Frostfire")) >= 0)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 20;
            }
            if (solarFire)//player.FindBuffIndex(mod.BuffType("Solarfire")) >= 0)
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
            if (frostFire)
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
            }else if (solarFire)
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
            if (sellGate && !ModContent.GetInstance<UnuServerConfig>().noSellItems.Contains(new Terraria.ModLoader.Config.ItemDefinition(caughtType)))
            {
                Item itm = new Item();
                itm.SetDefaults(caughtType, false);
                if (itm.maxStack == 1 || ModContent.GetInstance<UnuServerConfig>().forceSellItems.Contains(new Terraria.ModLoader.Config.ItemDefinition(caughtType)))
                {
                    caughtType = ItemID.CopperCoin;
                    if(itm.type == ItemID.BombFish)
                    {
                        int num9 = player.FishingLevel();
                        int minValue = (num9 / 20 + 3) / 2;
                        int num10 = (num9 / 10 + 6) / 2;
                        if (Main.rand.Next(50) < num9)
                        {
                            num10++;
                        }
                        if (Main.rand.Next(100) < num9)
                        {
                            num10++;
                        }
                        if (Main.rand.Next(150) < num9)
                        {
                            num10++;
                        }
                        if (Main.rand.Next(200) < num9)
                        {
                            num10++;
                        }
                        int stack = Main.rand.Next(minValue, num10 + 1);
                        fishedAmount = (itm.value*stack) / 5;
                    }
                    else if (itm.type == ItemID.FrostDaggerfish)
                    {
                        int num11 = player.FishingLevel();
                        int minValue2 = (num11 / 4 + 15) / 2;
                        int num12 = (num11 / 2 + 30) / 2;
                        if (Main.rand.Next(50) < num11)
                        {
                            num12 += 4;
                        }
                        if (Main.rand.Next(100) < num11)
                        {
                            num12 += 4;
                        }
                        if (Main.rand.Next(150) < num11)
                        {
                            num12 += 4;
                        }
                        if (Main.rand.Next(200) < num11)
                        {
                            num12 += 4;
                        }
                        int stack2 = Main.rand.Next(minValue2, num12 + 1);
                        fishedAmount = (itm.value * stack2) / 5;
                    }
                    fishedAmount = itm.value / 5;
                    return;
                }
            }
            if (canReplaceFish(caughtType))
            {
                

                if (player.position.Y >= Main.maxTilesY * 0.91f && liquidType == 1 && Main.rand.Next(6) == 0)
                {
                    caughtType = mod.ItemType("CrustyStar");
                    return;
                }
                if (liquidType == 2 && Main.rand.Next(7) == 0)
                {
                    caughtType = mod.ItemType("HoneyStar");
                    return;
                }


                if (player.ZoneBeach && liquidType == 0 && Main.rand.Next(9) == 0)
                {
                    caughtType = mod.ItemType("SeaweedStar");
                    return;
                }

                List<int> possibleCrate = new List<int>();

                if (((maxCrate && Main.rand.Next(25) == 0) || Main.rand.Next(50) == 0) && NPC.downedMoonlord && player.ZoneSkyHeight)
                {
                    caughtType = ModContent.ItemType<WingCrate>();
                    return;
                }

                if (((maxCrate && Main.rand.Next(40) == 0) || Main.rand.Next(80) == 0) && Main.hardMode)
                {
                    caughtType = ModContent.ItemType<AnkhCrate>();
                    return;
                }

                if ((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(16) == 0 || (caughtType == ItemID.WoodenCrate && Main.rand.Next(8) == 0))
                {
                    possibleCrate.AddRange(replaceWithEventCrate(fishingRod, liquidType));
                    if (possibleCrate.Count > 0)
                    {
                        caughtType = possibleCrate[Main.rand.Next(possibleCrate.Count)];
                        return;
                    }
                }

                if ((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(24) == 0 || (caughtType == ItemID.WoodenCrate && Main.rand.Next(12) == 0))
                {
                    possibleCrate.AddRange(replaceWithRodCrate(fishingRod, liquidType));
                    if (possibleCrate.Count > 0)
                    {
                        caughtType = possibleCrate[Main.rand.Next(possibleCrate.Count)];
                        return;
                    }
                }

                if (player.ZonePeaceCandle && ((maxCrate && Main.rand.Next(10) == 0) || Main.rand.Next(25) == 0))
                {
                    caughtType = ModContent.ItemType<CritterCrate>();
                    return;
                }

                if ((maxCrate && Main.rand.Next(6) == 0) || (!Main.hardMode && Main.rand.Next(32) == 0) || Main.rand.Next(64) == 0)
                {
                    caughtType = mod.ItemType("MimicCrate");
                    return;
                }
                if (Main.hardMode && (player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly) && ((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(12) == 0))
                {
                    caughtType = mod.ItemType("SoulCrate");
                    return;
                }
                if (((maxCrate && Main.rand.Next(3) == 0) || Main.rand.Next(6) == 0) && FishWorld.graniteTiles > 75)
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
                    if (Main.rand.Next(30) == 0)
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
                if (fishSlicer && !ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipesNotAuto.Contains(new Terraria.ModLoader.Config.ItemDefinition(caughtType)))
                {
                    if (ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes.ContainsKey(new Terraria.ModLoader.Config.ItemDefinition(caughtType)))
                    {
                        caughtType = ModContent.ItemType<FishSteaks>();
                        fishedAmount = ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes[new Terraria.ModLoader.Config.ItemDefinition(caughtType)];
                    }
                }
            }
        }

        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (player.anglerQuestsFinished == 20 && !player.GetModPlayer<FishPlayer>().MasterBaiter)
            {
                Item itm = new Item();
                itm.SetDefaults(ModContent.ItemType<MasterBaiterCertificate>(), true);
                itm.stack = 1;
                rewardItems.Add(itm);
            }
            if (NPC.downedMoonlord && Main.rand.Next(3) == 0)
            {
                Item itm = new Item();
                itm.SetDefaults(ModContent.ItemType<DoctorBait>(), true);
                itm.stack = Main.rand.Next(3,9);
                rewardItems.Add(itm);
            }
            bool hadBait = false;
            if (NPC.downedPlantBoss && Main.rand.Next(3) == 0)
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadMasterBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomMasterBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireMasterBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameMasterBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonMasterBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilMasterBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasMasterBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorMasterBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireMasterBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnMasterBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireMasterBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameMasterBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionMasterBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseMasterBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(3, 9);
                rewardItems.Add(itm);
                hadBait = true;
            }

            if (Main.hardMode && !hadBait && Main.rand.Next(3) == 0)
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(5, 13);
                rewardItems.Add(itm);
                hadBait = true;
            }

            if (!hadBait && Main.rand.Next(3) == 0)
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadApprenticeBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomApprenticeBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireApprenticeBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameApprenticeBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonApprenticeBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilApprenticeBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasApprenticeBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorApprenticeBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireApprenticeBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnApprenticeBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireApprenticeBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameApprenticeBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionApprenticeBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseApprenticeBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(5, 13);
                rewardItems.Add(itm);
                hadBait = true;
            }
        }

        public List<int> replaceWithEventCrate(Item fishingRod, int liquid)
        {
            List<int> ans = new List<int>();
            if(DD2Event.Ongoing && DD2Event.ReadyForTier3)
            {
                ans.Add(ModContent.ItemType<OldOnesCrate>());
            }
            if (Main.pumpkinMoon)
            {
                ans.Add(ModContent.ItemType<SpookyCrate>());
            }
            if (Main.snowMoon)
            {
                ans.Add(ModContent.ItemType<FrostMoonCrate>());
            }
            if (Main.eclipse)
            {
                ans.Add(ModContent.ItemType<EclipseCrate>());
            }
            if (Main.bloodMoon)
            {
                ans.Add(ModContent.ItemType<BloodCrate>());
            }
            if (Main.slimeRain)
            {
                ans.Add(ModContent.ItemType<SlimeCrate>());
            }

            if (Main.invasionType == 1) {
                ans.Add(ModContent.ItemType<GoblinCrate>());
            }
            else if (Main.invasionType == 2)
            {
                ans.Add(ModContent.ItemType<FrostLegionCrate>());
            }
            else if (Main.invasionType == 3)
            {
                ans.Add(ModContent.ItemType<TreasureCrate>());
            }
            else if (Main.invasionType == 4)
            {
                ans.Add(ModContent.ItemType<AlienCrate>());
            }

            if(Sandstorm.Happening && player.ZoneDesert)
            {
                ans.Add(ModContent.ItemType<SandstormCrate>());
            }

            if(Main.raining && player.ZoneSnow)
            {
                ans.Add(ModContent.ItemType<SnowstormCrate>());
            }

            return ans;
        }

        public List<int> replaceWithRodCrate(Item fishingRod, int liquid)
        {
            bool rodContainment = fishingRod.type == mod.ItemType("RodContainmentUnit");
            List<int> ans = new List<int>();

            if ((liquid == 2 || liquid == -1) && (rodContainment || fishingRod.type == mod.ItemType("BeeBattlerod")|| fishingRod.type == mod.ItemType("BeeteoriteBattlerod")))
            {
                ans.Add(mod.ItemType("BeeCrate"));
            }
            if ((liquid != 2)&& (rodContainment || fishingRod.type == mod.ItemType("HellstoneBattlerod")))
            {
                ans.Add(mod.ItemType("ObsidianCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("VortexBattlerod")|| fishingRod.type == mod.ItemType("SolarBattlerod") || fishingRod.type == mod.ItemType("NebulaBattlerod") || fishingRod.type == mod.ItemType("StardustBattlerod") || fishingRod.type == mod.ItemType("FractaliteBattlerod"))
            {
                ans.Add(mod.ItemType("LuminiteCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("MeteorBattlerod") || fishingRod.type == mod.ItemType("BeeteoriteBattlerod"))
            {
                ans.Add(mod.ItemType("MeteorCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("HallowedBattlerod") || fishingRod.type == mod.ItemType("HardTriadBattlerod"))
            {
                ans.Add(mod.ItemType("HallowedCrate"));
            }

            if (rodContainment || fishingRod.type == mod.ItemType("CorruptBattlerod") || fishingRod.type == mod.ItemType("EvilRodOfDarkness"))
            {
                ans.Add(mod.ItemType("CorruptCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("CrimsonBattlerod") || fishingRod.type == mod.ItemType("EvilRodOfBlood"))
            {
               ans.Add(mod.ItemType("CrimsonCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("LifeforceBattlerod")  || fishingRod.type == mod.ItemType("ShroomiteBattlerod"))
            {
                ans.Add(mod.ItemType("ShroomiteCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("LifeforceBattlerod") || fishingRod.type == mod.ItemType("EvilRodOfDarkness") || fishingRod.type == mod.ItemType("EvilRodOfBlood") ||fishingRod.type == mod.ItemType("SpectreBattlerod"))
            {
                ans.Add(mod.ItemType("SoulCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("LifeforceBattlerod") || fishingRod.type == mod.ItemType("ChlorophyteBattlerod") || fishingRod.type == mod.ItemType("TurtleBattlerod") || fishingRod.type == mod.ItemType("BeetleBattlerod"))
            {
                ans.Add(mod.ItemType("ChlorophyteCrate"));
            }
            if (rodContainment || fishingRod.type == mod.ItemType("TerraBattlerod"))
            {
                ans.Add(mod.ItemType("TerraCrate"));                
            }
            if (rodContainment || fishingRod.type == mod.ItemType("DungeonBattlerod"))
            {
                ans.Add(ItemID.DungeonFishingCrate);
            }
            if (rodContainment || fishingRod.type == mod.ItemType("SpookyBattlerod"))
            {
                ans.Add(mod.ItemType("SpookyCrate"));
            }

            return ans;
        }

        public static bool canReplaceFish(int fishFound)
        {
            if (ModContent.GetInstance<UnuServerConfig>().allowFishedItems) {
                if(ModContent.GetInstance<FishToReplaceConfig>().replaceAllFish)
                    return true;

                return ModContent.GetInstance<FishToReplaceConfig>().fishToReplace.Contains(new Terraria.ModLoader.Config.ItemDefinition(fishFound));
            }
            return false;
        }

        int lastChest = -1;
        public void checkForMimicLikeSpawns()
        {
            int spawnItem = 0;
            if (lastChest > -1 && player.chest == -1 && Main.chest[lastChest] != null && Main.chest[lastChest].item != null)
            {
                for(int i = 0; i < Main.chest[lastChest].item.Length; i++)
                {
                    if (isSpawnItem(Main.chest[lastChest].item[i].type) && Main.chest[lastChest].item[i].stack == 1)
                    {
                        if (spawnItem == 0)
                        {
                            spawnItem = Main.chest[lastChest].item[i].type;
                        }
                        else { lastChest = player.chest; return; }
                    }else if (Main.chest[lastChest].item[i].type != 0)
                    {
                        lastChest = player.chest; return;
                    }
                }
                if (spawnItem != 0)
                {
                    summonMimic(spawnItem);
                }
                else
                {
                    lastChest = player.chest;
                }
            }
            lastChest = player.chest;
        }

        public bool isSpawnItem(int type)
        {
            return type == ModContent.ItemType<IceyWorm>();
        }

        public void summonMimic(int itemType)
        {
            int x = Main.chest[lastChest].x;
            int y = Main.chest[lastChest].y;

            for (int i = 0; i < Main.chest[lastChest].item.Length; i++)
            {
                Main.chest[lastChest].item[i] = new Item();
            }

            for (int j = x; j <= x + 1; j++)
            {
                for (int k = y; k <= y + 1; k++)
                {
                    if (Main.tile[j, k].type == 21)
                    {
                        Main.tile[j, k].active(false);
                    }
                }
            }
            Chest.DestroyChest(x, y);
            if (itemType == ModContent.ItemType<IceyWorm>())
            {
                int type2 = ModContent.NPCType<CoolerBoss>();
                int num5 = NPC.NewNPC(x * 16 + 16, y * 16 + 32, type2, 0, 0f, 0f, 0f, 0f, 255);
                Main.npc[num5].whoAmI = num5;
                NetMessage.SendData(23, -1, -1, null, num5, 0f, 0f, 0f, 0, 0, 0);
                Main.npc[num5].BigMimicSpawnSmoke();
                if (Main.netMode == 0)
                {
                    Main.NewText(Language.GetTextValue("Announcement.HasAwoken", Main.npc[num5].GetTypeNetName()), 175, 75, 255, false);
                    return;
                }
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                                    Main.npc[num5].GetTypeNetName()
                    }), new Color(175, 75, 255), -1);
                }
            }
        }
        

        public void addBaitBuffs(int time, int slot, int buff = -1)
        {
            if (slot < 0 || slot > baitBuff.Length)
            {
                return;
            }
            baitBuff[slot] = buff;
            baitTimer = Math.Max(time, baitTimer);
            // checkBaitAndUpdateTime(time);

        }

        public void addBaitDebuffs(int time, int slot, int debuff = -1)
        {
            if (slot < 0 || slot > baitBuff.Length)
            {
                return;
            }
            baitDebuff[slot] = debuff;
            baitTimer = Math.Max(time, baitTimer);
            //checkBaitAndUpdateTime(time);
        }


       /* private void checkBaitAndUpdateTime(int time)
        {
            if (!hasAnyBaitBuffs() && !hasAnyBaitDebuffs()) { 
                baitTimer = 0;
            } else {
                baitTimer = Math.Max(time, baitTimer);
            }

            int pbtype = ModContent.BuffType<PoweredBaitBuff>();
            int baitBuffIdx = player.FindBuffIndex(pbtype);
            if (baitBuffIdx >= 0)
            {
                if (baitTimer == 0)
                {
                    player.DelBuff(baitBuffIdx);
                }
                else
                {
                    player.buffTime[baitBuffIdx] = baitTimer;
                }
            }
        }*/

        public void updateBaits()
        {
            updateBaits(player.whoAmI);
        }

        public override void OnEnterWorld(Player player)
        {
            if(Main.netMode == NetmodeID.SinglePlayer)
            {
               
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)9);
                pk.Send();
            }else if(Main.netMode == NetmodeID.Server)
            {
                for (int k = 0; k < Main.player.Length; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead)
                    {
                        FishPlayer pl = Main.player[k].GetModPlayer<FishPlayer>();
                        if (pl.hasAnyBaitBuffs() || pl.hasAnyBaitDebuffs())
                        {
                            pl.updateBaits();
                        }
                    }
                }
            }
        }

        public void updateBaits(int plr)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)10);
                pk.Write((short)plr);
                pk.Write(baitTimer);
                for (int k = 0; k < baitBuff.Length; k++)
                {
                    pk.Write((int)Main.player[plr].GetModPlayer<FishPlayer>().baitBuff[k]);
                }
                for (int k = 0; k < baitDebuff.Length; k++)
                {
                    pk.Write((int)Main.player[plr].GetModPlayer<FishPlayer>().baitDebuff[k]);
                }
                pk.Send();
            }
        }

        public bool hasAnyBaitBuffs()
        {
            if(baitBuff == null)
            {
                baitBuff = new int[4];
                for (int i = 0; i < baitBuff.Length; i++)
                {
                    baitBuff[i] = -1;
                }
                return false;
            }
            for (int i = 0; i < baitBuff.Length; i++)
            {
                if (baitBuff[i] != -1)
                    return true;
            }
            return false;
        }
        public bool hasAnyBaitDebuffs()
        {
            if (baitDebuff == null)
            {
                baitDebuff = new int[4];
                for (int i = 0; i < baitDebuff.Length; i++)
                {
                    baitDebuff[i] = -1;
                }
                return false;
            }

            for (int i = 0; i < baitDebuff.Length; i++)
            {
                if (baitDebuff[i] != -1)
                    return true;
            }
            return false;
        }

        public int getNumberOfBaitDebuffs()
        {
            int cnt = 0;
            for (int i = 0; i < baitDebuff.Length; i++)
            {
                if (baitDebuff[i] != -1)
                    cnt++;
            }
            return cnt;
        }

        public int getNumberOfBaitBuffs()
        {
            int cnt = 0;
            for (int i = 0; i < baitBuff.Length; i++)
            {
                if (baitBuff[i] != -1)
                    cnt++;
            }
            return cnt;
        }

        public int getNumberOfUseableBaits()
        {
            int cnt = 0;
            for (int i = 54; i < 58; i++)
            {
                BasePoweredBait b = player.inventory[i].modItem as BasePoweredBait;
                if (b != null)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        public void updateBuffByID(ref int id, int time, int buffSlot = -1)
        {
            if (player.buffImmune[id])
                return;

            ModBuff bff = BuffLoader.GetBuff(id);
            if(bff != null)
            {
                if (buffSlot == -1)
                {

                    for (buffSlot = 0; buffSlot < 22; buffSlot++)
                    {
                        if (player.buffType[buffSlot] == ModContent.BuffType<PoweredBaitBuff>())
                            break;
                    }
                    if (buffSlot == 22)
                        return;
                }
                bff.Update(player,ref buffSlot);
                player.buffType[buffSlot] = ModContent.BuffType<PoweredBaitBuff>();
                return;
            }

            if (id == 1)
            {
                player.lavaImmune = true;
                player.fireWalk = true;
                player.buffImmune[24] = true;
            }
            else if (id == 158)
            {
                player.manaRegenBonus += 2;
            }
            else if (id == 159 && player.inventory[player.selectedItem].melee)
            {
                player.armorPenetration = 4;
            }
            else if (id == 2)
            {
                player.lifeRegen += 4;
            }
            else if (id == 3)
            {
                player.moveSpeed += 0.25f;
            }
            else if (id == 4)
            {
                player.gills = true;
            }
            else if (id == 5)
            {
                player.statDefense += 8;
            }
            else if (id == 6)
            {
                player.manaRegenBuff = true;
            }
            else if (id == 7)
            {
                player.magicDamage += 0.2f;
            }
            else if (id == 8)
            {
                player.slowFall = true;
            }
            else if (id == 9)
            {
                player.findTreasure = true;
            }
            else if (id == 10)
            {
                player.invis = true;
            }
            else if (id == 11)
            {
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
            }
            else if (id == 12)
            {
                player.nightVision = true;
            }
            else if (id == 13)
            {
                player.enemySpawns = true;
            }
            else if (id == 14)
            {
                if (player.thorns < 1f)
                {
                    player.thorns = 0.333333343f;
                }
            }
            else if (id == 15)
            {
                player.waterWalk = true;
            }
            else if (id == 16)
            {
                player.archery = true;
            }
            else if (id == 17)
            {
                player.detectCreature = true;
            }
            else if (id == 18)
            {
                player.gravControl = true;
            }
            else if (id == 30)
            {
                player.bleed = true;
            }
            else if (id == 31)
            {
                player.confused = true;
            }
            else if (id == 32)
            {
                player.slow = true;
            }
            else if (id == 35)
            {
                player.silence = true;
            }
            else if (id == 160)
            {
                player.dazed = true;
            }
            else if (id == 46)
            {
                player.chilled = true;
            }
            else if (id == 47)
            {
                player.frozen = true;
            }
            else if (id == 156)
            {
                player.stoned = true;
            }
            else if (id == 69)
            {
                player.ichor = true;
                player.statDefense -= 20;
            }
            else if (id == 36)
            {
                player.brokenArmor = true;
            }
            else if (id == 48)
            {
                player.honey = true;
            }
            else if (id == 59)
            {
                player.shadowDodge = true;
            }
            else if (id == 93)
            {
                player.ammoBox = true;
            }
            else if (id == 58)
            {
                player.palladiumRegen = true;
            }
            else if (id == 88)
            {
                player.chaosState = true;
            }
            else if (id == 63)
            {
                player.moveSpeed += 1f;
            }
            else if (id == 104)
            {
                player.pickSpeed -= 0.25f;
            }
            else if (id == 105)
            {
                player.lifeMagnet = true;
            }
            else if (id == 106)
            {
                player.calmed = true;
            }
            else if (id == 121)
            {
                player.fishingSkill += 15;
            }
            else if (id == 122)
            {
                player.sonarPotion = true;
            }
            else if (id == 123)
            {
                player.cratePotion = true;
            }
            else if (id == 107)
            {
                player.tileSpeed += 0.25f;
                player.wallSpeed += 0.25f;
                player.blockRange += 1;
            }
            else if (id == 108)
            {
                player.kbBuff = true;
            }
            else if (id == 109)
            {
                player.ignoreWater = true;
                player.accFlipper = true;
            }
            else if (id == 110)
            {
                player.maxMinions += 1;
            }
            else if (id == 150)
            {
                player.maxMinions += 1;
            }
            else if (id == 111)
            {
                player.dangerSense = true;
            }
            else if (id == 112)
            {
                player.ammoPotion = true;
            }
            else if (id == 113)
            {
                player.lifeForce = true;
                player.statLifeMax2 += player.statLifeMax / 5 / 20 * 20;
            }
            else if (id == 114)
            {
                player.endurance += 0.1f;
            }
            else if (id == 115)
            {
                player.meleeCrit += 10;
                player.rangedCrit += 10;
                player.magicCrit += 10;
                player.thrownCrit += 10;
            }
            else if (id == 116)
            {
                player.inferno = true;
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
                int num3 = 24;
                float num4 = 200f;
                bool flag = player.infernoCounter % 60 == 0;
                int damage = 10;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num3] && Vector2.Distance(player.Center, nPC.Center) <= num4)
                        {
                            if (nPC.FindBuffIndex(num3) == -1)
                            {
                                nPC.AddBuff(num3, 120, false);
                            }
                            if (flag)
                            {
                                player.ApplyDamageToNPC(nPC, damage, 0f, 0, false);
                            }
                        }
                    }
                    if (player.hostile)
                    {
                        for (int m = 0; m < 255; m++)
                        {
                            Player pl = Main.player[m];
                            if (player != pl && pl.active && !pl.dead && pl.hostile && !pl.buffImmune[num3] && (pl.team != player.team || pl.team == 0) && Vector2.Distance(player.Center, pl.Center) <= num4)
                            {
                                if (pl.FindBuffIndex(num3) == -1)
                                {
                                    pl.AddBuff(num3, 120, true);
                                }
                                if (flag)
                                {
                                    pl.Hurt(PlayerDeathReason.LegacyEmpty(), damage, 0, true, false, false, -1);
                                    if (Main.netMode != 0)
                                    {
                                        PlayerDeathReason reason = PlayerDeathReason.ByPlayer(player.whoAmI);
                                        NetMessage.SendPlayerHurt(m, reason, damage, 0, false, true, 0, -1, -1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (id == 117)
            {
                player.thrownDamage += 0.1f;
                player.meleeDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.magicDamage += 0.1f;
                player.minionDamage += 0.1f;
            }
            else if (id == 119)
            {
                player.loveStruck = true;
            }
            else if (id == 120)
            {
                player.stinky = true;
            }
            else if (id == 124)
            {
                player.resistCold = true;
            }
            else if (id == 165)
            {
                player.lifeRegen += 6;
                player.statDefense += 8;
                player.dryadWard = true;
                if (player.thorns < 1f)
                {
                    player.thorns += 0.2f;
                }
            }
            else if (id == 144)
            {
                player.electrified = true;
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.3f, 0.8f, 1.1f);
            }
            else if (id == 94)
            {
                player.manaSick = true;
                player.manaSickReduction = Player.manaSickLessDmg * ((float)time / (float)Player.manaSickTime);
            }
            else if (id == 62)
            {
                if ((double)player.statLife <= (double)player.statLifeMax2 * 0.5)
                {
                    Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.1f, 0.2f, 0.45f);
                    player.iceBarrier = true;
                    player.endurance += 0.25f;
                    player.iceBarrierFrameCounter += 1;
                    if (player.iceBarrierFrameCounter > 2)
                    {
                        player.iceBarrierFrameCounter = 0;
                        player.iceBarrierFrame += 1;
                        if (player.iceBarrierFrame >= 12)
                        {
                            player.iceBarrierFrame = 0;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (id == 49)
            {
                for (int num20 = 191; num20 <= 194; num20++)
                {
                    if (player.ownedProjectileCounts[num20] > 0)
                    {
                        player.pygmy = true;
                    }
                }
            }
            else if (id == 83)
            {
                if (player.ownedProjectileCounts[317] > 0)
                {
                    player.raven = true;
                }
            }
            else if (id == 64)
            {
                if (player.ownedProjectileCounts[266] > 0)
                {
                    player.slime = true;
                }
            }
            else if (id == 125)
            {
                if (player.ownedProjectileCounts[373] > 0)
                {
                    player.hornetMinion = true;
                }
            }
            else if (id == 126)
            {
                if (player.ownedProjectileCounts[375] > 0)
                {
                    player.impMinion = true;
                }
            }
            else if (id == 133)
            {
                if (player.ownedProjectileCounts[390] > 0 || player.ownedProjectileCounts[391] > 0 || player.ownedProjectileCounts[392] > 0)
                {
                    player.spiderMinion = true;
                }
            }
            else if (id == 134)
            {
                if (player.ownedProjectileCounts[387] > 0 || player.ownedProjectileCounts[388] > 0)
                {
                    player.twinsMinion = true;
                }
            }
            else if (id == 135)
            {
                if (player.ownedProjectileCounts[393] > 0 || player.ownedProjectileCounts[394] > 0 || player.ownedProjectileCounts[395] > 0)
                {
                    player.pirateMinion = true;
                }
            }
            else if (id == 139)
            {
                if (player.ownedProjectileCounts[407] > 0)
                {
                    player.sharknadoMinion = true;
                }
            }
            else if (id == 140)
            {
                if (player.ownedProjectileCounts[423] > 0)
                {
                    player.UFOMinion = true;
                }
            }
            else if (id == 182)
            {
                if (player.ownedProjectileCounts[613] > 0)
                {
                    player.stardustMinion = true;
                }               
            }
            else if (id == 187)
            {
                if (player.ownedProjectileCounts[623] > 0)
                {
                    player.stardustGuardian = true;
                }               
            }
            else if (id == 188)
            {
                if (player.ownedProjectileCounts[625] > 0)
                {
                    player.stardustDragon = true;
                }                
            }
            else if (id == 161)
            {
                if (player.ownedProjectileCounts[533] > 0)
                {
                    player.DeadlySphereMinion = true;
                }                
            }
            else if (id == 90)
            {
                player.mount.SetMount(0, player, false);
               
            }
            else if (id == 128)
            {
                player.mount.SetMount(1, player, false);
                
            }
            else if (id == 129)
            {
                player.mount.SetMount(2, player, false);
                
            }
            else if (id == 130)
            {
                player.mount.SetMount(3, player, false);
                
            }
            else if (id == 118)
            {
                player.mount.SetMount(6, player, true);
                
            }
            else if (id == 138)
            {
                player.mount.SetMount(6, player, false);
                
            }
            else if (id == 167)
            {
                player.mount.SetMount(11, player, true);
                
            }
            else if (id == 166)
            {
                player.mount.SetMount(11, player, false);
                
            }
            else if (id == 184)
            {
                player.mount.SetMount(13, player, true);
                
            }
            else if (id == 185)
            {
                player.mount.SetMount(13, player, false);
                
            }
            else if (id == 131)
            {
                player.ignoreWater = true;
                player.accFlipper = true;
                player.mount.SetMount(4, player, false);
               
            }
            else if (id == 132)
            {
                player.mount.SetMount(5, player, false);
               
            }
            else if (id == 168)
            {
                player.ignoreWater = true;
                player.accFlipper = true;
                player.mount.SetMount(12, player, false);
               
            }
            else if (id == 141)
            {
                player.mount.SetMount(7, player, false);
                
            }
            else if (id == 142)
            {
                player.mount.SetMount(8, player, false);
                
            }
            else if (id == 143)
            {
                player.mount.SetMount(9, player, false);
                
            }
            else if (id == 162)
            {
                player.mount.SetMount(10, player, false);
                
            }
            else if (id == 193)
            {
                player.mount.SetMount(14, player, false);
               
            }
            else if (id == 37)
            {
                if (Main.wof >= 0 && Main.npc[Main.wof].type == 113)
                {
                    player.gross = true;
                    
                }
            }
            else if (id == 38)
            {
                player.tongued = true;
            }
            else if (id == 146)
            {
                player.moveSpeed += 0.1f;
                player.moveSpeed *= 1.1f;
                player.sunflower = true;
            }
            else if (id == 19)
            {
                player.lightOrb = true;
                bool flag2 = true;
                if (player.ownedProjectileCounts[18] > 0)
                {
                    flag2 = false;
                }
                if (flag2 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 18, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 155)
            {                
                player.crimsonHeart = true;
                bool flag3 = true;
                if (player.ownedProjectileCounts[500] > 0)
                {
                    flag3 = false;
                }
                if (flag3 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 500, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 190)
            {
                player.suspiciouslookingTentacle = true;
                bool flag4 = true;
                if (player.ownedProjectileCounts[650] > 0)
                {
                    flag4 = false;
                }
                if (flag4 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 650, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 27 || id == 101 || id == 102)
            {             
                bool flag5 = true;
                int num21 = 72;
                if (id == 27)
                {
                    player.blueFairy = true;
                }
                if (id == 101)
                {
                    num21 = 86;
                    player.redFairy = true;
                }
                if (id == 102)
                {
                    num21 = 87;
                    player.greenFairy = true;
                }
                if (player.head == 45 && player.body == 26 && player.legs == 25)
                {
                    num21 = 72;
                }
                if (player.ownedProjectileCounts[num21] > 0)
                {
                    flag5 = false;
                }
                if (flag5 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, num21, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 40)
            {
                player.bunny = true;
                bool flag6 = true;
                if (player.ownedProjectileCounts[111] > 0)
                {
                    flag6 = false;
                }
                if (flag6 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 111, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 148)
            {
                player.rabid = true;
                if (Main.rand.Next(1200) == 0)
                {
                    int num22 = Main.rand.Next(6);
                    float num23 = (float)Main.rand.Next(60, 100) * 0.01f;
                    if (num22 == 0)
                    {
                        player.AddBuff(22, (int)(60f * num23 * 3f), true);
                    }
                    else if (num22 == 1)
                    {
                        player.AddBuff(23, (int)(60f * num23 * 0.75f), true);
                    }
                    else if (num22 == 2)
                    {
                        player.AddBuff(31, (int)(60f * num23 * 1.5f), true);
                    }
                    else if (num22 == 3)
                    {
                        player.AddBuff(32, (int)(60f * num23 * 3.5f), true);
                    }
                    else if (num22 == 4)
                    {
                        player.AddBuff(33, (int)(60f * num23 * 5f), true);
                    }
                    else if (num22 == 5)
                    {
                        player.AddBuff(35, (int)(60f * num23 * 1f), true);
                    }
                }
                player.meleeDamage += 0.2f;
                player.magicDamage += 0.2f;
                player.rangedDamage += 0.2f;
                player.thrownDamage += 0.2f;
                player.minionDamage += 0.2f;
            }
            else if (id == 41)
            {
             
                player.penguin = true;
                bool flag7 = true;
                if (player.ownedProjectileCounts[112] > 0)
                {
                    flag7 = false;
                }
                if (flag7 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 112, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 152)
            {
             
                player.magicLantern = true;
                if (player.ownedProjectileCounts[492] == 0 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 492, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 91)
            {
             
                player.puppy = true;
                bool flag8 = true;
                if (player.ownedProjectileCounts[334] > 0)
                {
                    flag8 = false;
                }
                if (flag8 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 334, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 92)
            {
             
                player.grinch = true;
                bool flag9 = true;
                if (player.ownedProjectileCounts[353] > 0)
                {
                    flag9 = false;
                }
                if (flag9 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 353, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 84)
            {
             
                player.blackCat = true;
                bool flag10 = true;
                if (player.ownedProjectileCounts[319] > 0)
                {
                    flag10 = false;
                }
                if (flag10 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 319, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 61)
            {
             
                player.dino = true;
                bool flag11 = true;
                if (player.ownedProjectileCounts[236] > 0)
                {
                    flag11 = false;
                }
                if (flag11 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 236, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 154)
            {
             
                player.babyFaceMonster = true;
                bool flag12 = true;
                if (player.ownedProjectileCounts[499] > 0)
                {
                    flag12 = false;
                }
                if (flag12 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 499, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 65)
            {
             
                player.eyeSpring = true;
                bool flag13 = true;
                if (player.ownedProjectileCounts[268] > 0)
                {
                    flag13 = false;
                }
                if (flag13 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 268, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 66)
            {
             
                player.snowman = true;
                bool flag14 = true;
                if (player.ownedProjectileCounts[269] > 0)
                {
                    flag14 = false;
                }
                if (flag14 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 269, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 42)
            {
             
                player.turtle = true;
                bool flag15 = true;
                if (player.ownedProjectileCounts[127] > 0)
                {
                    flag15 = false;
                }
                if (flag15 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 127, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 45)
            {
             
                player.eater = true;
                bool flag16 = true;
                if (player.ownedProjectileCounts[175] > 0)
                {
                    flag16 = false;
                }
                if (flag16 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 175, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 50)
            {
             
                player.skeletron = true;
                bool flag17 = true;
                if (player.ownedProjectileCounts[197] > 0)
                {
                    flag17 = false;
                }
                if (flag17 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 197, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 51)
            {
             
                player.hornet = true;
                bool flag18 = true;
                if (player.ownedProjectileCounts[198] > 0)
                {
                    flag18 = false;
                }
                if (flag18 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 198, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 52)
            {
             
                player.tiki = true;
                bool flag19 = true;
                if (player.ownedProjectileCounts[199] > 0)
                {
                    flag19 = false;
                }
                if (flag19 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 199, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 53)
            {
             
                player.lizard = true;
                bool flag20 = true;
                if (player.ownedProjectileCounts[200] > 0)
                {
                    flag20 = false;
                }
                if (flag20 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 200, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 54)
            {
             
                player.parrot = true;
                bool flag21 = true;
                if (player.ownedProjectileCounts[208] > 0)
                {
                    flag21 = false;
                }
                if (flag21 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 208, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 55)
            {
             
                player.truffle = true;
                bool flag22 = true;
                if (player.ownedProjectileCounts[209] > 0)
                {
                    flag22 = false;
                }
                if (flag22 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 209, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 56)
            {
             
                player.sapling = true;
                bool flag23 = true;
                if (player.ownedProjectileCounts[210] > 0)
                {
                    flag23 = false;
                }
                if (flag23 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 210, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 85)
            {
             
                player.cSapling = true;
                bool flag24 = true;
                if (player.ownedProjectileCounts[324] > 0)
                {
                    flag24 = false;
                }
                if (flag24 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 324, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 81)
            {
             
                player.spider = true;
                bool flag25 = true;
                if (player.ownedProjectileCounts[313] > 0)
                {
                    flag25 = false;
                }
                if (flag25 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 313, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 82)
            {
             
                player.squashling = true;
                bool flag26 = true;
                if (player.ownedProjectileCounts[314] > 0)
                {
                    flag26 = false;
                }
                if (flag26 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 314, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 57)
            {
             
                player.wisp = true;
                bool flag27 = true;
                if (player.ownedProjectileCounts[211] > 0)
                {
                    flag27 = false;
                }
                if (flag27 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 211, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 60)
            {
             
                player.crystalLeaf = true;
                bool flag28 = true;
                for (int num24 = 0; num24 < 1000; num24++)
                {
                    if (Main.projectile[num24].active && Main.projectile[num24].owner == player.whoAmI && Main.projectile[num24].type == 226)
                    {
                        if (!flag28)
                        {
                            Main.projectile[num24].Kill();
                        }
                        flag28 = false;
                    }
                }
                if (flag28 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 226, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 127)
            {
             
                player.zephyrfish = true;
                bool flag29 = true;
                if (player.ownedProjectileCounts[380] > 0)
                {
                    flag29 = false;
                }
                if (flag29 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 380, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 136)
            {
             
                player.miniMinotaur = true;
                bool flag30 = true;
                if (player.ownedProjectileCounts[398] > 0)
                {
                    flag30 = false;
                }
                if (flag30 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, 398, 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            else if (id == 70)
            {
                player.venom = true;
            }
            else if (id == 20)
            {
                player.poisoned = true;
            }
            else if (id == 21)
            {
                player.potionDelay = time;
            }
            else if (id == 22)
            {
                player.blind = true;
            }
            else if (id == 80)
            {
                player.blackout = true;
            }
            else if (id == 23)
            {
                player.noItems = true;
            }
            else if (id == 24)
            {
                player.onFire = true;
            }
            else if (id == 103)
            {
                player.dripping = true;
            }
            else if (id == 137)
            {
                player.drippingSlime = true;
            }
            else if (id == 67)
            {
                player.burned = true;
            }
            else if (id == 68)
            {
                player.suffocating = true;
            }
            else if (id == 39)
            {
                player.onFire2 = true;
            }
            else if (id == 44)
            {
                player.onFrostBurn = true;
            }
            else if (id == 163)
            {
                player.headcovered = true;
                player.bleed = true;
            }
            else if (id == 164)
            {
                player.vortexDebuff = true;
            }
            else if (id == 194)
            {
                player.windPushed = true;
            }
            else if (id == 195)
            {
                player.witheredArmor = true;
            }
            else if (id == 205)
            {
                player.ballistaPanic = true;
            }
            else if (id == 196)
            {
                player.witheredWeapon = true;
            }
            else if (id == 197)
            {
                player.slowOgreSpit = true;
            }
            else if (id == 198)
            {
                player.parryDamageBuff = true;
            }
            else if (id == 145)
            {
                player.moonLeech = true;
            }
            else if (id == 149)
            {
                player.webbed = true;
                if (player.velocity.Y != 0f)
                {
                    player.velocity = new Vector2(0f, 1E-06f);
                }
                else
                {
                    player.velocity = Vector2.Zero;
                }
                Player.jumpHeight = 0;
                player.gravity = 0f;
                player.moveSpeed = 0f;
                player.dash = 0;
                player.noKnockback = true;
                player.grappling[0] = -1;
                player.grapCount = 0;
                for (int num25 = 0; num25 < 1000; num25++)
                {
                    if (Main.projectile[num25].active && Main.projectile[num25].owner == player.whoAmI && Main.projectile[num25].aiStyle == 7)
                    {
                        Main.projectile[num25].Kill();
                    }
                }
            }
            else if (id == 43)
            {
                player.defendedByPaladin = true;
            }
            else if (id == 29)
            {
                player.magicCrit += 2;
                player.magicDamage += 0.05f;
                player.statManaMax2 += 20;
                player.manaCost -= 0.02f;
            }
            else if (id == 28)
            {
                if (!Main.dayTime && player.wolfAcc && !player.merman)
                {
                    player.lifeRegen++;
                    player.wereWolf = true;
                    player.meleeCrit += 2;
                    player.meleeDamage += 0.051f;
                    player.meleeSpeed += 0.051f;
                    player.statDefense += 3;
                    player.moveSpeed += 0.05f;
                }
            }
            else if (id == 33)
            {
                player.meleeDamage -= 0.051f;
                player.meleeSpeed -= 0.051f;
                player.statDefense -= 4;
                player.moveSpeed -= 0.1f;
            }
            else if (id == 25)
            {
                player.statDefense -= 4;
                player.meleeCrit += 2;
                player.meleeDamage += 0.1f;
                player.meleeSpeed += 0.1f;
            }
            else if (id == 26)
            {
                player.wellFed = true;
                player.statDefense += 2;
                player.meleeCrit += 2;
                player.meleeDamage += 0.05f;
                player.meleeSpeed += 0.05f;
                player.magicCrit += 2;
                player.magicDamage += 0.05f;
                player.rangedCrit += 2;
                player.rangedDamage += 0.05f;
                player.thrownCrit += 2;
                player.thrownDamage += 0.05f;
                player.minionDamage += 0.05f;
                player.minionKB += 0.5f;
                player.moveSpeed += 0.2f;
            }
            else if (id == 71)
            {
                player.meleeEnchant = 1;
            }
            else if (id == 73)
            {
                player.meleeEnchant = 2;
            }
            else if (id == 74)
            {
                player.meleeEnchant = 3;
            }
            else if (id == 75)
            {
                player.meleeEnchant = 4;
            }
            else if (id == 76)
            {
                player.meleeEnchant = 5;
            }
            else if (id == 77)
            {
                player.meleeEnchant = 6;
            }
            else if (id == 78)
            {
                player.meleeEnchant = 7;
            }
            else if (id == 79)
            {
                player.meleeEnchant = 8;
            }
        }
        public void decreaseBaitTimer(int v)
        {
            if (hasAnyBaitDebuffs() && baitTimer > 0)
            {
                baitTimer = Math.Max(0, baitTimer - v);
                updateBaits();
            }
        }

        public void updateCurrentInflictedBaitDebuffs()
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = mod.GetPacket();
                pk.Write((byte)UnuBattleRods.Message.DebuffUpdate);
                pk.Write(player.whoAmI + Main.npc.Length);
                pk.Write(debuffsPresent.Count);
                for (int i = 0; i < debuffsPresent.Count; i++)
                {
                    pk.Write(debuffsPresent[i]);
                }
                pk.Send();
            }
        }

        public void onBobKill(Bobber proj)
        {
            if(currentDiscard != null)
            {
                currentDiscard.onDiscard(this.player,proj.projectile.damage, proj.getStuckEntity());
                currentDiscard = null;
            }
        }
    }
}
