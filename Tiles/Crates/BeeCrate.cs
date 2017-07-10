
namespace UnuBattleRods.Tiles.Crates
{
    class BeeCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("BeeCrate");
            name = CreateMapEntryName();
            name.SetDefault("Bee Crate");
            base.SetDefaults();
        }
    }
}
