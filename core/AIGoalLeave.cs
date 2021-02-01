using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoalLeave : AIGoal
    {
        public AIGoalLeave(AIAgent agent) : base(agent)
        {
            BasePriority = 0.5f;

            PostConditions.Add("left", true);
        }

        public override float GetScore()
        {

            if (mAgent.BladderLevel > 150 || mAgent.HungerLevel > 150 || mAgent.ThirstLevel > 150 || Game1.mMainGameState.TimeOfDay > 200)
            {
                return 9999;
            }
            else
                return BasePriority;
            

        }

    }


}
