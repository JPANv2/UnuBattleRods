using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;

namespace UnuBattleRods
{
    public class FishWorld : ModWorld
    {
        public static int marbleTiles = 0;
        public static int graniteTiles = 0;

        public static bool downedCooler = false;

        public override void PostUpdate()
        {
            if (Main.netMode != 1 && Main.time % 60 == 0)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active && Main.player[i].GetModPlayer<FishPlayer>().wormSpawner && Main.rand.Next(30) == 1)
                    {
                        bool doneWorm = false;
                        if (Main.player[i].ZoneGlowshroom)
                        {
                            if (Main.rand.Next(16) == 0)
                            {
                                NPC.NewNPC((int)(Main.player[i].position.X), (int)(Main.player[i].position.Y), NPCID.TruffleWorm);
                                doneWorm = true;
                            }
                        }
                        if (!doneWorm)
                        {
                            NPC.NewNPC((int)(Main.player[i].position.X), (int)(Main.player[i].position.Y), Main.rand.Next(100) == 0 ? NPCID.GoldWorm : NPCID.Worm);
                        }
                    }
                }
            }
        }


        public override void TileCountsAvailable(int[] tileCounts)
        {
            marbleTiles = tileCounts[TileID.Marble];
            graniteTiles = tileCounts[TileID.Granite];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int finalIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (finalIndex != -1)
            {
                tasks.Insert(finalIndex, new PassLegacy("Insert in Dungeon Chest", delegate (GenerationProgress progress)
                {
                    progress.Message = "Inserting rod in Dungeon Chest";

                    List<Chest> l = new List<Chest>(Main.chest);
                    l = l.FindAll(k => (k != null && k.item != null));
                    List<Chest> ans = new List<Chest>();
                    List<Chest> temp = l.FindAll(k => (k.item[0].type == ItemID.MagicMissile));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }
                    temp = l.FindAll(k => (k.item[0].type == ItemID.BlueMoon));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }

                    temp = l.FindAll(k => (k.item[0].type == ItemID.Valor));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }
                    temp = l.FindAll(k => (k.item[0].type == ItemID.ShadowKey));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }
                    temp = l.FindAll(k => (k.item[0].type == ItemID.AquaScepter));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }
                    temp = l.FindAll(k => (k.item[0].type == ItemID.Handgun));
                    if (temp.Count > 1)
                    {
                        temp.RemoveAt(0);
                        ans.AddRange(temp);
                    }

                    if (ans.Count > 0)
                    {
                        int totalReplace = (ans.Count / 6) + 1;

                        for (int i = 0; i < totalReplace; i++)
                        {
                            int idx = Main.rand.Next(ans.Count);
                            ans[idx].item[0].SetDefaults(mod.ItemType("DungeonBattlerod"));
                            ans.RemoveAt(idx);
                        }
                    }

                }));
            }
        }
        public override TagCompound Save()
        {
            TagCompound ans = new TagCompound();
            if (downedCooler)
                ans["downedCooler"] = downedCooler;

            return ans;
        }

        public override void Load(TagCompound tag)
        {
            if (tag.ContainsKey("downedCooler"))
                downedCooler = true;
            return;
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write((byte)(downedCooler ? 1 : 0));
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            downedCooler = (reader.ReadByte() & 1) == 1;
        }
    }
}
