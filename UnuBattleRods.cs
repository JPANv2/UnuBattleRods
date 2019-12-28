using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;
using UnuBattleRods.Items.Baits.SummonBaits;
using UnuBattleRods.Items.BossBags;
using UnuBattleRods.Items.Currency;
using UnuBattleRods.Items.Materials;
using UnuBattleRods.Items.Rods.HardMode;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.Items.Weapons.Cooler;
using UnuBattleRods.NPCs;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods
{
    public class UnuBattleRods : Mod
    {
        public static string GithubUserName { get { return "jpanv2"; } }
        public static string GithubProjectName { get { return "UnuBattleRods"; } }

        public static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "UnuBattleRods");

        public static string ConfigFileRelativePath
        {
            get { return "Mod Configs/UnuBattleRods/main.json"; }
        }

        public static bool startWithRod = true;
        public static bool startWithBait = true;

        public static bool harderLureRecipes = true;
        public static bool allowFishedItems = true;

        public static bool doesFallOnFloor = true;
        public static bool explosivesDamageEveryone = true;

        public static List<String> fishToReplace = new List<String>();

        public enum Message
        {
            BobProjectilePosition = 0,
            MimicSpawn = 1,
            BobAIUpdate = 2,
            BobDPS = 3,
            HealEffect = 4,
            ManaEffect = 5,
            BaitUpdate = 8,
            DebuffUpdate = 12,
            ReceiveConfig = 14
        }
        public UnuBattleRods()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        public static int fishSteaksCurrencyID = -1;
        public override void Load()
        {
            loadConfig();
            fishSteaksCurrencyID = CustomCurrencyManager.RegisterCurrency(new FishCurrency(ModContent.ItemType<FishSteaks>(), 999));
            base.Load();
        }

        public static void ReloadConfigFromFile()
        {
            UnuBattleRods.loadConfig();
        }

        public static void loadConfig()
        {
            if (!System.IO.Directory.Exists(ConfigPath))
            {
                System.IO.Directory.CreateDirectory(ConfigPath);
            }
            Preferences config = new Preferences(Path.Combine(ConfigPath, "main.json"));
            if (!config.Load())
            {
                config.Put("startWithRod", true);
                config.Put("startWithBait", true);
                config.Put("harderLureRecipes", true);
                config.Put("allowFishedItems", true);
                makeDefaultFishReplaceList();
                config.Put("fishToReplace", fishToReplace);
                config.Put("canBobberFallOnFloor", doesFallOnFloor);
                config.Put("explosivesDamageEveryone", explosivesDamageEveryone);
                config.Save();
            }

            startWithRod = config.Get<bool>("startWithRod", true);
            startWithBait = config.Get<bool>("startWithBait", true);
            harderLureRecipes = config.Get<bool>("harderLureRecipes", true);
            allowFishedItems = config.Get<bool>("allowFishedItems", true);
            doesFallOnFloor = config.Get<bool>("canBobberFallOnFloor", true);
            explosivesDamageEveryone = config.Get<bool>("explosivesDamageEveryone", true);
            fishToReplace.Clear();
            List<string> fr = getStringListFromConfig(config, "fishToReplace");
            if(fr.Count != 0)
            {
                fishToReplace.AddRange(fr);
            }else
            {
                makeDefaultFishReplaceList();
            }
            
        }

        private static void makeDefaultFishReplaceList()
        {
            fishToReplace.Clear();
            fishToReplace.Add("" + ItemID.WoodenCrate);
            fishToReplace.Add("" + ItemID.Bass);
            fishToReplace.Add("" + ItemID.Salmon);
            fishToReplace.Add("" + ItemID.Trout);
            fishToReplace.Add("" + ItemID.Tuna);
            fishToReplace.Add("" + ItemID.AtlanticCod);
            fishToReplace.Add("" + ItemID.NeonTetra);
            fishToReplace.Add("" + ItemID.RedSnapper);
            fishToReplace.Add("" + ItemID.Honeyfin);
            fishToReplace.Add("" + ItemID.Obsidifish);
            return;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Jellyfish (Bait)", new int[]
         {
                 ItemID.PinkJellyfish, ItemID.GreenJellyfish, ItemID.BlueJellyfish
         });
            RecipeGroup.RegisterGroup("UnuBattleRods:Jellies", group);


            AddRGBars();
            AddRGArmors();
            AddRGRods();

        }

        public static bool thoriumPresent = false;

        public override void PostSetupContent()
        {
            Mod bosses = ModLoader.GetMod("BossChecklist");
            if (bosses != null)
            {
                /*
                 "AddBoss",
                 float progression,
                 int/List<int> BossNPCIDs,
                 Mod mod, string bossName,
                 Func<bool> downedBoss,
                 int/List<int> SpawnItemIDs, 
                 int/List<int> CollectionItemIDs, 
                 int/List<int> LootItemIDs, [string spawnInfo], [string despawnMessage], [string texture], [string overrideHeadIconTexture], [Func<bool> bossAvailable]
                 */
                object[] parameters = new object[11];
                parameters[0] = "AddBoss";
                parameters[1] = 5.39f;
                parameters[2] = ModContent.NPCType<CoolerBoss>();
                parameters[3] = this;
                parameters[4] = "Cooler";
                parameters[5] = (Func<bool>)(() => FishWorld.downedCooler);
                parameters[6] = ModContent.ItemType<IceyWorm>();
                parameters[7] = 0;
                parameters[8] = new List<int>
                {
                    ModContent.ItemType<CoolerBossBag>(),
                    ItemID.Hook,
                    ModContent.ItemType<MasterBaiterCertificate>(),
                    ModContent.ItemType<CoolerBattlerod>(),
                    ModContent.ItemType<Melonbrand>(),
                    ModContent.ItemType<MagicSoda>(),
                    ModContent.ItemType<BeerPack>(),
                    ModContent.ItemType<IceCreamer>()
                };
                parameters[9] = "Place a [i:" + this.ItemType("IceyWorm") + "] alone in a chest. Warning: Difficulty increases in hardmode.";
                parameters[10] = "The {0} defeated all players!";

                /*OLD*/
                /*
               object[] parameters = new object[5];
               parameters[0] = "AddBossWithInfo";
               parameters[1] = "Cooler";
               parameters[2] = 5.39f;
               parameters[3] = new Func<bool>(() => FishWorld.downedCooler);
               parameters[4] = "Place a [i:" + this.ItemType("IceyWorm") + "] alone in a chest. Warning: Difficulty increases in hardmode.";
               */
                bosses.Call(parameters);
            }
            thoriumPresent = ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            int i = reader.ReadByte();
            try
            {
                if (i == 0 && Main.netMode == 2)
                {
                    int idx = reader.ReadInt16();
                    int proj = reader.ReadInt16();
                    int posX = reader.ReadInt32();
                    int posY = reader.ReadInt32();
                    if (Main.projectile[proj].type == 0 || !Main.projectile[proj].active)
                        return;
                    Bobber bob = (Main.projectile[proj].modProjectile) as Bobber;
                    if (bob == null)
                        return;
                    Entity target = bob.projectile;
                    if (idx <= -1)
                    {
                        target = bob.projectile;
                        bob.updatePos = true;
                    }
                    if (idx < Main.npc.Length)
                    {
                        target = Main.npc[idx];
                    }
                    else if (idx < Main.npc.Length + Main.player.Length)
                    {
                        target = Main.player[idx - Main.npc.Length];
                    }


                    bob.npcIndex = (short)idx;

                    target.Center = new Microsoft.Xna.Framework.Vector2(posX, posY);
                    bob.projectile.Center = target.Center;

                }
                if (i == 1 && Main.netMode == 2)
                {
                    int p = reader.ReadUInt16();
                    int mimicX = reader.ReadInt32();
                    int mimicY = reader.ReadInt32();
                    FishPlayer f = Main.player[p].GetModPlayer<FishPlayer>();
                    if (f != null)
                    {
                        f.mimicX = mimicX;
                        f.mimicY = mimicY;
                        f.mimicToSpawn = true;
                    }
                }
                if (i == 2)
                {
                    int proj = reader.ReadUInt16();
                    short npcIndex = reader.ReadInt16();
                    float ai0 = reader.ReadSingle();
                    Main.projectile[proj].ai[0] = ai0;
                    ((Bobber)(Main.projectile[proj].modProjectile)).npcIndex = npcIndex;
                }
                if (i == 3 && Main.netMode == 1)
                {
                    int dmg = reader.ReadInt32();
                    if (Main.player[Main.myPlayer].accDreamCatcher)
                    {
                        Main.player[Main.myPlayer].addDPS(dmg);
                    }
                }
                if (i == 4 && Main.netMode != 2)
                {
                    int p = reader.ReadUInt16();
                    int heal = reader.ReadInt32();
                    Player player = Main.player[p];
                    if (player.statLifeMax2 > player.statLife)
                    {
                        player.statLife += heal;
                        if (player.statLife > player.statLifeMax2)
                        {
                            player.statLife = player.statLifeMax2;
                        }
                        player.HealEffect(heal);
                    }
                }

                if (i == 5 && Main.netMode != 2)
                {
                    int p = reader.ReadUInt16();
                    int syphon = reader.ReadInt32();
                    Player player = Main.player[p];
                    if (player.statManaMax2 > player.statMana)
                    {
                        player.statMana += syphon;
                        if (player.statMana > player.statManaMax2)
                        {
                            player.statMana = player.statManaMax2;
                        }
                        player.ManaEffect(syphon);
                    }
                }
                if (i == 6)
                {
                    int who = reader.ReadInt16();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    //float xSpeed = reader.ReadSingle();
                    //float ySpeed = reader.ReadSingle();

                    if (who >= 0 && who < Main.npc.Length)
                    {
                        FishGlobalNPC npcGlobal = Main.npc[who].GetGlobalNPC<FishGlobalNPC>();
                        //npcGlobal.newSpeed = new Vector2(xSpeed, ySpeed);
                        npcGlobal.newCenter = new Vector2(x, y);
                    }
                    else if (who >= Main.npc.Length && who < Main.npc.Length + Main.player.Length)
                    {
                        FishPlayer p = Main.player[who - Main.npc.Length].GetModPlayer<FishPlayer>();
                        //p.newSpeed = new Vector2(xSpeed, ySpeed);
                        p.newCenter = new Vector2(x, y);
                    }
                    else
                    {
                        return;
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket pk = GetPacket();
                        pk.Write((byte)6);
                        pk.Write((short)who);
                        pk.Write(x);
                        pk.Write(y);
                        // pk.Write(xSpeed);
                        // pk.Write(ySpeed);
                        pk.Send();
                    }
                }
                if (i == 7)
                {
                    int who = reader.ReadInt16();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    //float xSpeed = reader.ReadSingle();
                    //float ySpeed = reader.ReadSingle();

                    if (Main.netMode != NetmodeID.MultiplayerClient && who != Main.myPlayer)
                    {
                        FishPlayer p = Main.player[who].GetModPlayer<FishPlayer>();
                        //p.newSpeed = new Vector2(xSpeed, ySpeed);
                        p.newCenter = new Vector2(x, y);
                    }
                    else
                    {
                        return;
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket pk = GetPacket();
                        pk.Write((byte)7);
                        pk.Write((short)who);
                        pk.Write(x);
                        pk.Write(y);
                        // pk.Write(xSpeed);
                        // pk.Write(ySpeed);
                        pk.Send();
                    }
                }
                if (i == 9)
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
                if (i == 10)
                {
                    int plr = reader.ReadInt16();
                    int[] buffs = new int[4];
                    int[] debuffs = new int[4];
                    // ErrorLogger.Log("Recieved update package. Player no " + plr);
                    int buffTime = reader.ReadInt32();
                    FishPlayer pl = Main.player[plr].GetModPlayer<FishPlayer>();
                    for (int k = 0; k < pl.baitBuff.Length; k++)
                    {
                        buffs[k] = reader.ReadInt32();
                    }
                    for (int k = 0; k < pl.baitDebuff.Length; k++)
                    {
                        debuffs[k] = reader.ReadInt32();
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == plr)
                        return;
                    int pbtype = ModContent.BuffType<PoweredBaitBuff>();
                    for (int j = 0; j < 22; j++)
                    {
                        if (pl.player.buffType[j] == pbtype)
                        {
                            pl.player.buffTime[j] = buffTime;
                            pl.baitTimer = buffTime;
                            for (int k = 0; k < pl.baitBuff.Length; k++)
                            {
                                pl.addBaitBuffs(buffTime, k, buffs[k]);
                            }
                            for (int k = 0; k < pl.baitDebuff.Length; k++)
                            {
                                pl.addBaitDebuffs(buffTime, k, debuffs[k]);
                            }
                        }
                    }
                    for (int j = 0; j < 22; j++)
                    {
                        if (pl.player.buffType[j] <= 0)
                        {
                            pl.player.buffType[j] = pbtype;
                            pl.player.buffTime[j] = buffTime;
                            pl.baitTimer = buffTime;
                            for (int k = 0; k < pl.baitBuff.Length; k++)
                            {
                                pl.addBaitBuffs(buffTime, k, buffs[k]);
                            }
                            for (int k = 0; k < pl.baitDebuff.Length; k++)
                            {
                                pl.addBaitDebuffs(buffTime, k, debuffs[k]);
                            }
                        }
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket pk = GetPacket();
                        pk.Write((byte)10);
                        pk.Write((short)plr);
                        pk.Write(buffTime);
                        for (int k = 0; k < pl.baitBuff.Length; k++)
                        {
                            pk.Write(pl.baitBuff[k]);
                        }
                        for (int k = 0; k < pl.baitDebuff.Length; k++)
                        {
                            pk.Write(pl.baitDebuff[k]);
                        }
                        pk.Send();
                    }
                }
                if (i == 12)
                {
                    int updatee = reader.ReadInt32();
                    int count = reader.ReadInt32();
                    List<int> debuffs = new List<int>();
                    for (int k = 0; k < count; k++)
                    {
                        debuffs.Add(reader.ReadInt32());
                    }
                    if (updatee >= Main.npc.Length)
                    {
                        FishPlayer pl = Main.player[updatee - Main.npc.Length].GetModPlayer<FishPlayer>();
                        pl.debuffsPresent.Clear();
                        pl.debuffsPresent.AddRange(debuffs);
                    }
                    else
                    {
                        NPC npc = Main.npc[updatee];
                        FishGlobalNPC fgnpc = npc.GetGlobalNPC<FishGlobalNPC>();
                        fgnpc.debuffsPresent.Clear();
                        fgnpc.debuffsPresent.AddRange(debuffs);
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket pk = GetPacket();
                        pk.Write((byte)UnuBattleRods.Message.DebuffUpdate);
                        pk.Write(updatee);
                        pk.Write(count);
                        for (int k = 0; k < count; k++)
                        {
                            pk.Write(debuffs[k]);
                        }
                        pk.Send();
                    }
                }
                if (i == 14)//Syncronize Config to server options
                {
                    if (Main.netMode == NetmodeID.Server)
                    {
                        sendMessage14(-1);
                    }
                    else
                    {
                        byte flags = reader.ReadByte();
                        harderLureRecipes = ((flags & 1) == 1);
                        allowFishedItems = ((flags & 2) == 2);
                        doesFallOnFloor = ((flags & 4) == 4);
                        explosivesDamageEveryone = ((flags & 8) == 8);
                        int fishCount = reader.ReadInt32();
                        fishToReplace.Clear();
                        for (int k = 0; k < fishCount; k++)
                        {
                            fishToReplace.Add(reader.ReadString());
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                if (Main.netMode != 2)
                {
                    Main.NewText("Exception on message " + i + ": " + ex.ToString());
                }
                else
                {
                    Console.WriteLine("Exception on message " + i + ": " + ex.ToString());
                }
            }
        }

        public static void sendMessage14(int player)
        {
            ModPacket pk = ModLoader.GetMod("UnuBattleRods").GetPacket();
            pk.Write((byte)14);
            byte flags = (byte)(harderLureRecipes ? 1 : 0);
            flags |= (byte)(allowFishedItems ? 2 : 0);
            flags |= (byte)(doesFallOnFloor ? 4 : 0);
            flags |= (byte)(explosivesDamageEveryone ? 8 : 0);
            pk.Write(flags);
            pk.Write(fishToReplace.Count);
            for (int k = 0; k < fishToReplace.Count; k++)
            {
                pk.Write(fishToReplace[k]);
            }
            if (player < 0 || player > Main.player.Length)
            {
                pk.Send();
            }
            else
            {
                pk.Send(player);
            }
        }

        public void AddRGBars()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane", new int[]
            {
                 ItemID.DemoniteBar, ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:BossBars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Rotten Chunk/Vertebrea", new int[]
            {
                 ItemID.RottenChunk, ItemID.Vertebrae
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:EvilDrop", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Shadow Scales/Tissue Samples", new int[]
            {
                 ItemID.ShadowScale, ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:EvilScale", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Bar", new int[]
            {
                 ItemID.CopperBar, ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Bar", new int[]
           {
                 ItemID.SilverBar, ItemID.TungstenBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Bar", new int[]
           {
                 ItemID.GoldBar, ItemID.PlatinumBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Bar", new int[]
           {
                 ItemID.CobaltBar, ItemID.PalladiumBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Bar", new int[]
          {
                 ItemID.MythrilBar, ItemID.OrichalcumBar
          });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Bar", new int[]
          {
                 ItemID.AdamantiteBar, ItemID.TitaniumBar
          });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Bars", group);
        }

        public void AddRGArmors()
        {
            //boss bars
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Helmet", new int[]
            {
                 ItemID.ShadowHelmet, ItemID.AncientShadowHelmet,ItemID.CrimsonHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:BossHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Body", new int[]
            {
                 ItemID.ShadowScalemail,ItemID.AncientShadowScalemail, ItemID.CrimsonScalemail
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:BossBody", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Legs", new int[]
            {
                 ItemID.ShadowGreaves,ItemID.AncientShadowGreaves, ItemID.CrimsonGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:BossLegs", group);

            //copper/tin tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Helmet", new int[]
            {
                 ItemID.CopperHelmet, ItemID.TinHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Body", new int[]
            {
                 ItemID.CopperChainmail, ItemID.TinChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Legs", new int[]
            {
                 ItemID.CopperGreaves, ItemID.TinGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Legs", group);

            //iron/lead tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Helmet", new int[]
            {
                 ItemID.IronHelmet, ItemID.AncientIronHelmet, ItemID.LeadHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier1Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Body", new int[]
            {
                 ItemID.IronChainmail, ItemID.LeadChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier1Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Legs", new int[]
            {
                 ItemID.IronGreaves, ItemID.LeadGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier1Legs", group);

            //silver/tungsten tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Helmet", new int[]
            {
                 ItemID.SilverHelmet, ItemID.TungstenHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Body", new int[]
            {
                 ItemID.SilverChainmail, ItemID.TungstenChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Legs", new int[]
            {
                 ItemID.SilverGreaves, ItemID.TungstenGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Legs", group);

            //gold/platinum tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Helmet", new int[]
            {
                 ItemID.GoldHelmet, ItemID.AncientGoldHelmet, ItemID.PlatinumHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Body", new int[]
            {
                 ItemID.GoldChainmail, ItemID.PlatinumChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Legs", new int[]
            {
                 ItemID.GoldGreaves, ItemID.PlatinumGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Legs", group);

            //hardmode ores

            //cobalt/palladium
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Helmet", new int[]
            {
                 ItemID.CobaltHelmet, ItemID.CobaltMask, ItemID.CobaltHat,
                 ItemID.AncientCobaltHelmet,
                 ItemID.PalladiumHelmet, ItemID.PalladiumMask, ItemID.PalladiumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Body", new int[]
            {
                 ItemID.CobaltBreastplate, ItemID.AncientCobaltBreastplate, ItemID.PalladiumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Legs", new int[]
            {
                 ItemID.CobaltLeggings, ItemID.AncientCobaltLeggings ,ItemID.PalladiumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Legs", group);

            //mythirl/orichalcum
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Helmet", new int[]
            {
                 ItemID.MythrilHelmet, ItemID.MythrilHood, ItemID.MythrilHat,
                 ItemID.OrichalcumHelmet, ItemID.OrichalcumMask, ItemID.OrichalcumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Body", new int[]
            {
                 ItemID.MythrilChainmail, ItemID.OrichalcumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Legs", new int[]
            {
                 ItemID.MythrilGreaves, ItemID.OrichalcumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Legs", group);

            //Adamantite/titanium

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Helmet", new int[]
            {
                 ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear,
                 ItemID.TitaniumHelmet, ItemID.TitaniumMask, ItemID.TitaniumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Body", new int[]
            {
                 ItemID.AdamantiteBreastplate, ItemID.TitaniumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Legs", new int[]
            {
                 ItemID.AdamantiteLeggings, ItemID.TitaniumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Legs", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Hallowed Helmet", new int[]
            {
                 ItemID.HallowedHelmet, ItemID.HallowedMask, ItemID.HallowedHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:HallowedHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Chlorophyte Helmet", new int[]
            {
                 ItemID.ChlorophyteHelmet, ItemID.ChlorophyteMask, ItemID.ChlorophyteHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:ChlorophyteHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Shroomite Helmet", new int[]
            {
                 ItemID.ShroomiteHelmet, ItemID.ShroomiteMask, ItemID.ShroomiteHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:ShroomiteHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Spectre Helmet", new int[]
            {
                 ItemID.SpectreHood, ItemID.SpectreMask
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:SpectreHelmets", group);

        }

        public void AddRGRods()
        {
            //rods
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Battlerod", new int[]
            {
                 ModContent.ItemType<CopperBattlerod>(), ModContent.ItemType<TinBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Battlerod", new int[]
            {
                 ModContent.ItemType<IronBattlerod>(), ModContent.ItemType<LeadBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Battlerod", new int[]
            {
                 ModContent.ItemType<SilverBattlerod>(), ModContent.ItemType<TungstenBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Battlerod", new int[]
           {
                 ModContent.ItemType<GoldBattlerod>(), ModContent.ItemType<PlatinumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Battlerod", new int[]
        {
                 ModContent.ItemType<EvilRodOfDarkness>(), ModContent.ItemType<EvilRodOfBlood>()
        });
            RecipeGroup.RegisterGroup("UnuBattleRods:EvilRods", group);


            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Battlerod", new int[]
           {
                 ModContent.ItemType<CobaltBattlerod>(), ModContent.ItemType<PalladiumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Battlerod", new int[]
           {
                 ModContent.ItemType<MythrilBattlerod>(), ModContent.ItemType<OrichalcumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Battlerod", new int[]
           {
                 ModContent.ItemType<AdamantiteBattlerod>(), ModContent.ItemType<TitaniumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Rods", group);
        }


        public static int getTypeFromTag(string tag)
        {
            int type = 0;
            if (!Int32.TryParse(tag, out type))
            {
                Mod m = ModLoader.GetMod(tag.Split(':')[0]);
                if (m != null)
                    type = m.ItemType(tag.Split(':')[1]);
            }
            return type;
        }

        public static Item getItemFromTag(string tag, bool noMatCheck = false)
        {
            Item ret = new Item();
            int type = getTypeFromTag(tag);
            if (type != 0)
                ret.SetDefaults(type, noMatCheck);
            return ret;
        }

        public static string ItemToTag(Item itm)
        {
            String type = "" + itm.type;
            if (itm.modItem != null)
            {
                type = itm.modItem.mod.Name + ":" + itm.modItem.Name;
            }

            return type;
        }

        public static int getItemTypeFromTag(string tag)
        {
            int type = 0;
            if (!Int32.TryParse(tag, out type))
            {
                Mod m = ModLoader.GetMod(tag.Split(':')[0]);
                if (m != null)
                    type = m.ItemType(tag.Split(':')[1]);
            }
            return type;
        }

        public static List<string> getStringListFromConfig(Preferences configuration, string tokenID)
        {
            List<string> ans = new List<string>();
            Newtonsoft.Json.Linq.JArray o = configuration.Get<Newtonsoft.Json.Linq.JArray>(tokenID, null);
            if (o != null)
            {
                foreach (Newtonsoft.Json.Linq.JToken j in o)
                {
                    ans.Add(j.ToString());
                }
            }
            return ans;
        }

        public override void AddRecipes()
        {
            AddLureRecipes();
            AddSelectiveRecipes();
        }

       

        ModRecipe recipe1Lure;
        ModRecipe recipe1Loser;
        ModRecipe recipe2Lure;
        ModRecipe recipe2Loser;
        ModRecipe recipe4Lure;
        ModRecipe recipe4LureHM;
        ModRecipe recipe4Loser;
        ModRecipe recipe4LoserHM;
        ModRecipe recipe8Lure;
        ModRecipe recipe8LureHM;
        ModRecipe recipe16Lure;
        ModRecipe recipe16LureHM;
        ModRecipe recipe32Lure;
        ModRecipe recipe32LureHM;

        public void AddLureRecipes()
        {
            recipe1Lure = new ModRecipe(this);
            recipe1Lure.AddIngredient(ItemID.Cobweb, 25);
            recipe1Lure.AddIngredient(ItemID.Hook, 1);
            recipe1Lure.AddTile(TileID.WorkBenches);
            recipe1Lure.SetResult(this, "ExtraLure");

            recipe1Loser = new ModRecipe(this);
            recipe1Loser.AddIngredient(ItemID.Cobweb, 25);
            recipe1Loser.AddIngredient(ItemID.Hook, 1);
            recipe1Loser.AddTile(TileID.WorkBenches);
            recipe1Loser.SetResult(this, "BobLoser");

            recipe2Lure = new ModRecipe(this);
            recipe2Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe2Lure.AddIngredient(this, "ExtraLure", 2);
            recipe2Lure.AddTile(TileID.WorkBenches);
            recipe2Lure.SetResult(this, "DoubleLure");

            recipe2Loser = new ModRecipe(this);
            recipe2Loser.AddIngredient(ItemID.Cobweb, 10);
            recipe2Loser.AddIngredient(this, "BobLoser", 2);
            recipe2Loser.AddTile(TileID.WorkBenches);
            recipe2Loser.SetResult(this, "DoubleBobLoser");

            recipe4Lure = new EasyRecipe(this);
            recipe4Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe4Lure.AddIngredient(this, "DoubleLure", 2);
            recipe4Lure.AddTile(TileID.WorkBenches);
            recipe4Lure.SetResult(this, "QuadLure");

            recipe4LureHM = new HardRecipe(this);
            recipe4LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe4LureHM.AddIngredient(this, "DoubleLure", 2);
            recipe4LureHM.AddIngredient(this, "StarMix", 6);
            recipe4LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe4LureHM.SetResult(this, "QuadLure");

            recipe4Loser = new EasyRecipe(this);
            recipe4Loser.AddIngredient(ItemID.Cobweb, 10);
            recipe4Loser.AddIngredient(this, "DoubleBobLoser", 2);
            recipe4Loser.AddTile(TileID.WorkBenches);
            recipe4Loser.SetResult(this, "QuadBobLoser");

            recipe4LoserHM = new HardRecipe(this);
            recipe4LoserHM.AddIngredient(ItemID.Cobweb, 25);
            recipe4LoserHM.AddIngredient(this, "DoubleBobLoser", 2);
            recipe4LoserHM.AddIngredient(this, "StarMix", 6);
            recipe4LoserHM.AddTile(TileID.TinkerersWorkbench);
            recipe4LoserHM.SetResult(this, "QuadBobLoser");

            recipe8Lure = new EasyRecipe(this);
            recipe8Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe8Lure.AddIngredient(this, "QuadLure", 2);
            recipe8Lure.AddTile(TileID.TinkerersWorkbench);
            recipe8Lure.SetResult(this, "OctoLure");

            recipe8LureHM = new HardRecipe(this);
            recipe8LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe8LureHM.AddIngredient(this, "QuadLure", 2);
            recipe8LureHM.AddIngredient(this, "EnergyAmalgamate", 4);
            recipe8LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe8LureHM.SetResult(this, "OctoLure");
                       
            recipe16Lure = new EasyRecipe(this);
            recipe16Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe16Lure.AddRecipeGroup("UnuBattleRods:HMTier2Bars", 5);
            recipe16Lure.AddIngredient(this, "OctoLure", 2);
            recipe16Lure.AddTile(TileID.TinkerersWorkbench);
            recipe16Lure.SetResult(this, "BoxOfLures");

            recipe16LureHM = new HardRecipe(this);
            recipe16LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe16LureHM.AddIngredient(ItemID.LunarBar, 5);
            recipe16LureHM.AddIngredient(this, "OctoLure", 2);
            recipe16LureHM.AddIngredient(this, "EnergyAmalgamate", 8);
            recipe16LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe16LureHM.SetResult(this, "BoxOfLures");
            
            
            recipe32Lure = new EasyRecipe(this);
            recipe32Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe32Lure.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe32Lure.AddIngredient(this, "BoxOfLures", 2);
            recipe32Lure.AddTile(TileID.TinkerersWorkbench);
            recipe32Lure.SetResult(this, "BoxOfCountlessLures");

            recipe32LureHM = new HardRecipe(this);
            recipe32LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe32LureHM.AddIngredient(this, "BoxOfLures", 2);
            recipe32LureHM.AddIngredient(this, "FractaliteBar", 12);
            recipe32LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe32LureHM.SetResult(this, "BoxOfCountlessLures");        

            recipe1Lure.AddRecipe();
            recipe1Loser.AddRecipe();
            recipe2Lure.AddRecipe();
            recipe2Loser.AddRecipe();

            recipe4LureHM.AddRecipe();
            recipe4LoserHM.AddRecipe();
            recipe8LureHM.AddRecipe();
            recipe16LureHM.AddRecipe();
            recipe32LureHM.AddRecipe();
            
            recipe4Lure.AddRecipe();
            recipe4Loser.AddRecipe();
            recipe8Lure.AddRecipe();
            recipe16Lure.AddRecipe();
            recipe32Lure.AddRecipe();

            


            ModRecipe recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "ExtraLure");
            recipeConversion.SetResult(this, "BobLoser");
            recipeConversion.AddRecipe();

            recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "BobLoser");
            recipeConversion.SetResult(this, "ExtraLure");
            recipeConversion.AddRecipe();

            recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "DoubleLure");
            recipeConversion.SetResult(this, "DoubleBobLoser");
            recipeConversion.AddRecipe();

            recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "DoubleBobLoser");
            recipeConversion.SetResult(this, "DoubleLure");
            recipeConversion.AddRecipe();

            recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "QuadLure");
            recipeConversion.SetResult(this, "QuadBobLoser");
            recipeConversion.AddRecipe();

            recipeConversion = new ModRecipe(this);
            recipeConversion.AddIngredient(this, "QuadBobLoser");
            recipeConversion.SetResult(this, "QuadLure");
            recipeConversion.AddRecipe();

        }

        ModRecipe selectiveBobbers;

        ModRecipe doubleSelectiveBobbers;
        ModRecipe doubleSelectiveBobbersHM;
        ModRecipe turretBobbers;
        ModRecipe turretBobbersHM;

        ModRecipe smartBobbers;
       

        public void AddSelectiveRecipes()
        {
            selectiveBobbers = new ModRecipe(this);
            selectiveBobbers.AddIngredient(this, "ExtraLure", 2);
            selectiveBobbers.AddIngredient(ItemID.IronBar, 10);
            selectiveBobbers.AddIngredient(this, "Sandpaper");
            selectiveBobbers.anyIronBar = true;
            selectiveBobbers.AddTile(TileID.TinkerersWorkbench);
            selectiveBobbers.SetResult(this, "SelectiveBobbers");

            doubleSelectiveBobbers = new EasyRecipe(this);
            doubleSelectiveBobbers.AddIngredient(this, "DoubleLure", 2);
            doubleSelectiveBobbers.AddIngredient(this, "Sandpaper");
            doubleSelectiveBobbers.AddTile(TileID.TinkerersWorkbench);
            doubleSelectiveBobbers.SetResult(this, "DoubleSelectiveBobbers");

            doubleSelectiveBobbersHM = new HardRecipe(this);
            doubleSelectiveBobbersHM.AddIngredient(this, "SelectiveBobbers", 2);
            doubleSelectiveBobbersHM.AddIngredient(this, "Sandpaper", 5);
            doubleSelectiveBobbersHM.AddIngredient(ItemID.IronBar, 10);
            doubleSelectiveBobbersHM.AddIngredient(this, "LesserEnergyAmalgamate", 2);
            doubleSelectiveBobbersHM.anyIronBar = true;
            doubleSelectiveBobbersHM.AddTile(TileID.TinkerersWorkbench);
            doubleSelectiveBobbersHM.SetResult(this, "DoubleSelectiveBobbers");


            turretBobbers = new EasyRecipe(this);
            turretBobbers.AddIngredient(ItemID.ChlorophyteBar, 10);
            turretBobbers.AddIngredient(this, "Sandpaper");
            turretBobbers.AddTile(TileID.TinkerersWorkbench);
            turretBobbers.SetResult(this, "TurretBobbers");

            turretBobbersHM = new HardRecipe(this);
            turretBobbersHM.AddIngredient(ItemID.ChlorophyteBar, 20);
            turretBobbersHM.AddIngredient(this, "Sandpaper");
            turretBobbersHM.AddIngredient(this, "EnergyAmalgamate", 4);
            turretBobbersHM.AddTile(TileID.TinkerersWorkbench);
            turretBobbersHM.SetResult(this, "TurretBobbers");

            selectiveBobbers.AddRecipe();
            doubleSelectiveBobbers.AddRecipe();
            doubleSelectiveBobbersHM.AddRecipe();
            turretBobbers.AddRecipe();
            turretBobbersHM.AddRecipe();

        }

        public static void sendChat(string msg)
        {
            sendChat(msg, Color.White);
        }

        public static void debugChat(string msg)
        {
            sendChat(msg, Color.Yellow);
        }

        public static void sendChatToAll(string msg)
        {
            sendChatToAll(msg, Color.White);
        }

        public static void sendChat(string msg, Color c)
        {
            if (Main.netMode == 0 || Main.netMode == 1) // Single Player
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        Main.NewText(toSend[i], c.R, c.G, c.B);
                }
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void sendChatToAll(string msg, Color c)
        {
            if (Main.netMode == 2) // Server
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(toSend[i]), c);
                }

            }
            else if (Main.netMode == 1)
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(toSend[i]), c, Main.myPlayer);
                }
            }
            else if (Main.netMode == 0) // Single Player
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        Main.NewText(toSend[i], c.R, c.G, c.B);
                }
            }
        }
    }
}
