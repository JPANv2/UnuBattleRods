using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using UnuBattleRods.Items.Baits.DebuffBaits;

namespace UnuBattleRods.Configs
{
    public class UnuPlayerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Start with Wooden Battlerod")]
        [DefaultValue(true)]
        public bool startWithRod;

        [Label("Start with Poison Bait")]
        [DefaultValue(true)]
        public bool startWithBait;
    }

    public class UnuServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        
        [Label("Harder Lure Recipes")]
        [Tooltip("If the multi-lure Recipes should require more and harder to get materials, or should be made with just cobweb and the previous lure.")]
        [DefaultValue(true)]
        public bool harderLureRecipes = true;
        [Label("Allow this mod's fished items")]
        [Tooltip("If the crates added by this mod should appear at all when fishing.")]
        [DefaultValue(true)]
        public bool allowFishedItems = true;

        [Label("Bobbers always break")]
        [Tooltip("If bobbers should never fall from the target when detatching.")]
        [DefaultValue(false)]
        public bool dontFallOnFloor = false;

        [Label("Explosives damage everyone")]
        [Tooltip("Should Grenade and Dynamite Bobbers damage everyone (including the player that triggered it), or not.")]
        [DefaultValue(true)]
        public bool explosivesDamageEveryone = true;

        [Label("Items that should not be sold")]
        [Tooltip("Items that the Sell Gate and Killing Gate will not replace with coins.")]
        public List<ItemDefinition> noSellItems = new List<ItemDefinition>();

        [Label("Items that should always be sold")]
        [Tooltip("Items that the Sell Gate and Killing Gate will replace with coins even if their max stack is not 1.\n\nWarning:Anything added here will take precedence to any other replacing, so if all fish from the replace option are here, no crates will appear.\nSame with Fish Steaks.")]
        public List<ItemDefinition> forceSellItems = new List<ItemDefinition>();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            return false;
        }

    }
}
