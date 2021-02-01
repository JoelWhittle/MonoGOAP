using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoalBladder : AIGoal
    {
        public AIGoalBladder(AIAgent agent) : base(agent)
        {
            BasePriority = 3f;
            PreConditions.Add("bladder", true);
            PostConditions.Add("bladder", false);

        }

        public override float GetScore()
        {
            return BasePriority + (mAgent.BladderLevel * 5);
        }
       
    }

  
}
