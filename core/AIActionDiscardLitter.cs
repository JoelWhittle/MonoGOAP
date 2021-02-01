using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionDiscardLitter : AIAction
    {
        public bool InBin = true;
        public Bin MyBin;
        public AIActionDiscardLitter(AIAgent agent) : base(agent)
        {
            PreConditions.Add("carryingLitter", true);
            PostConditions.Add("carryingLitter", false);

            StateMachine.AddState(new AIActionStateDiscardLitter(agent), "ACTION");
            StateMachine.ChangeState("ACTION");

        }



        public override void Update(float dt, AIAgent agent)
        {


            base.Update(dt, agent);
        }
        public override void OnTaskComplete()
        {
            mAgent.WorldState["carryingLitter"] = false;

            if (InBin && MyBin != null)
            {
                MyBin.CurRubbish++;
            }
            else
            {
                var p = 2;
            }
                   
            base.OnTaskComplete();
        }

        public override List<GameObject> FilterTargets(List<GameObject> q)
        {
            return q.Where(a => a is IBin).ToList();
        }


        public override bool CheckProceduralPreCondition()
        {



            float d = 5000;

            TargetObject = null;




            foreach (var potentialTarget in Game1.Bins)
            {
                if (Vector2.Distance(mAgent.Position, ((GameObject)potentialTarget).Position) < d)
                {
                    d = Vector2.Distance(mAgent.Position, ((GameObject)potentialTarget).Position);
                    TargetObject = ((GameObject)potentialTarget);
                    ActionLocation = TargetObject.Position;

                    if (!((Bin)potentialTarget).IsFull())
                    {

                        ((AIActionStateDiscardLitter)StateMachine.States["ACTION"]).inBin = true;

                   
                        MyBin = ((Bin)potentialTarget);
                        InBin = true;
                    }
                }
            }
            if(TargetObject == null)
            {
                TargetObject = mAgent.CurrentTile;
                ActionLocation = TargetObject.Position;
                MyBin = null;
                InBin = false;
                ((AIActionStateDiscardLitter)StateMachine.States["ACTION"]).inBin = false;

            }

            return true;
        }

    }
}


