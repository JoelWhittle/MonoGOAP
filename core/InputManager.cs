using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public static  class InputManager
    {
        public static KeyboardState OldKeyState;
        public static KeyboardState CurrentKeyState;

        public static bool GetKeyReleased(Keys key)
        {
            return CurrentKeyState.IsKeyUp(key) && OldKeyState.IsKeyDown(key) ;
        }
       
    }
}
