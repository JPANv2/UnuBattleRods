using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.NormalMode
{
	public class CoolerBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cooler Battle Rod");
            Tooltip.SetDefault("Allows 3 different powered baits at once.\nDoes (almost) no damage.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("CoolerBobber");
            base.item.damage = 1;
            base.item.crit = 0;
            base.item.rare = 8;
            base.item.fishingPole = 30;
            base.item.value = Item.sellPrice(0,1,0,0);

            noOfBobs = 2;
            noOfBaits = 3;
        }

    }
}
