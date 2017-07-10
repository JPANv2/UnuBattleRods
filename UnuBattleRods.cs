using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Rods.HardMode;
using UnuBattleRods.Items.Rods.NormalMode;
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



            group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane", new int[]
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

            //rods
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Battlerod", new int[]
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
            }catch (Exception ex)
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
    }
}
