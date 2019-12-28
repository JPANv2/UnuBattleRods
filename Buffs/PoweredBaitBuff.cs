using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRods.NPCs;
using UnuBattleRods.Projectiles.Bobbers;
using Terraria.Localization;

namespace UnuBattleRods.Buffs
{
    public class PoweredBaitBuff : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Powered Bait");
            Description.SetDefault("You have the following Buffs active:");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            List<Entity> hookedEntities = getHookedEntities(pl);
            if(pl.baitTimer < player.buffTime[buffIndex])
            {
                player.buffTime[buffIndex] = pl.baitTimer;
            }
            if(hookedEntities.Count == 0)
            {
                player.buffTime[buffIndex] = pl.baitTimer;
                return;
            }
            if (player.buffTime[buffIndex] > 0)
            {
                for (int i = 0; i < pl.baitBuff.Length; i++)
                {
                    if (pl.baitBuff[i] > 0)
                    {
                        pl.updateBuffByID(ref pl.baitBuff[i], player.buffTime[buffIndex], buffIndex);
                    }
                }
                pl.baitTimer = player.buffTime[buffIndex];
            }
        }

        public List<Entity> getHookedEntities(FishPlayer player)
        {
            List<Entity> ans = new List<Entity>();
            List<Bobber> bobbers = player.getOwnedAttatchedBobbers();
            foreach (Bobber b in bobbers) { 
                ans.Add(b.getStuckEntity());
            }
            return ans;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "";
            FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            if (pl.hasAnyBaitBuffs())
            {
                tip += Description.GetTranslation(Language.ActiveCulture)+"\n";
                for(int i = 0; i < pl.baitBuff.Length; i++)
                {
                    if(pl.baitBuff[i] > 0)
                    {
                        tip += Lang.GetBuffName(pl.baitBuff[i])+"\n";
                    }
                }
                tip += "\n";
            }
            if (pl.hasAnyBaitDebuffs())
            {
                tip += "You are inflicting the following Debuffs:" + "\n";
                for (int i = 0; i < pl.baitDebuff.Length; i++)
                {
                    if (pl.baitDebuff[i] > 0)
                    {
                        tip += Lang.GetBuffName(pl.baitDebuff[i]) + "\n";
                    }
                }
            }
        }
    }

    public class PoweredBaitDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Powered Bait");
            Description.SetDefault("You have the following Debuffs active:");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }

        public override bool ReApply(Player p, int time, int buffIndex)
        {
            return true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "";
            //List<int> debuffsPresent = getBaitDebuffsFromPlayers(Bobber.getOwnersOfBobbersAttatchedTo(Main.player[Main.myPlayer]));
            FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            if (pl.debuffsPresent.Count > 0)
            {
                tip += Description.GetTranslation(Language.ActiveCulture) + "\n";
                foreach (int id in pl.debuffsPresent)
                {
                    tip += Lang.GetBuffName(id) + "\n";
                }
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            List<int> recurringDebuffs = getBaitDebuffsFromPlayers(Bobber.getOwnersOfBobbersAttatchedTo(npc));
            FishGlobalNPC fnpc = npc.GetGlobalNPC<FishGlobalNPC>();
            if (recurringDebuffs.Count > 0)
            {
                npc.buffTime[buffIndex] = 120;
                addAllBuffsToList(npc,fnpc, recurringDebuffs);
            }
            int id = 0;
            for (int i = 0; i < fnpc.debuffsPresent.Count; i++)
            {
                id = fnpc.debuffsPresent[i];
                fnpc.UpdateDebuffsByID(npc, ref id, 120, buffIndex);
            }
            if (npc.buffTime[buffIndex] <= 2)
            {
                fnpc.debuffsPresent.Clear();
            }
        }

        public void addAllBuffsToList(NPC npc, FishGlobalNPC fnpc, List<int> recurringDebuffs)
        {
            int size = fnpc.debuffsPresent.Count;
            for(int i = 0; i < recurringDebuffs.Count; i++)
            {
                if (!fnpc.debuffsPresent.Contains(recurringDebuffs[i]))
                {
                    fnpc.debuffsPresent.Add(recurringDebuffs[i]);
                }
            }
            if (size < fnpc.debuffsPresent.Count)
            {
                fnpc.debuffsPresent.Sort();
                fnpc.updateCurrentInflictedBaitDebuffs(npc);
            }
        }

        public void addAllBuffsToList(FishPlayer fnpc, List<int> recurringDebuffs)
        {
            int size = fnpc.debuffsPresent.Count;
            for (int i = 0; i < recurringDebuffs.Count; i++)
            {
                if (!fnpc.debuffsPresent.Contains(recurringDebuffs[i]))
                {
                    fnpc.debuffsPresent.Add(recurringDebuffs[i]);
                }
            }
            if (size < fnpc.debuffsPresent.Count)
            {
                fnpc.debuffsPresent.Sort();
                fnpc.updateCurrentInflictedBaitDebuffs();
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            List<int> recurringDebuffs = getBaitDebuffsFromPlayers(Bobber.getOwnersOfBobbersAttatchedTo(player));

            FishPlayer pl2 = player.GetModPlayer<FishPlayer>();
            if (recurringDebuffs.Count > 0)
            {
                player.buffTime[buffIndex] = 120;
                addAllBuffsToList(pl2, recurringDebuffs);
            }
           
            int id = 0;
            for (int i = 0; i < pl2.debuffsPresent.Count; i++)
            {
                id = pl2.debuffsPresent[i];
                pl2.updateBuffByID(ref id, 120, buffIndex);
            }

            if (player.buffTime[buffIndex] <= 2)
            {
                pl2.debuffsPresent.Clear();
            }
        }

        public List<int> getBaitDebuffsFromPlayers(List<Player> list)
        {
           // ErrorLogger.Log(list.Count + " Players found.");
            List<int> ans = new List<int>();

            foreach(Player p in list)
            {
                if(p.active && !p.dead && p.FindBuffIndex(ModContent.BuffType<PoweredBaitBuff>())>= 0)
                {
                    FishPlayer pl = p.GetModPlayer<FishPlayer>();
                    for(int i = 0; i <pl.baitDebuff.Length; i++)
                    {
                        if(pl.baitDebuff[i] > 0 && !ans.Contains(pl.baitDebuff[i]))
                        {
             //               ErrorLogger.Log(pl.baitDebuff[i] + ": Added debuff.");
                            ans.Add(pl.baitDebuff[i]);
                        }
                    }
                }
            }
            ans.Sort();
            return ans;
        }
    }
}

