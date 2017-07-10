using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnuBattleRods.Items.Crates
{
    public class MimicCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Odd Crate");
            Tooltip.SetDefault("Something tells me I should not try and open this...");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = 0;
            item.createTile = mod.TileType("MimicCrate");

        }

        public override void RightClick(Player player)
        {
            FishPlayer f = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>(mod);

            if (f == null || f.mimicToSpawn)
                return;

            f.mimicX = (int)player.Center.X;
            f.mimicY = (int)player.Center.Y;
            f.mimicToSpawn = true;
        }
    }
}
