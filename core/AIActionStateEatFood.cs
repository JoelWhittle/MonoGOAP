using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionStateEatFood : AIActionState
    {

        public float Timer = 1;
        public float CurTimer = 1;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }


        public AIActionStateEatFood(AIAgent owner) : base (owner)
        {

        }
        public override void OnStateUpdate(float dt)
        {
            CurTimer -= dt;
            if(CurTimer < 0)
            {
                FlagTaskComplete = true;
                CurTimer = Timer;
            }
            

            base.OnStateUpdate(dt);
        }

    }
}
