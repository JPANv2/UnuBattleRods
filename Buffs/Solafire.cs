using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRods.NPCs;

namespace UnuBattleRods.Buffs
{
    public class Solarfire : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Solar Fire");
            Description.SetDefault("From being too close to the sun.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            FishGlobalNPC gnpc = npc.GetGlobalNPC<FishGlobalNPC>();
            gnpc.solarFire = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.solarFire = true;
        }

    }
}
