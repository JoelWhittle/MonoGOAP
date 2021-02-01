using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionRandomWander : AIAction
    {

        public AIActionRandomWander(AIAgent agent) : base(agent)
        {
            PreConditions.Add("idle", true);
            PostConditions.Add("idle", false);

            StateMachine.AddState(new AIActionStateEatFood(agent), "ACTION");
            StateMachine.ChangeState("ACTION");

        }



        public override void Update(float dt, AIAgent agent)
        {

            base.Update(dt, agent);
        }
        public override void OnTaskComplete()

        {
            mAgent.WorldState["idle"] = true;

            base.OnTaskComplete();
        }


        public override bool CheckProceduralPreCondition()
        {



            float d = 5000;

            TargetObject = null;


            bool found = false;
            while (!found)
            {
                int x = Game1.mRandom.Next(0, 39);
                int y = Game1.mRandom.Next(0, 39);
                TargetObject = MapGenerator.Map[x, y];

                if(MapGenerator.Map[x,y].Walkable)
                {
                    ActionLocation = TargetObject.Position;
                    TargetObject = ((GameObject)MapGenerator.Map[x,y]);

                    ((AIActionStateMovingTo)StateMachine.States["MOVE"]).Target = MapGenerator.Map[x, y];
                    found = true;


                }
            }

           

            return  true;
        }

    }
}

