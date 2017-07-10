using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs
{
    public class BobEscalation : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bob Escalation");
            Description.SetDefault("2% damage increase per second while attatched to the same enemy, up to 100%.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>(mod).escalation = true;
            player.GetModPlayer<FishPlayer>(mod).escalationBonus += 0.02f;
        }
    }
}
