using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoalDiscardLitter : AIGoal
    {


        public AIGoalDiscardLitter(AIAgent agent) : base(agent)
        {
            BasePriority = 99999;

            PreConditions.Add("carryingLitter", true);
            PostConditions.Add("carryingLitter", false);


        }

        public override float GetScore()
        {
            return BasePriority ;
        }

    }
}
