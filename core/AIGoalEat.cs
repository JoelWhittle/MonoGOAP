using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
   public class AIGoalEat : AIGoal
    {


        public AIGoalEat(AIAgent agent) : base(agent)
        {
            BasePriority = 1;

            PreConditions.Add("hungry", true);
            PostConditions.Add("hungry", false);


        }

        public override float GetScore()
        {
            return BasePriority + (mAgent.HungerLevel * 5);
        }

    }
}
