using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRods.Items.Parts
{
    public class CoolerPart : ModItem
    {
        public override bool CloneNewInstances => true;
        /*full path to the texture*/
        public string worldDisplay = "UnuBattleRods/Items/Parts/CoolerPart_World";


        public override void SetDefaults()
        {
            base.item.width = 16;
            base.item.height = 16;
            base.item.maxStack = 999;
            base.item.value = Item.sellPrice(0, 0, 0, 1);
            base.item.rare = 2;
            base.item.useStyle = 4;
            base.item.useTime = 5;
            base.item.useAnimation = 5;

        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Main.itemFrameCounter[whoAmI]++;
            if (Main.itemFrameCounter[whoAmI] > 5)
            {
                Main.itemFrameCounter[whoAmI] = 0;
                Main.itemFrame[whoAmI]++;
                if (Main.itemFrame[whoAmI] > 9)
                {
                    Main.itemFrame[whoAmI] = 0;
                }
            }
            Texture2D texture = ModContent.GetTexture(worldDisplay);
            Rectangle rectangle = Utils.Frame(texture, 1, 10, 0, Main.itemFrame[whoAmI]);
            rectangle.Height -= 2;
            Vector2 value = new Vector2((float)(base.item.width / 2 - rectangle.Width / 2), (float)(base.item.height - rectangle.Height));
            spriteBatch.Draw(texture, base.item.position - Main.screenPosition + Utils.Size(rectangle) / 2f + value, new Rectangle?(rectangle), alphaColor, rotation, Utils.Size(rectangle) / 2f, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
