using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoalIdle : AIGoal
    {


        public AIGoalIdle(AIAgent agent) : base(agent)
        {
            BasePriority = 1;


            PreConditions.Add("idle", true);
            PostConditions.Add("idle", false);


        }

        public override float GetScore()
        {
            return BasePriority ;
        }

    }
}
