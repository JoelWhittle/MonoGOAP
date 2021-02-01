using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class Litter : SpriteObject , ILitter
    {
        public Litter()
        {
            MyTexture = TextureManager.TextureDictionary["litter"];
        }
    }
}
