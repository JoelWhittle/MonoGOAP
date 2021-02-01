using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionBladder : AIAction
    {

        public AIActionBladder(AIAgent agent) : base(agent)
        {
            PreConditions.Add("bladder", true);
            PostConditions.Add("bladder", false);

            StateMachine.AddState(new AIActionStateEatFood(agent), "ACTION");
            StateMachine.ChangeState("ACTION");

        }

     

        public override void Update(float dt, AIAgent agent)
        {


            base.Update(dt, agent);
        }
        public override void OnTaskComplete()
        {
            mAgent.BladderLevel = 0;
            base.OnTaskComplete();
        }

        public override List<GameObject> FilterTargets(List<GameObject> q)
        {
            return q.Where(a => a is IBladderable).ToList();
        }


        public override bool CheckProceduralPreCondition()
        {



            float d = 5000;

            TargetObject = null;




            foreach (var potentialTarget in Game1.Toilets)
            {
                if (Vector2.Distance(mAgent.Position, ((GameObject)potentialTarget).Position) < d)
                {
                    d = Vector2.Distance(mAgent.Position, ((GameObject)potentialTarget).Position);
                    TargetObject = ((GameObject)potentialTarget);
                    ActionLocation = TargetObject.Position;
                }
            }

            return TargetObject != null;
        }

    }
}


