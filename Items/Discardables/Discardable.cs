using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRods.Projectiles.Discardables;

namespace UnuBattleRods.Items.Discardables
{
    public abstract class Discardable : ModItem
    {
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        protected int discardableProjectileID;
        protected float damageMod = 1.0f;
        protected int damageAdd = 0;
        protected int projectileDuration = 30;

        public virtual void onDiscard(Player p, int damage, Entity target)
        {
            int projID = Projectile.NewProjectile(target.position, Vector2.Zero, discardableProjectileID, ((int)Math.Round(damage * damageMod)) + damageAdd,0,p.whoAmI);

            if(projID >= 0 && projID < Main.projectile.Length)
            {
                Main.projectile[projID].damage = ((int)Math.Round(damage * damageMod)) + damageAdd;
                //UnuBattleRods.debugChat("Made projectile have " + Main.projectile[projID].damage + " damage");
                Main.projectile[projID].Center = target.Center;
                Main.projectile[projID].timeLeft = projectileDuration;
                if (Main.projectile[projID].modProjectile as DiscardableProjectile != null)
                {
                    (Main.projectile[projID].modProjectile as DiscardableProjectile).npcIndex = target is Player ? (target.whoAmI + Main.npc.Length) : target is NPC ? target.whoAmI : -1;
                }
                Main.projectile[projID].netUpdate = true;
            }
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            item.ammo = ModContent.ItemType<ExplosiveBobbers>();
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;
        }
    }
}
