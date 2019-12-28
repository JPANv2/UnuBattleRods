using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRods.Buffs;
using UnuBattleRods.Items.Baits;
using UnuBattleRods.Projectiles.Bobbers;

namespace UnuBattleRods.Items.Rods
{
	public abstract class BattleRod : ModItem
	{
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public int noOfBobs = 1;
        public int noOfBaits = 1;
		public override void SetDefaults()
		{
            base.item.CloneDefaults(2291);
            base.item.ranged = true;
            base.item.noMelee = true;
            base.item.shoot = base.mod.ProjectileType("Bobber");
            base.item.shootSpeed = 9f;
            base.item.rare = 1;
            base.item.fishingPole = 5;
            base.item.value = Item.buyPrice(0,0,0,50);
            
        }


        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= ((player.GetModPlayer<FishPlayer>().bobberDamage)/ player.rangedDamage);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            int lures = noOfBobs + p.multilineFishing;

            if (lures <= 1)
                lures = 1;

            Vector2 speedVector = new Vector2(speedX, speedY);
            float trueSpeed = speedVector.Length() * p.bobberShootSpeed;

            if (p.aimBobber && player.whoAmI == Main.myPlayer)
            {
                speedVector = Main.MouseWorld - position;
                speedVector.Normalize();
                speedX = speedVector.X;
                speedY = speedVector.Y;
            }else
            {
                speedVector.Normalize();
                speedX = speedVector.X;
                speedY = speedVector.Y;
            }
            speedX *= trueSpeed;
            speedY *= trueSpeed;
            speedVector.X *= trueSpeed;
            speedVector.Y *= trueSpeed;
            int trueDamage = 0;
            if(damage > 0)
            {
                trueDamage = (damage / lures < 1 ? 1 : damage / lures);
            }
            if (lures > 1)
            {
                if (p.aimBobber)
                {
                    for (int i = 0; i < lures - 1; i++)
                    {
                        Projectile.NewProjectile(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5), speedVector.X, speedVector.Y, type, trueDamage, knockBack, player.whoAmI);
                    }
                }
                else
                {
                    for (int i = 1; i < lures; i++)
                    {
                        Projectile.NewProjectile(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5), speedX + Main.rand.Next(lures) - lures / 2, speedY + Main.rand.Next(lures) - lures / 2, type, trueDamage, knockBack, player.whoAmI);
                    }                    
                }
                if (trueDamage * lures < damage)
                {
                    damage = damage - (trueDamage * (lures - 1));
                }
            }
            int baitTotal = getNoOfBaits(p);
            int totalBuffs = getNoOfBuffs(p.player);
            int usedBaitCount = p.getNumberOfBaitDebuffs() + p.getNumberOfBaitBuffs();
            int useableBaits = p.getNumberOfUseableBaits();
            //if rod can use powered bait && less than ten seconds in bait timer && the total number of bait the rod can use is bigger than the current applied bait count
            if (baitTotal > 0 && 
                (p.baitTimer < 600 || (usedBaitCount != useableBaits && baitTotal != usedBaitCount))
                && totalBuffs < player.buffType.Length)
            {
                int slot = 0;
                for (int i = 54; i < 58; i++)
                {
                    BasePoweredBait b = player.inventory[i].modItem as BasePoweredBait;
                    if (b != null)
                    {
                        b.addBuffToPlayer(player, slot);
                        player.inventory[i].stack--;
                        slot++;
                        if (slot >= baitTotal)
                        {
                            for (; slot < p.baitBuff.Length; slot++)
                            {
                                p.addBaitBuffs(0, slot, -1);
                                p.addBaitDebuffs(0, slot, -1);
                            }
                            if (Main.netMode != 0)
                                p.updateBaits(player.whoAmI);
                           
                            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
                        }
                    }
                }
                if(slot > 0)
                {
                    for (; slot < p.baitBuff.Length; slot++)
                    {
                        p.addBaitBuffs(0, slot, -1);
                        p.addBaitDebuffs(0, slot, -1);
                    }
                    if (Main.netMode != 0)
                        p.updateBaits(player.whoAmI);
                }
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public int getNoOfBuffs(Player player)
        {
            int ans = 0;
            int baitbufftype = ModContent.GetInstance<PoweredBaitBuff>().Type;
            for(int i = 0; i<player.buffType.Length; i++)
            {
                if(player.buffType[i] != 0 && player.buffType[i] != baitbufftype)
                {
                    ans++;
                }
            }
            return ans;
        }

        public virtual int getNoOfBaits(FishPlayer p)
        {
            return noOfBaits;
        }

        private Vector2[] getSpeedsForBobbers(Vector2 speedVector, int lures)
        {
            Vector2[] ans = new Vector2[lures];
            float speedForce = speedVector.Length();
            double angle = Math.Atan2(speedVector.Y/ speedForce, speedVector.X/speedForce);
            if (Math.Asin(speedVector.Y / speedForce) < 0) {
                angle = -angle;
            }
            double startAngle = -Math.PI / 4 + angle;
            double endAngle = Math.PI / 4 + angle;
            double deltaAngle = (Math.PI / 2) / lures;
            ans[lures / 2] = speedVector;
            for(int i = lures/2 - 1; i >= 0; i--)
            {
                if(i >= 0)
                {
                    ans[i] = new Vector2((float)(Math.Cos(startAngle + deltaAngle * i)*speedForce),
                        (float)(Math.Sin(startAngle + deltaAngle * i)*speedForce));
                }
                if ((lures-i) < lures)
                {
                    ans[lures-i] = new Vector2((float)(Math.Cos(endAngle - deltaAngle * i) * speedForce),
                        (float)(Math.Sin(startAngle - deltaAngle * i) * speedForce));
                }
            }
            return ans;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            int lures = noOfBobs + pl.multilineFishing;

            if (lures <= 1)
                lures = 1;

            int trueDamage = 0;
            if (getDamage() > 0)
            {
                trueDamage = (getDamage() / lures < 1 ? 1 : getDamage() / lures);
            }

            int idx = tooltips.FindIndex(x => x.Name == "Damage");
            if (idx >= 0)
            {
                tooltips.RemoveAt(idx);
                tooltips.Insert(idx, new TooltipLine(mod, "Damage", getDamage() + " Fishing damage"+ ((lures > 1 ? (" (" + trueDamage + "x " + lures + " bobbers)") : ""))));
            }else
            {
                idx = tooltips.FindIndex(x => x.Name == "FishingPower");
                tooltips.Insert(idx, new TooltipLine(mod, "Damage", getDamage() + " Fishing damage" + ((lures > 1 ? (" (" + trueDamage + "x " + lures + " bobbers)") : ""))));
            }
                
            idx = tooltips.FindIndex(x => x.Name == "FishingPower");
            Projectile p = new Projectile();
            p.SetDefaults(item.shoot);
            if (p.modProjectile != null && p.modProjectile is Bobber)
            {
                Bobber b = (Bobber)p.modProjectile;
                b.projectile.owner = Main.myPlayer;
                if(b.reelTime() == 0)
                {
                    tooltips.Insert(idx + 1, new TooltipLine(mod, "ReelSpeed", "Cannot reel"));
                }
                else
                {
                    tooltips.Insert(idx + 1, new TooltipLine(mod, "ReelSpeed", ((((float)(b.reelTime())) / 60f) * b.speedIncrease).ToString("0.0") + "pix/s^2 reel speed"));
                }
                
                if (b.bobTime() == 0)
                {
                    tooltips.Insert(idx + 1, new TooltipLine(mod, "BobSpeed", "No bobs"));
                }
                else
                {
                    tooltips.Insert(idx + 1, new TooltipLine(mod, "BobSpeed", (((float)(b.bobTime())) / 60f).ToString("0.00") + "s bobs"));
                }
                tooltips.Insert(idx + 1, new TooltipLine(mod, "TensileStrength", b.TensileStrength().ToString("0.0") + "pix/s Tensile Strength"));
                if(noOfBobs > 1)
                {
                    tooltips.Insert(idx + 1, new TooltipLine(mod, "BobNo", "Launches "+noOfBobs+ " bobbers at once."));
                }
            }
        }

        private int getDamage()
        {
            float dmg = item.damage;
            Player p = Main.player[Main.myPlayer];

            FishPlayer f = p.GetModPlayer<FishPlayer>();
            dmg *= f.bobberDamage;

            return (int)Math.Round(dmg);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }


        public override ModItem Clone()
        {
            return (ModItem)this.MemberwiseClone();
        }
    }
}
