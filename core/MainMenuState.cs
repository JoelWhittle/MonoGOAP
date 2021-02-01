using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class MainMenuState : State
    {
        public override void OnStateEnter()
        {
            Context.Context.ScreenClearColor = Color.White;

        }
        public override void OnStateExit()
        {
        }
        public override void OnStateUpdate(float dt)
        {
            if (InputManager.GetKeyReleased(Keys.Enter))
            {
                Context.ChangeState("MAINGAME");
            }  
        }
    }
}
