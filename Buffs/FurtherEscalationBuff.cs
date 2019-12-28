using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs
{
    public class FurtherEscalationBuff : ModBuff
    {
        public override void SetDefaults()
        {
           DisplayName.SetDefault("Further Bob Escalation");
            Description.SetDefault("Bob Escalation can reach 300% Max Damage. Does not cause escalation damage.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>().escalation = true;
            player.GetModPlayer<FishPlayer>().escalationMax += 2.0f;
        }
    }
}
