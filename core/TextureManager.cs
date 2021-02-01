using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine
{
    public static class  TextureManager
    {
        public static Dictionary<string, Texture2D> TextureDictionary = new Dictionary<string, Texture2D>();

        public static void LoadTexture(string name, Texture2D texture)
        {
            TextureDictionary.Add(name, texture);
        }
    }
}
