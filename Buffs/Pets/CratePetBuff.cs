using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Buffs.Pets
{
    public class CratePetBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Pet Crate");
            Description.SetDefault("A crate that helps you find crates!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<FishPlayer>(mod).maxCrate = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("CratePetProjectile")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("CratePetProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
