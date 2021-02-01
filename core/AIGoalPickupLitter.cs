using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{

   public class AIGoalPickupLitter : AIGoal
    {
        public AIGoalPickupLitter(AIAgent agent) : base(agent)
        {
            BasePriority = 1;
            PreConditions.Add("seeLitter", true);

            PostConditions.Add("seeLitter", false);
        }

        public override float GetScore()
        {

            return BasePriority;

        }

    }


}
