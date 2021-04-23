using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.BossBags;
using UnuBattleRods.Items.Crates;
using UnuBattleRods.Items.Materials;
using UnuBattleRods.Items.Parts;
using UnuBattleRods.Items.Rods;
using UnuBattleRods.Items.Rods.HardMode;
using UnuBattleRods.Items.Rods.NormalMode;
using UnuBattleRods.Items.Weapons.Cooler;
using UnuBattleRods.NPCs;

namespace UnuBattleRods
{
    public partial class UnuBattleRods : Mod
    {
        private void AddPartSupport()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return; //If part mod not loaded, no need for support
            parts.Call();
            AddNewPart();
            AddCratePartRecipes();
            AddBattlerodPartRecipes();
            AddGeneralPartRecipes();
            addFishLadyRecipes();
            addCoolerDropRecipes();
        }

        /*In this function, we add the Cooler boss part and the FishSteakPart, that will
         be used by this mod's recipes */
        private void AddNewPart()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            /*this will simply add a part, no drop rate or anything. Good for Currency Parts, or parts given to the player by other means (such as quests).*/
            parts.Call("AddNewPart", ModContent.ItemType<FishSteakPart>());

            /*this adds a Boss part to the npc. If the NPC is a boss, the parts will fall between 2-5 from the boss when defeated*/
            parts.Call("AddPartToDropFromNPC", ModContent.ItemType<CoolerPart>(), ModContent.NPCType<CoolerBoss>());

            /*this adds an existing part (the mimic part) to this mod's mimic. It will drop 1-3 as it is not a boss.*/
            parts.Call("AddPartToDropFromNPC", "ARareItemSwapJPANs:MimicPart", ModContent.NPCType<CrateMimic>());

            /*You can also use the AddPartToDropAt to make any existing part drop from a new circumstance, as stated here*/
            parts.Call("AddPartToDropAt",
                "weather", //Using weather because it's the least filled one, so there is more chance to drop as long as it isn't raining or sandstorming
                 "ARareItemSwapJPANs:FishingPart",
                 (Func<bool>)(() => 
                 ((Main.rand.Next(4) == 0) && Main.player[Main.myPlayer].HeldItem.modItem != null && Main.player[Main.myPlayer].HeldItem.modItem is BattleRod)
                 )
                 );

        }

        /*In this function, we will be adding recipes for the mod's uncraftable drops, 
         such as the Shadowflame and the Betsy's scales*/
        private void AddGeneralPartRecipes()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            object[] parameters = new object[10];
            parameters[0] = "AddPartRecipe";
            parameters[1] = "UnuBattleRods:BetsyScales";
            parameters[2] = 2;
            parameters[3] = true; //irreversible
            parameters[4] = 1;  //total number of parts
            parameters[5] = "ARareItemSwapJPANs:BetsyPart";
            parameters[6] = 1;
            parameters[7] = "Unusacies Battle Rods";
            parameters[8] = null; //No need to check for boss defeated, as you can only get parts after defeating the boss
            parameters[9] = new List<String>()
            {
                "By Boss/Betsy/Material",
                "Material"
            };

            object[] toSend = new object[10];
            Array.Copy(parameters, toSend, 10); /*with this, we can reuse some parts of the mod call*/
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:Shadowflame";
            parameters[2] = 1;
            parameters[5] = "ARareItemSwapJPANs:GoblinSummonerPart";
            parameters[9] = new List<String>()
            {
                "By Boss/Goblin Summoner/Material",
                "Material"
            };
            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            /*can also be called like this*/
            parts.Call("AddPartRecipe",
                "UnuBattleRods:SeaweedStar", 2,
                false,
                1,
                "ARareItemSwapJPANs:FishingPart", 1,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "By Biome/Ocean/Material",
                     "Fishing/Material",
                     "Material/Fishing"
                });

            parts.Call("AddPartRecipe",
                "UnuBattleRods:HoneyStar", 2,
                false,
                1,
                "ARareItemSwapJPANs:FishingPart", 1,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "By Biome/Jungle/Material",
                     "Fishing/Material",
                     "Material/Fishing"
                });

            parts.Call("AddPartRecipe",
                "UnuBattleRods:CrustyStar", 1,
                false,
                1,
                "ARareItemSwapJPANs:FishingPart", 2,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "By Biome/Hell/Material",
                     "Fishing/Material",
                     "Material/Fishing"
                });

            parts.Call("AddPartRecipe",
                "UnuBattleRods:FlinxFur", 1,
                false,
                1,
                "ARareItemSwapJPANs:IceUndergroundPart", 5,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "By Biome/Snow (Ice)/Material",
                     "Material"
                });

            parts.Call("AddPartRecipe",
                "UnuBattleRods:FungalSpores", 25,
                false,
                1,
                "ARareItemSwapJPANs:GlowingMushroomUndergroundPart", 1,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "By Biome/Glowing Mushroom/Material",
                     "Material"
                });

        }

        private void AddCratePartRecipes()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            object[] parameters = new object[12];
            parameters[0] = "AddPartRecipe";
            parameters[1] = "UnuBattleRods:AlienCrate";
            parameters[2] = 1;
            parameters[3] = false; //reversible
            parameters[4] = 2;  //total number of parts
            parameters[5] = "ARareItemSwapJPANs:FishingPart";
            parameters[6] = 5;
            parameters[7] = "ARareItemSwapJPANs:MartianInvasionPart";
            parameters[8] = 1;
            parameters[9] = "Unusacies Battle Rods";
            parameters[10] = null;
            parameters[11] = new List<String>()
            {
                "By Event/Martian Invasion/Crate",
                "Fishing/Crate"
            };

            object[] toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:BloodCrate";
            parameters[7] = "ARareItemSwapJPANs:BloodMoonPart";
            parameters[11] = new List<String>()
            {
                "By Event/Blood Moon/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:EclipseCrate";
            parameters[7] = "ARareItemSwapJPANs:SolarEclipsePart";
            parameters[11] = new List<String>()
            {
                "By Event/Solar Eclipse/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:FrostLegionCrate";
            parameters[7] = "ARareItemSwapJPANs:FrostLegionPart";
            parameters[11] = new List<String>()
            {
                "By Event/Frost Legion/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:FrostMoonCrate";
            parameters[7] = "ARareItemSwapJPANs:FrostMoonPart";
            parameters[11] = new List<String>()
            {
                "By Event/Frost Moon/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:FrostMoonCrate";
            parameters[7] = "ARareItemSwapJPANs:FrostMoonPart";
            parameters[11] = new List<String>()
            {
                "By Event/Frost Moon/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:SpookyCrate";
            parameters[7] = "ARareItemSwapJPANs:PumpkinMoonPart";
            parameters[11] = new List<String>()
            {
                "By Event/Pumpkin Moon/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:GoblinCrate";
            parameters[7] = "ARareItemSwapJPANs:GoblinInvasionPart";
            parameters[11] = new List<String>()
            {
                "By Event/Goblin Invasion/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:OldOnesCrate";
            parameters[7] = "ARareItemSwapJPANs:DefenderShopPart";
            parameters[11] = new List<String>()
            {
                "By Event/Old Ones Army/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:SlimeCrate";
            parameters[7] = "ARareItemSwapJPANs:SlimeRainPart";
            parameters[11] = new List<String>()
            {
                "By Event/Slime Rain/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:TreasureCrate";
            parameters[7] = "ARareItemSwapJPANs:PirateInvasionPart";
            parameters[11] = new List<String>()
            {
                "By Event/Pirate Invasion/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);


            parameters[1] = "UnuBattleRods:SnowstormCrate";
            parameters[7] = "ARareItemSwapJPANs:RainPart";
            parameters[11] = new List<String>()
            {
                "By Weather/Rain/Crate",
                "By Biome/Snow (Ice)/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:SandstormCrate";
            parameters[7] = "ARareItemSwapJPANs:SandstormPart";
            parameters[11] = new List<String>()
            {
                "By Weather/Sandstorm/Crate",
                "By Biome/Desert/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:GraniteCrate";
            parameters[7] = "ARareItemSwapJPANs:GraniteUndergroundPart";
            parameters[11] = new List<String>()
            {
                "By Biome/Granite/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:MarbleCrate";
            parameters[7] = "ARareItemSwapJPANs:MarbleUndergroundPart";
            parameters[11] = new List<String>()
            {
                "By Biome/Marble/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:MeteorCrate";
            parameters[7] = "ARareItemSwapJPANs:MeteoriteSurfacePart";
            parameters[11] = new List<String>()
            {
                "By Biome/Meteorite/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:SoulCrate";
            parameters[7] = "ARareItemSwapJPANs:HardmodePart";
            parameters[11] = new List<String>()
            {
                "By Biome/Corruption/Crate",
                "By Biome/Crimson/Crate",
                "By Biome/Hallowed/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:WingCrate";
            parameters[3] = true;
            parameters[6] = 25;
            parameters[7] = "ARareItemSwapJPANs:PostPlanteraPart";
            parameters[11] = new List<String>()
            {
                "By Biome/Sky/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:AnkhCrate";
            parameters[11] = new List<String>()
            {
                "By Biome/Sky/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:LuminiteCrate";
            parameters[3] = true;
            parameters[6] = 5;
            parameters[7] = "ARareItemSwapJPANs:PostMoonLordPart";
            parameters[11] = new List<String>()
            {
                "By Biome/Forest/Crate",
                "Fishing/Crate"
            };
            toSend = new object[12];
            Array.Copy(parameters, toSend, 12);
            parts.Call(toSend);

            parameters = new object[10];
            parameters[0] = "AddPartRecipe";
            parameters[1] = "UnuBattleRods:BeeCrate";
            parameters[2] = 1;
            parameters[3] = false; //reversible
            parameters[4] = 1;  //total number of parts
            parameters[5] = "ARareItemSwapJPANs:FishingPart";
            parameters[6] = 5;
            parameters[7] = "Unusacies Battle Rods";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<BeeBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Jungle/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:ChlorophyteCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<ChlorophyteBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Jungle/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:CorruptCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<CorruptBattlerod>() ||
                                               Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<EvilRodOfDarkness>());
            parameters[9] = new List<String>()
            {
                "By Biome/Corruption/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:CrimsonCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<CrimsonBattlerod>() ||
                                               Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<EvilRodOfBlood>());
            parameters[9] = new List<String>()
            {
                "By Biome/Crimson/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:HallowedCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<HallowedBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Hallowed/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:ObsidianCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<HellstoneBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Hell/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:ShroomiteCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<ShroomiteBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Glowing Mushroom/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            parameters[1] = "UnuBattleRods:TerraCrate";
            parameters[8] = (Func<bool>)(() => Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<TerraBattlerod>());
            parameters[9] = new List<String>()
            {
                "By Biome/Forest/Crate",
                "Fishing/Crate"
            };

            toSend = new object[10];
            Array.Copy(parameters, toSend, 10);
            parts.Call(toSend);

            /*It is also valid to send an Item, ModItem or ItemType(int), not just tags*/
            parts.Call("AddPartRecipe",
              ModContent.ItemType<CritterCrate>(), 1,
              false,
              1,
              "ARareItemSwapJPANs:FishingPart", 3,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                     "By Biome/Forest/Material",
                     "Fishing/Material",
                     "Material/Fishing"
              });

            parts.Call("AddPartRecipe",
              ModContent.ItemType<MimicCrate>(), 1,
              false,
              1,
              "ARareItemSwapJPANs:FishingPart", 20,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                     "By Biome/Ocean/Material",
                     "Fishing/Material",
                     "Material/Fishing"
              });
        }

        private void AddBattlerodPartRecipes()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            parts.Call("AddPartRecipe",
              ModContent.ItemType<BeeBattlerod>(), 1,
              false,
              1,
              "ARareItemSwapJPANs:QueenBeePart", 25,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                    "Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod",
                     "By Boss/Queen Bee/Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "By Boss/Queen Bee/Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod"

              });

            parts.Call("AddPartRecipe",
              ModContent.ItemType<FishronBattlerod>(), 1,
              false,
              1,
              "ARareItemSwapJPANs:DukeFishronPart", 25,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                    "Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod",
                     "By Boss/Duke Fishron/Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "By Boss/Duke Fishron/Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod"

              });

            parts.Call("AddPartRecipe",
             ModContent.ItemType<DungeonBattlerod>(), 1,
             false,
             1,
             "ARareItemSwapJPANs:DungeonUndergroundPart", 20,
              "Unusacies Battle Rods",
              null,
              new List<String>()
             {
                    "Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod",
                    "By Biome/Dungeon/Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "By Biome/Dungeon/Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod"
             });
        }

        private void addFishLadyRecipes()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            parts.Call("AddPartRecipe",
             ItemType("Buddylure"), 1,
             false,
             1,
             ItemType("FishSteakPart"), 100,
              "Unusacies Battle Rods",
              null,
              new List<String>()
             {
                    "Weapon/By Weapon Type/" +"Summoning"+"/"+"Fishing",
                    "Weapon/By Damage Type/"+"Fishing"+"/"+"Summoning",
                    "By Shop/Fish Lady/"+"Weapon/By Weapon Type/" +"Summoning"+"/"+"Fishing",
                    "By Shop/Fish Lady/"+"Weapon/By Damage Type/"+"Fishing"+"/"+"Summoning"
             });

            parts.Call("AddPartRecipe",
             ItemType("BaitDisperser"), 1,
             false,
             1,
             ItemType("FishSteakPart"), 80,
              "Unusacies Battle Rods",
              null,
              new List<String>()
             {
                    "Accessory/Fishing",
                    "Fishing/Accessory",
                    "By Shop/Fish Lady/"+"Accessory/Fishing",
                    "By Shop/Fish Lady/"+"Fishing/Accessory"
             });
            parts.Call("AddPartRecipe",
             ItemType("FishSlicer"), 1,
             false,
             1,
             ItemType("FishSteakPart"), 50,
              "Unusacies Battle Rods",
              null,
              new List<String>()
             {
                    "Accessory/Fishing",
                    "Fishing/Accessory",
                    "By Shop/Fish Lady/"+"Accessory/Fishing",
                    "By Shop/Fish Lady/"+"Fishing/Accessory"
             });
            parts.Call("AddPartRecipe",
               ItemType("SmartBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 500,
                "Unusacies Battle Rods",
                (Func<bool>)(()=>NPC.downedMoonlord),
                new List<String>()
               {
                    "Accessory/Fishing",
                    "Fishing/Accessory",
                    "By Shop/Fish Lady/"+"Accessory/Fishing",
                    "By Shop/Fish Lady/"+"Fishing/Accessory"
               });
            parts.Call("AddPartRecipe",
               ItemType("VacuumWire"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 20,
                "Unusacies Battle Rods",
                null,
                new List<String>()
               {
                    "Accessory/Fishing",
                    "Fishing/Accessory",
                    "By Shop/Fish Lady/"+"Accessory/Fishing",
                    "By Shop/Fish Lady/"+"Fishing/Accessory"
               });

            parts.Call("AddPartRecipe",
               ItemType("ApprenticeBaitBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 1,
                "Unusacies Battle Rods",
                null,
                new List<String>()
               {
                    "Fishing/Bait",
                    "By Shop/Fish Lady/"+"Fishing/Bait"
               });
            parts.Call("AddPartRecipe",
               ItemType("JourneymanBaitBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 5,
                "Unusacies Battle Rods",
                null,
                new List<String>()
               {
                    "Fishing/Bait",
                    "By Shop/Fish Lady/"+"Fishing/Bait"
               });
            parts.Call("AddPartRecipe",
               ItemType("MasterBaitBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 10,
                "Unusacies Battle Rods",
                null,
                new List<String>()
               {
                    "Fishing/Bait",
                    "By Shop/Fish Lady/"+"Fishing/Bait"
               });

            parts.Call("AddPartRecipe",
               ItemType("SnowyBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 1,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => !Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("ExplosiveBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 3,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => !Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("MolotovBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 3,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => !Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("DynamiteBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 8,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => !Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("SnowyBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 1,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("ExplosiveBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 3,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("MolotovBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 3,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("DynamiteBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 8,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("ScytheBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 6,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("ShadowBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 8,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode && NPC.downedGoblins),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("SandnadoBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 12,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode && NPC.downedMechBossAny),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("NuclearBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 12,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode && NPC.downedPlantBoss),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("BetsyBobbers"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 6,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => Main.hardMode && DD2Event.DownedInvasionT3),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("ScytheBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 6,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => NPC.downedMoonlord),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });
            parts.Call("AddPartRecipe",
               ItemType("ShadowBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 8,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => NPC.downedMoonlord && NPC.downedGoblins),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("SandnadoBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 12,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => NPC.downedMoonlord),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("NuclearBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 12,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => NPC.downedMoonlord),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

            parts.Call("AddPartRecipe",
               ItemType("BetsyBobbersBox"), 1,
               false,
               1,
               ItemType("FishSteakPart"), 6,
                "Unusacies Battle Rods",
                 (Func<bool>)(() => NPC.downedMoonlord && DD2Event.DownedInvasionT3),
                new List<String>()
               {
                    "Fishing/Ammo/Discardable Bobbers",
                    "By Shop/Fish Lady/"+"Fishing/Ammo/Discardable Bobbers"
               });

        }

        private void addCoolerDropRecipes()
        {
            Mod parts = ModLoader.GetMod("ARareItemSwapJPANs");
            if (parts == null)
                return;

            parts.Call("AddPartRecipe",
              ItemID.Hook, 5,
              true,
              1,
              ItemType("CoolerPart"), 1,
               "Unusacies Battle Rods",
                null,
               new List<String>()
              {
                    "Material",
                    "By Boss/Cooler/"+"Material"
              });

            parts.Call("AddPartRecipe",
              ModContent.ItemType<CoolerBossBag>(), 1,
              false,
              1,
              ItemType("CoolerPart"), 200,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                     "Boss Bag",
                     "By Boss/Cooler/"+ "Boss Bag"
              });
            parts.Call("AddPartRecipe",
              ModContent.ItemType<MasterBaiterCertificate>(), 1,
              false,
              1,
              ItemType("CoolerPart"), 100,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                     "Material",
                     "By Boss/Cooler/"+"Material"
              });

            parts.Call("AddPartRecipe",
              ModContent.ItemType<CoolerBattlerod>(), 1,
              false,
              1,
              ItemType("CoolerPart"), 15,
               "Unusacies Battle Rods",
               null,
               new List<String>()
              {
                     "Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod",
                     "By Boss/Cooler/Weapon/By Weapon Type/" +"Battlerod"+"/"+"Fishing",
                    "By Boss/Cooler/Weapon/By Damage Type/"+"Fishing"+"/"+"Battlerod"
              });

            parts.Call("AddPartRecipe",
                ModContent.ItemType<Melonbrand>(), 1,
                false,
                1,
                ItemType("CoolerPart"), 25,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "Weapon/By Weapon Type/" +"Sword"+"/"+"Melee",
                    "Weapon/By Damage Type/"+"Melee"+"/"+"Sword",
                     "By Boss/Cooler/Weapon/By Weapon Type/"+"Sword"+"/"+"Melee",
                    "By Boss/Cooler/Weapon/By Damage Type/"+"Melee"+"/"+"Sword"
                });

            parts.Call("AddPartRecipe",
                ModContent.ItemType<MagicSoda>(), 1,
                false,
                1,
                ItemType("CoolerPart"), 25,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "Weapon/By Weapon Type/" +"Book"+"/"+"Magic",
                    "Weapon/By Damage Type/"+"Magic"+"/"+"Book",
                     "By Boss/Cooler/Weapon/By Weapon Type/"+"Book"+"/"+"Magic",
                    "By Boss/Cooler/Weapon/By Damage Type/"+"Magic"+"/"+"Book",
                });

            parts.Call("AddPartRecipe",
                ModContent.ItemType<IceCreamer>(), 1,
                false,
                1,
                ItemType("CoolerPart"), 25,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "Weapon/By Weapon Type/" +"Gun"+"/"+"Ranged",
                    "Weapon/By Damage Type/"+"Ranged"+"/"+"Gun",
                     "By Boss/Cooler/Weapon/By Weapon Type/"+"Gun"+"/"+"Ranged",
                    "By Boss/Cooler/Weapon/By Damage Type/"+"Ranged"+"/"+"Gun",
                });

            parts.Call("AddPartRecipe",
                ModContent.ItemType<BeerPack>(), 1,
                false,
                1,
                ItemType("CoolerPart"), 25,
                 "Unusacies Battle Rods",
                 null,
                 new List<String>()
                {
                     "Weapon/By Weapon Type/" +"Other"+"/"+"Throwing",
                    "Weapon/By Damage Type/"+"Throwing"+"/"+"Other",
                     "By Boss/Cooler/Weapon/By Weapon Type/"+"Other"+"/"+"Throwing",
                    "By Boss/Cooler/Weapon/By Damage Type/"+"Throwing"+"/"+"Other",
                });
        }
    }
}
