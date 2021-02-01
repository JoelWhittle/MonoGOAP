using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class FSM
    {
        public State CurrentState;
        public Dictionary<string, State> States = new Dictionary<string, State>();
        public Game1 Context;

        public bool ChangeState(string newStateName)
        {
            if(States.ContainsKey(newStateName))
            {
                if (CurrentState!= null)
                {
                    CurrentState.OnStateExit();
                }
                CurrentState = States[newStateName];
                CurrentState.OnStateEnter();
                return true;

            }

            return false ;
        }

        public void AddState(State state, string name)
        {
            state.Context = this;
            States.Add(name, state);
        }

        public void Update(float dt)
        {
            if (CurrentState != null)
            {
                CurrentState.OnStateUpdate(dt);
            }
        }

    }
}
