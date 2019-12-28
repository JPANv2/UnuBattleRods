using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs
{
    public class MaximumEscalationBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Maximum Bob Escalation");
            Description.SetDefault("8% damage increase per second while attatched to the same enemy, up to 100 times normal damage.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>().escalation = true;
            player.GetModPlayer<FishPlayer>().escalationBonus = 0.08f;
            player.GetModPlayer<FishPlayer>().escalationMax += 99f;
        }
    }
}
