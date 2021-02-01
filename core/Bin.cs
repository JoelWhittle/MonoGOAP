using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class Bin : SpriteObject , IBin
    {
        public int CurRubbish = 0;
        public int MaxRubbish = 10;
        public Bin()
        {

       
            MyTexture = TextureManager.TextureDictionary["bin"];

           
        }
        public bool IsFull()
        {
            if (CurRubbish >= MaxRubbish)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
