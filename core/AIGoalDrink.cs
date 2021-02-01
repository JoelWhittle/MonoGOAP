using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoalDrink : AIGoal
    {
        public AIGoalDrink(AIAgent agent) : base(agent)
        {
            BasePriority = 1.5f;
            PreConditions.Add("thirsty", true);
            PostConditions.Add("thirsty", false);

        }

        public override float GetScore()
        {
            return BasePriority + (mAgent.ThirstLevel * 5);
        }
  

    }
}

