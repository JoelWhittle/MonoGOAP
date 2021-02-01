using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class SplashState : State
    {
        public double CurTimer = 0;
        public double MaxTimer = 2;
        public override void OnStateEnter()
        {
            Context.Context.ScreenClearColor = Color.HotPink;

        }
        public override void OnStateExit()
        {
            CurTimer = 0;
        }
        public override void OnStateUpdate(float dt)
        {
            CurTimer +=dt;
            if(CurTimer > MaxTimer)
            {
                Context.ChangeState("MAINMENU");
            }
        }
    }
}
