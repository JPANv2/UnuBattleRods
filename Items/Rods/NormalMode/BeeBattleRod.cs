using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class BeeBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Battle Rod");
            Tooltip.SetDefault("So many Bees!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 14f;
            base.item.shoot = base.mod.ProjectileType("BeeBobber");
            base.item.damage = 85;
            base.item.rare = 3;
            base.item.fishingPole = 28;
            base.item.value = Item.buyPrice(0,15,0,0);
        }

	}
}
