using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Rods.HardMode;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.NPCs;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods
{
	public class UnuBattleRods : Mod
	{
       public enum Message
        {
            BobProjectilePosition = 0,
            MimicSpawn = 1,
            BobAIUpdate = 2,
            BobDPS = 3,
            HealEffect = 4,
            ManaEffect = 5
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

        

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
           int i = reader.ReadByte();
            try
            {
                if (i == 0 && Main.netMode == 2)
                {
                    int idx = reader.ReadInt16();
                    int proj = reader.ReadUInt16();
                    int posX = reader.ReadInt32();
                    int posY = reader.ReadInt32();
                    if (idx <= -1)
                    {
                        return;
                    }
                    Bobber bob = (Bobber)(Main.projectile[proj].modProjectile);
                    Entity target = bob.projectile;
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
                    FishPlayer f = Main.player[p].GetModPlayer<FishPlayer>(this);
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
                if(i == 6)
                {
                    int who = reader.ReadInt16();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    //float xSpeed = reader.ReadSingle();
                    //float ySpeed = reader.ReadSingle();

                    if(who >= 0 && who < Main.npc.Length)
                    {
                        FishGlobalNPC npcGlobal = Main.npc[who].GetGlobalNPC<FishGlobalNPC>();
                        //npcGlobal.newSpeed = new Vector2(xSpeed, ySpeed);
                        npcGlobal.newCenter = new Vector2(x, y);
                    }else if (who >= Main.npc.Length && who < Main.npc.Length + Main.player.Length)
                    {
                        FishPlayer p = Main.player[who - Main.npc.Length].GetModPlayer<FishPlayer>();
                        //p.newSpeed = new Vector2(xSpeed, ySpeed);
                        p.newCenter = new Vector2(x, y);
                    }else
                    {
                        return;
                    }
                    if(Main.netMode == NetmodeID.Server)
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

            }
            catch (Exception ex)
            {
                if(Main.netMode != 2)
                {
                    Main.NewText("Exception on message " + i + ": " + ex.ToString());
                }else
                {
                    Console.WriteLine("Exception on message " + i + ": " + ex.ToString());
                }
            }
        }


        public void AddRGBars()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane", new int[]
            {
                 ItemID.DemoniteBar, ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:BossBars", group);

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
                 this.ItemType<CopperBattlerod>(), this.ItemType<TinBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier0Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Battlerod", new int[]
            {
                 this.ItemType<IronBattlerod>(), this.ItemType<LeadBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Battlerod", new int[]
            {
                 this.ItemType<SilverBattlerod>(), this.ItemType<TungstenBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Battlerod", new int[]
           {
                 this.ItemType<GoldBattlerod>(), this.ItemType<PlatinumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:Tier3Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Battlerod", new int[]
        {
                 this.ItemType<EvilRodOfDarkness>(), this.ItemType<EvilRodOfBlood>()
        });
            RecipeGroup.RegisterGroup("UnuBattleRods:EvilRods", group);


            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Battlerod", new int[]
           {
                 this.ItemType<CobaltBattlerod>(), this.ItemType<PalladiumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Battlerod", new int[]
           {
                 this.ItemType<MythrilBattlerod>(), this.ItemType<OrichalcumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Battlerod", new int[]
           {
                 this.ItemType<AdamantiteBattlerod>(), this.ItemType<TitaniumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRods:HMTier3Rods", group);
        }
    }
}
