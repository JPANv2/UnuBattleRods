using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Items.Currency;
using Microsoft.Xna.Framework;

namespace UnuBattleRods.Projectiles.Bobbers
{
    class GlobalBobberProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int amount = 0;
        public override void PostAI(Projectile projectile)
        {
            if (projectile.aiStyle == 61)
            {
                int provisoryAmmount = Main.player[projectile.owner].GetModPlayer<FishPlayer>().fishedAmount;
                
                if (projectile.localAI[1] == ItemID.CopperCoin)
                {
                    if (provisoryAmmount > 0)
                    {
                       
                        amount = provisoryAmmount;
                        Main.player[projectile.owner].GetModPlayer<FishPlayer>().fishedAmount = 0;
                    }
                } else if (projectile.localAI[1] == ModContent.ItemType<FishSteaks>())
                {
                        if (provisoryAmmount > 0)
                        {
                        
                        amount = provisoryAmmount;
                            Main.player[projectile.owner].GetModPlayer<FishPlayer>().fishedAmount = 0;
                        }
                }
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.aiStyle == 61 && projectile.ai[1] > 0f && projectile.ai[1] < (float)ItemLoader.ItemCount)
            {
                 if (projectile.ai[1] == ItemID.CopperCoin) {
                    int platinum = amount / 1000000;
                    int gold = (amount / 10000) % 100;
                    int silver = (amount / 100) % 100;
                    int copper = (amount % 100);
                    if(copper != 0)
                        Main.player[projectile.owner].QuickSpawnItem(ItemID.CopperCoin, copper);
                    if (silver != 0)
                        Main.player[projectile.owner].QuickSpawnItem(ItemID.SilverCoin, silver);
                    if (gold != 0)
                        Main.player[projectile.owner].QuickSpawnItem(ItemID.GoldCoin, gold);
                    if (platinum != 0)
                        Main.player[projectile.owner].QuickSpawnItem(ItemID.PlatinumCoin, platinum);

                }
                else if(projectile.ai[1] == ModContent.ItemType<FishSteaks>())
                {
                    Main.player[projectile.owner].QuickSpawnItem((int)projectile.ai[1], amount - 1);
                }
            }
        }
    }
}
