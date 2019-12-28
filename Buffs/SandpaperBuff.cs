using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs
{
    public class SandpaperBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Smooth Bobber");
            Description.SetDefault("Bobber will remain when falling from a defeated enemy.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>().fallOnFloorPercentage = 100;
        }
    }
}
