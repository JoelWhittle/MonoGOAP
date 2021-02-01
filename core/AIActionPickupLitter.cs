using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionPickupLitter : AIAction
    {

        public AIActionPickupLitter(AIAgent agent) : base(agent)
        {
            PreConditions.Add("seeLitter", true);
            PostConditions.Add("seeLitter", false);

            StateMachine.AddState(new AIActionStateEatFood(agent), "ACTION");
            StateMachine.ChangeState("ACTION");

        }



        public override void Update(float dt, AIAgent agent)
        {
            
            base.Update(dt, agent);
        }
        public override void OnTaskComplete()

        {
            mAgent.WorldState["seeLitter"] = false;

            List<ILitter> tmp = new List<ILitter>();
            foreach(var l in Game1.Litter)
            {

                if (((GameObject)l).CurrentTile == TargetObject.CurrentTile)
                {
                    ((GameObject)l).flagForRemoval = true;
                    tmp.Add(l);

                }
            }

            foreach(var l in tmp)
            {
                Game1.Litter.Remove(((ILitter)l));

            }
            TargetObject.flagForRemoval = true;
            Game1.Litter.Remove(((ILitter)TargetObject));

            base.OnTaskComplete();
        }


        public override bool CheckProceduralPreCondition()
        {



            float d = 5000;

            TargetObject = null;




            foreach (var potentialTarget in Game1.Litter)
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

