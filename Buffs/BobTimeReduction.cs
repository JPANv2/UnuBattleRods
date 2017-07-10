using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs
{
    public class BobTimeReduction : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bob Time Reduction");
            Description.SetDefault("25% less time between bobs");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>(mod).bobberSpeed += 0.25f;
        }
    }
}
