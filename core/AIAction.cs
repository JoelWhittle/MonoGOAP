using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoEngine.core
{
    public class AIAction
    {
        public bool TaskComplete = false;

        public Dictionary<string, object> PreConditions = new Dictionary<string, object>();
        public Dictionary<string, object> PostConditions = new Dictionary<string, object>();

        public AIAgent mAgent;

        public FSM StateMachine = new FSM();

        public Microsoft.Xna.Framework.Vector2 ActionLocation = new Microsoft.Xna.Framework.Vector2(0,0);
        public float g = 0;
        public AIAgent TargetAgent;
        public GameObject TargetObject;
        public float MaxDistanceFromLocation = 80;


        public AIAction Parent = null;
        public AIAction(AIAgent agent)
        {
            mAgent = agent;

            AIActionStateMovingTo state = new AIActionStateMovingTo(agent);
            state.Owner = agent;
            StateMachine.AddState(state, "MOVE");
        }

        public virtual float GetCost()
        {



            float x = Math.Abs(mAgent.CurrentTile.GridX - TargetObject.CurrentTile.GridX);
            float y = Math.Abs(mAgent.CurrentTile.GridY - TargetObject.CurrentTile.GridY);


            return x + y ;
        }

        public virtual void Update(float dt, AIAgent agent)
        {

            if (Vector2.Distance(ActionLocation, mAgent.CurrentTile.Position) > MaxDistanceFromLocation && StateMachine.CurrentState == StateMachine.States["ACTION"])
            {
              
                    ((AIActionStateMovingTo)StateMachine.States["MOVE"]).Target = TargetObject.CurrentTile;
               ((AIActionStateMovingTo)StateMachine.States["MOVE"]).SetTarget(TargetObject.CurrentTile, agent);
                StateMachine.ChangeState("MOVE");
            }
            else
            {
                if (Vector2.Distance(ActionLocation, mAgent.CurrentTile.Position) < MaxDistanceFromLocation + 10) // && StateMachine.CurrentState == StateMachine.States["MOVE"])
                {
                    StateMachine.ChangeState("ACTION");
                }
            }


            if (((AIActionState)StateMachine.States["ACTION"]).FlagTaskComplete)
            {
                OnTaskComplete();
            }
            StateMachine.Update(dt);
        }

        public virtual void OnTaskComplete()
        {
            mAgent.Position = mAgent.CurrentTile.Position;

            TaskComplete = true;
        }

        public virtual void OnEnterAction()
        {

        }

        public virtual List<GameObject> FilterTargets(List<GameObject> q)
        {
            return null;
        }

        public virtual bool CheckProceduralPreCondition()
        {
            ((AIActionState)StateMachine.States["ACTION"]).FlagTaskComplete = false;



            return false;
        }

        public virtual float Heuristic(AIGoal goal)
        {
            //float r = 0;

            //foreach (var keys in goal.PostConditions)
            //{
            //    object Output;
            //    if (PostConditions.TryGetValue(keys.Key, out Output))
            //    {
            //        r++;
            //        if (Convert.ToBoolean(Output) != Convert.ToBoolean(keys.Value))
            //        {

            //        }
            //        else
            //        {
            //            r--;
            //        }
            //    }
            //    else
            //    {
            //    }
            //}




            //return r;

            return 0;
        }

        public virtual float GetF(AIGoal goal)
        {
            return Heuristic(goal) + g;
        }

        public bool IsCompatibleWithCurWorldState(Dictionary<string,object> ws)
        {
            bool valid = true;

        
                foreach (var keys in ws)
                {
                    object Output;
                    if (PreConditions.TryGetValue(keys.Key, out Output))
                    {
                        if (Convert.ToBoolean(Output) != Convert.ToBoolean(keys.Value))
                        {
                            valid = false;
                        }

                    }
                    else
                    {
                       
                    }
                }
           return valid;
        }

        public List<AIAction> GetActionNeighbours(AIAgent agent)
        {
            List<AIAction>  n = new List<AIAction>();

            foreach(var action in agent.Actions)
            {
                if (action != this)
                {
                    bool valid = true;

                    foreach (var keys in PreConditions)
                    {
                        bool innerValid = true;
                        object Output;
                        if (PreConditions.TryGetValue(keys.Key, out Output))
                        {
                            if (Convert.ToBoolean(Output) != Convert.ToBoolean(keys.Value))
                            {
                                valid = false;
                                innerValid = false;
                            }
                            else
                            {
                                n.Add(action);
                            }


                        }
                        else
                        {

                        }
                    }

                }
            }


            return n;
        }
    }
}