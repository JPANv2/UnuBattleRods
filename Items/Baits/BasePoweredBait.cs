using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;

namespace UnuBattleRods.Items.Baits
{
    public abstract class BasePoweredBait : ModItem
    {
        protected int buffID = -1;
        protected int debuffID = -1;
        protected int buffTime = 0;

        
        public virtual void addBuffToPlayer(Player player, int slot)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            int pbtype = mod.BuffType<PoweredBaitBuff>();

            for (int j = 0; j < 22; j++)
            {
                if (player.buffType[j] == pbtype)
                {
                    player.buffTime[j] = buffTime;
                    pl.baitTimer = buffTime;
                    pl.addBaitBuffs(buffTime, slot, buffID);
                    pl.addBaitDebuffs(buffTime, slot, debuffID);
                    return;
                }
            }
            for (int i = 0; i< 22; i++)
            {
                if (player.buffType[i] <= 0)
                {
                    player.buffType[i] = pbtype;
                    player.buffTime[i] = buffTime;
                    pl.baitTimer = buffTime;
                    pl.addBaitBuffs(buffTime, slot, buffID);
                    pl.addBaitDebuffs(buffTime, slot, debuffID);
                    return;
                }        
            }
        }
    }

    public abstract class ApprenticePoweredBait : BasePoweredBait
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.CloneDefaults(ItemID.ApprenticeBait);
            item.bait = 15;
            item.rare = 2;
            item.maxStack = 999;
            buffTime = 3600;
        }
    }

    public abstract class PoweredBait : BasePoweredBait
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.CloneDefaults(ItemID.JourneymanBait);
            item.bait = 25;
            item.rare = 3;
            item.maxStack = 999;
            buffTime = 9000;
        }
    }

    public abstract class MasterPoweredBait : BasePoweredBait
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.CloneDefaults(ItemID.MasterBait);
            item.bait = 50;
            item.rare = 5;
            item.maxStack = 999;
            buffTime = 18000;
        }
    }


    public class BaitRecipe : ModRecipe
    {
        public BaitRecipe(Mod mod):base(mod)
        {

        }

        public override bool RecipeAvailable()
        {
            Player p = Main.player[Main.myPlayer];
            FishPlayer pl = p.GetModPlayer<FishPlayer>(this.mod);
            return pl.MasterBaiter;
        }
    }
}
