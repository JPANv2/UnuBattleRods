using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

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

    }
}
