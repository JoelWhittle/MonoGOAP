using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public static class Camera
    {
        public static Vector2 Position = new Vector2(0, 0);
        public static Vector2 GetPosition()
        {
            return new Vector2((Position.X - Game1.graphics.GraphicsDevice.Viewport.Width / 2), Position.Y +( Game1.graphics.GraphicsDevice.Viewport.Height / 2));
        }
    }
}
