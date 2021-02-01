using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEngine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine
{
    public class SpriteObject : GameObject , IRenderable
    {
        public Texture2D MyTexture;
        public Vector2 Size = new Vector2(50,50);
        public float Rotation;
        public bool ShouldDraw = true;
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (ShouldDraw)
            {
                float posX = (int)Position.X - (int)(Size.X / 2);

                posX -= Camera.GetPosition().X;

                float posY = (int)Position.Y - (int)(Size.Y / 2);

                posY += Camera.GetPosition().Y;


                Vector2 scale = new Vector2(Size.X / (float)GetTexture().Width, Size.Y / (float)GetTexture().Height);

                //spriteBatch.Draw(GetTexture(), new Rectangle((int)posX , (int)posY  , (int)Size.X , (int)Size.Y ), Color.White);
                spriteBatch.Draw(GetTexture(), new Vector2(posX, posY), new Microsoft.Xna.Framework.Rectangle(0, 0, (int)GetTexture().Width, (int)GetTexture().Height), Color.White, Rotation, new Vector2(0, 0), scale, SpriteEffects.None, 0);

            }
        }
        public Texture2D GetTexture()
        {
            return MyTexture;
        }
    }
}
