using Terraria;

namespace UnuBattleRods.Tiles.Crates
{
    class MimicCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("MimicCrate");
            name = CreateMapEntryName();
            name.SetDefault("Odd Crate");
            base.SetDefaults();
        }

        public override bool NewRightClick(int i, int j)
        {

            FishPlayer f = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();

            if (f == null || f.mimicToSpawn)
                return false;

            if (Main.tile[i, j].frameX > 0)
                i--;
            if (Main.tile[i, j].frameY > 0)
                j--;

            Main.tile[i, j].ClearTile();
            Main.tile[i+1, j].ClearTile();
            Main.tile[i, j+1].ClearTile();
            Main.tile[i+1, j+1].ClearTile();
            NetMessage.SendTileSquare(Main.myPlayer, i, j, 2);

            f.mimicX = i * 16 + 16;
            f.mimicY = j * 16 + 32;
            f.mimicToSpawn = true;
            return true;
        }

        public override void HitWire(int i, int j)
        {
            NewRightClick(i, j);
        }
    }
}
