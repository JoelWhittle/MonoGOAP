using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIGoal
    {

  

        public Dictionary<string, object> PreConditions = new Dictionary<string, object>();
        public Dictionary<string, object> PostConditions = new Dictionary<string, object>();
        public float BasePriority = 1;
        public AIAgent mAgent;
        public virtual float GetScore()
        {
            return BasePriority;
        }
        public AIGoal(AIAgent agent)
        {
            mAgent = agent;
        }

       
    }
}
