using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Rods.HardMode
{
	public class FishronBattlerod : BattleRod
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishron Battle Rod");
            Tooltip.SetDefault("King of the Sea. Releases bubbles.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.item.shootSpeed = 20.0f;
            base.item.shoot = base.mod.ProjectileType("FishronBobber");
            base.item.damage = 200;
            base.item.crit = 20;
            base.item.rare = 8;
            base.item.fishingPole = 70;
            base.item.value = Item.sellPrice(0,10,0,0);

            noOfBobs = 3;
        }
    }
}
