using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public interface IRenderable
    {
        Texture2D GetTexture();
        
         void Draw(SpriteBatch spriteBatch);
       
        
    }
}
