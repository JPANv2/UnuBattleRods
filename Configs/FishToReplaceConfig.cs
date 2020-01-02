using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using UnuBattleRods.Items.Baits.DebuffBaits;

namespace UnuBattleRods.Configs
{
    public class FishToReplaceConfig : ModConfig
    {
        public FishToReplaceConfig()
        {
            fishToReplace = new List<ItemDefinition>();
            fishToReplace.Add(new ItemDefinition(ItemID.WoodenCrate));
            fishToReplace.Add(new ItemDefinition(ItemID.Bass));
            fishToReplace.Add(new ItemDefinition(ItemID.Salmon));
            fishToReplace.Add(new ItemDefinition(ItemID.Trout));
            fishToReplace.Add(new ItemDefinition(ItemID.Tuna));
            fishToReplace.Add(new ItemDefinition(ItemID.Shrimp));
            fishToReplace.Add(new ItemDefinition(ItemID.AtlanticCod));
            fishToReplace.Add(new ItemDefinition(ItemID.NeonTetra));
            fishToReplace.Add(new ItemDefinition(ItemID.RedSnapper));
            fishToReplace.Add(new ItemDefinition(ItemID.Honeyfin));
            fishToReplace.Add(new ItemDefinition(ItemID.Obsidifish));
        }
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Replace all fish")]
        [Tooltip("This mod will be able to replace any fished items with its own. If set, the below list is ignored.")]
        [DefaultValue(false)]
        public bool replaceAllFish = false;

        [Label("Fish to Replace")]
        [Tooltip("List of fished items that can be replaced with this mod's items. Changing it does not require reload.")]
        public List<ItemDefinition> fishToReplace = new List<ItemDefinition>();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            return false;
        }
    }
}
