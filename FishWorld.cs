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

namespace UnuBattleRods
{
    public class FishWorld : ModWorld
    {
        public static int marbleTiles = 0;
        public static int graniteTiles = 0;

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
                    if (temp.Count > 1) {
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
                    temp = l.FindAll(k => ( k.item[0].type == ItemID.ShadowKey));
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

                        for(int i = 0; i < totalReplace; i++)
                        {
                            int idx = Main.rand.Next(ans.Count);
                            ans[idx].item[0].SetDefaults(mod.ItemType("DungeonBattlerod"));
                            ans.RemoveAt(idx);
                        }
                    }

                }));
            }
        }
        }
}
