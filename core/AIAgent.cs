using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIAgent : Agent
    {
        public Dictionary<string, object> WorldState = new Dictionary<string, object>();

        public List<AIAction> JobQueue = new List<AIAction>();
        public List<AIGoal> Goals = new List<AIGoal>();
        public List<AIAction> Actions = new List<AIAction>();

        public float HungerLevel = 0;
        public float BladderLevel = 0;
        public float ThirstLevel = 0;

        public int JobsComplete = 0;

        public float MaxReationTimer = 1f;
        public float CurReactionTimer = 0f;
        public List<GameObject> q = new List<GameObject>();

        public AIAgent TargetAgent;

        public Tile Home;
        public AIAgent(Vector2 position, float health) : base(position, health)
        {
           

        }

        public override void Update(float dt)
        {

            Position = base.Position;
            if (Dead)
            {
                return;
            }

            HungerLevel += dt;
            BladderLevel += dt;
            ThirstLevel += dt;

            CurReactionTimer -= dt;

            if (JobQueue.Count() > 0)
            {
                if (JobQueue[0].TaskComplete)
                {
                   ((AIActionState)JobQueue[0].StateMachine.States["ACTION"]).FlagTaskComplete = false;
                  //  Actions[0].CheckProceduralPreCondition();
                   // Actions[1].CheckProceduralPreCondition();
                    JobQueue[0].TaskComplete = false;
                    JobQueue.RemoveAt(0);

                    JobsComplete++;
                }
                else
                {
                    JobQueue[0].Update(dt, this);
                }

            }
            else
            {


                UpdateWorldState(dt);
                OrderGoals();
                  JobQueue.AddRange(MakePlan());

              //  Actions[0].CheckProceduralPreCondition();
              //  Actions[1].CheckProceduralPreCondition();

              //  JobQueue.Add(Actions[0]);
               // JobQueue.Add(Actions[1]);
               // JobQueue.Add(Actions[0]);
               // JobQueue.Add(Actions[1]);


                base.Update(dt);
            }
        }

        public virtual void UpdateWorldState(float dt)
        {

            if (HungerLevel > 60)
            {
                WorldState["hungry"] = true;

            }
            else
            {
                WorldState["hungry"] = false;
            }
            if (ThirstLevel > 60)
            {
                WorldState["thirsty"] = true;

            }
            else
            {
                WorldState["thirsty"] = false;
            }
            if (BladderLevel > 60)
            {
                WorldState["bladder"] = true;

            }
            else
            {
                WorldState["bladder"] = false;
            }
            if (CurHealth < MaxHealth)
            {
                WorldState["injured"] = true;

            }
            else
            {
                WorldState["injured"] = false;
            }





        }

        public void OrderGoals()
        {
            Goals = Goals.OrderBy(o => o.GetScore()).ToList();
            Goals.Reverse();
        }

        public List<AIAction> MakePlan()
        {

            List<AIAction> plan = new List<AIAction>();
            List<AIGoal> validGoal = new List<AIGoal>();


            foreach (var goall in Goals)
            {
                bool valid = true;
                foreach (var keys in goall.PreConditions)
                {
                    object Output;
                    if (WorldState.TryGetValue(keys.Key, out Output))
                    {
                        if (Convert.ToBoolean(Output) != Convert.ToBoolean(keys.Value))
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    validGoal.Add(goall);
                }

            }

            bool gotPlan = false;
            
            foreach (var corGoal in validGoal)
            {
                if (!gotPlan)
                {
                    var p = TryAndPlan(corGoal);
                    if(p.Count() > 0)
                    {
                        gotPlan = true;
                        plan = p;
                    }
                }
                
            }



            return plan;



            //Random r = new Random();
            //int x = r.Next(0, 40 * 50);
            //int y = r.Next(0, 40 * 50);

            //Actions[0].ActionLocation = TargetAgent.Position;

            //Actions[0].TargetAgent = TargetAgent;
            //plan.Add(Actions[0]);
        }


        public List<AIAction> TryAndPlan(AIGoal corGoal)
        {
            List<AIAction> plan = new List<AIAction>();

            List<AIAction> validActions = new List<AIAction>();
            bool valid = true;
            foreach (var action in Actions)
            {
                action.mAgent = this;
                valid = true;
                foreach (var keys in action.PostConditions)
                {
                    object Output;
                    if (corGoal.PostConditions.TryGetValue(keys.Key, out Output))
                    {
                        if (Convert.ToBoolean(Output) != Convert.ToBoolean(keys.Value))
                        {
                            valid = false;
                        }

                    }
                    else
                    {
                        valid = false;
                    }
                }



                if (valid)
                {


                    if (action.CheckProceduralPreCondition())
                    {
                        validActions.Add(action);
                    }
                }

            }


            validActions.Sort((x, y) => x.GetCost().CompareTo(y.GetCost()));

            if (validActions.Count() == 0)
            {
                return plan;
            }

            List<AIAction> path = new List<AIAction>();



            //clear tiles prev stuff
            foreach (var t in Actions)
            {
                t.g = 0;
            }

            var lastCheckedNode = validActions[0];
            List<AIAction> openSet = new List<AIAction>();
            // openSet starts with beginning node only
            openSet.Add(lastCheckedNode);
            List<AIAction> closedSet = new List<AIAction>();

            //This function returns a measure of aesthetic preference for
            //use when ordering the openSet. It is used to prioritise
            //between equal standard heuristic scores. It can therefore
            //be anything you like without affecting the ability to find
            //a minimum cost path.



            //Run one finding step.
            //returns 0 if search ongoing
            //returns 1 if goal reached
            //returns -1 if no solution
            var end = corGoal;
            bool searching = true;
            while (searching == true)
            {
                if (openSet.Count() > 0)
                {
                    // Best next option
                    var winner = 0;
                    for (var i = 1; i < openSet.Count(); i++)
                    {
                        if (openSet[i].GetF(end) < openSet[winner].GetF(end))
                        {
                            winner = i;
                        }
                        //if we have a tie according to the standard heuristic
                        if (openSet[i].GetF(end) == openSet[winner].GetF(end))
                        {
                            //Prefer to explore options with longer known paths (closer to goal)
                            if (openSet[i].g > openSet[winner].g)
                            {
                                winner = i;
                            }


                        }
                    }
                    var current = openSet[winner];
                    lastCheckedNode = current;
                    // Did I finish?
                    if (current.IsCompatibleWithCurWorldState(WorldState))
                    {

                        path.Add(current);

                        AIAction cur = current;
                        while (cur.Parent != null)
                        {
                            path.Add(cur.Parent);
                            cur = cur.Parent;
                        }
                        path.Reverse();
                        return path;
                    }

                    // Best option moves from openSet to closedSet
                    openSet.Remove(current);
                    closedSet.Add(current);

                    // Check all the neighbors
                    var neighbors = current.GetActionNeighbours(this);

                    neighbors = neighbors.Where(o => o.CheckProceduralPreCondition()).ToList();

                    for (var i = 0; i < neighbors.Count(); i++)
                    {
                        var neighbor = neighbors[i];

                        // Valid next spot?
                        if (!closedSet.Contains(neighbor))
                        {
                            // Is this a better path than before?
                            var tempG = current.g + 0 + current.GetCost(); ;

                            // Is this a better path than before?
                            if (!openSet.Contains(neighbor))
                            {

                                openSet.Add(neighbor);
                            }
                            else if (tempG >= neighbor.g)
                            {
                                // No, it's not a better path
                                continue;
                            }

                            neighbor.g = tempG;

                            neighbor.Parent = current;
                        }

                    }

                }
                else
                {
                    searching = false;

                    return path;
                }
                // Uh oh, no solution
            }
            return path;


        }
    }

}




