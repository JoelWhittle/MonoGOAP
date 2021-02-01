using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class PhysicsObject :SpriteObject,IPhysicsObject
    {
        public Body mBody;

        public PhysicsObject(Vector2 position)
        {
            Position = position;
        

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Position = mBody.Position * Game1.mPhysicsManager.unitToPixel;
            Position = mBody.Position;// * Game1.mPhysicsManager.pixelToUnit;
            Vector2 position = Position; //* Game1.mPhysicsManager.pixelToUnit;

            float posX = (int)position.X - (int)(Size.X / 2);

            posX -= Camera.GetPosition().X;

            float posY = (int)position.Y - (int)(Size.Y / 2);

            posY += Camera.GetPosition().Y;
            Rotation = mBody.Rotation;


            Vector2 scale = new Vector2(Size.X / (float)GetTexture().Width, Size.Y / (float)GetTexture().Height);
            spriteBatch.Draw(GetTexture(), new Vector2(posX, posY), null, Color.White, mBody.Rotation, new Vector2(GetTexture().Width / 2.0f, (GetTexture().Height / 2.0f)), scale, SpriteEffects.None, 0);

            //   base.Draw(spriteBatch);

        }



    }
}
