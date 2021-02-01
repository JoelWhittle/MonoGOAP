using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class State
    {
        public FSM Context;
        public virtual void OnStateEnter()
        {

        }
        public virtual void OnStateExit()
        {

        }
        public virtual void OnStateUpdate(float dt)
        {
            
        }
    }
}
