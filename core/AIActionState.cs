using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionState : State

    {
        public bool FlagTaskComplete = false;
        public bool FlagTaskFailed = false;


        public AIAgent TargetAgent = null;

        public AIAgent Owner = null;


        public AIActionState(AIAgent owner)
        {
            Owner = owner;
        }
    }
}
