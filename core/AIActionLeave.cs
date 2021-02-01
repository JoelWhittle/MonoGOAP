using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionLeave : AIAction
    {
 
        public AIActionLeave(AIAgent agent) : base(agent)
        {
            PostConditions.Add("left", true);

            StateMachine.AddState(new AIActionStateEatFood(agent), "ACTION");
            StateMachine.ChangeState("ACTION");

        }



        public override void Update(float dt, AIAgent agent)
        {


            base.Update(dt, agent);
        }
        public override void OnTaskComplete()
        {
            mAgent.WorldState["left"] = true;

          

            base.OnTaskComplete();

            mAgent.flagForRemoval = true;
        }

        public override bool CheckProceduralPreCondition()
        {

            TargetObject = MapGenerator.Map[0, 0];
            return true;
        }

    }
}


