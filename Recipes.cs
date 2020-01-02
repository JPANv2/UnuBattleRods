using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRods.Configs;

namespace UnuBattleRods
{
    public class EasyRecipe : ModRecipe
    {
        public EasyRecipe(Mod mod) : base(mod)
        {
        }

        public override bool RecipeAvailable()
        {
            return !ModContent.GetInstance<UnuServerConfig>().harderLureRecipes && base.RecipeAvailable();
        }
    }

    public class HardRecipe : ModRecipe
    {
        public HardRecipe(Mod mod) : base(mod)
        {
        }

        public override bool RecipeAvailable()
        {
            return ModContent.GetInstance<UnuServerConfig>().harderLureRecipes && base.RecipeAvailable();
        }
    }
}
