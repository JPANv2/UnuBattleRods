
namespace UnuBattleRods.Tiles.Crates
{
    class AnkhCrate: CrateTile
    {
        public override void SetDefaults()
        {
            itemID = mod.ItemType("AnkhCrate");
            name = CreateMapEntryName();
            name.SetDefault("Ankh Crate");
            base.SetDefaults();
        }
    }
}
