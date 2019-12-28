
namespace UnuBattleRods.Tiles.Crates
{
    class AlienCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("AlienCrate");
            name = CreateMapEntryName();
            name.SetDefault("Alien Crate");
            base.SetDefaults();
        }
    }
}
