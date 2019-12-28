using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRods.NPCs;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace UnuBattleRods.Buffs
{
    public class EnemyFrozenDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Frozen");
            Description.SetDefault("Sticks an NPC in place.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffImmune[BuffID.Frostburn])
            {
                npc.buffTime[buffIndex] = 0;
                npc.buffType[buffIndex] = 0;
                return;
            }

            npc.velocity = new Microsoft.Xna.Framework.Vector2(0, 8);

            /*if (npc.buffTime[buffIndex] % 6 == 0)
            {
               npc.StrikeNPC(1, 1, 0);
            }*/

            if ((npc.buffTime[buffIndex] % 120) == 0) {
                int totalDust = Main.rand.Next(0, 4);
                for (int i = 0; i < totalDust; i++) 
                {
                    int dt = Dust.NewDust(Vector2.Add(npc.position, new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height))), 8, 8, DustID.t_Frozen, 0, -0.5f, 0, default(Color), Main.rand.NextFloat() * 2 + 0.5f);
                }
            }

        }

    }
}
