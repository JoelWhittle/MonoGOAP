using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonoEngine.core
{
    public class AIActionStateMovingTo : AIActionState

    {

        public double CurTimer = 0;
        public double MaxTimer = 2;
        public AIAgent Owner;
        public Vector2 TargetLocation = new Vector2(0, 0);
        public float MaxDistance = 30;







        public Tile Target;
        public Agent AgentTarget;
        public List<Tile> Path = new List<Tile>();
        public Agent MyAgent;
        bool CheckedOnce = false;

        public float MaxPathReset = 1f;
        public float CurPathReset = 0;


        public  AIActionStateMovingTo(AIAgent owner) : base (owner)
            {

            }
        public override void OnStateEnter()
        {
           SetTarget(Target, MyAgent);


          //  Target = MapGenerator.GetTileAtPosition(Owner.JobQueue[0].ActionLocation);
        }
        public override void OnStateExit()
        {

        }
        public override void OnStateUpdate(float dt)
        {
            if (Vector2.Distance(Owner.Position,TargetLocation) < MaxDistance)
            {
                Context.ChangeState("ACTION");
            }
            else
            {
                Update(dt, Owner);


            }
        }



        public  void Update(float dt, AIAgent agent)
        {
            MyAgent = agent;

            CurPathReset -= dt;
            //if (CurPathReset < 0)
            //{
            //    CurPathReset = MaxPathReset;

            //    if (AgentTarget != null)
            //    {
            //        SetTarget(AgentTarget, agent);

            //    }

            //    else
            //    {

            //        if (Target != null)
            //        {
            //            SetTarget(Target, agent);
            //        }
            //    }
            //}
            if (Path.Count() > 0)
            {
                if (Vector2.Distance(agent.Position, Path[0].Position) < 5)
                {
                    agent.CurrentTile = Path[0];
                    Path.RemoveAt(0);
                }
            }
            if (Path.Count() == 0 && CheckedOnce)
            {

            }
            Vector2 totalDir = new Vector2(0, 0);
            Vector2 pathTargetDir = new Vector2(0, 0);
            float pathSteerWeight = 2;
            float unitAvoidanceWeight = 1.5f;
            //if (Path.Count() > 0)
            //{
            //    pathTargetDir = Path[0].Position - agent.Position;

            //    pathTargetDir.Normalize();
            //    pathTargetDir *= agent.Speed * dt;

            //    agent.Position += pathTargetDir;
            //}
            if (Path.Count() > 0)
            {
                pathTargetDir = Path[0].Position - agent.Position;

                if (AgentTarget != null)
                {
                    pathTargetDir = AgentTarget.Position - agent.Position;
                }

                pathTargetDir.Normalize();
                pathTargetDir *= pathSteerWeight;

                float closestDistance = 9999999;
                Agent closestAgent;
                closestAgent = MyAgent;

                Rectangle rect = new Rectangle();
                rect.Position = agent.Position;
                rect.Size = new Vector2(50, 50);
                var q = Game1.quadtree.CircleQuery(agent.Position, 50);

                //var t = new List<GameObject>();

                //var l = Game1.GameObjects.Where(o => o is Agent);
                //        foreach(var f in l)
                //{
                //    if(Vector2.Distance(f.Position, this.MyAgent.Position) < 300)
                //    {
                //        t.Add(f);
                //    }
                //}

                foreach (var go in q.Where(o => o is Agent))
                {
                    if (go != this.MyAgent)
                    {
                        if (Vector2.Distance(go.Position, this.MyAgent.Position) < closestDistance)
                        {
                            if (((Agent)go) != agent.TargetAgent)
                            {
                                closestAgent = ((Agent)go);
                                closestDistance = Vector2.Distance(go.Position, this.MyAgent.Position);
                            }
                        }
                    }

                }

                if (closestAgent != null && closestAgent != MyAgent)
                {
                    Vector2 agentAvoidDir = closestAgent.Position - MyAgent.Position;
                    agentAvoidDir.Normalize();
                    agentAvoidDir *= -unitAvoidanceWeight;
                    totalDir += agentAvoidDir;
                }
                totalDir += pathTargetDir;
                totalDir.Normalize();

                totalDir *= agent.Speed * dt;


                agent.Position += totalDir;
            }


        }

        public void SetTarget(Agent target, Agent agent)
        {
            SetTarget(target.CurrentTile, agent);
            AgentTarget = target;
        }
        public void SetTarget(Tile target, Agent agent)
        {
            CheckedOnce = true;
            Target = target;
            MyAgent = agent;
            bool genPath = true;
            bool targetMoveCheck = true;
            bool stillWalkableCheck = true;
            Path.Clear();

            if (Path.Count() > 0)
            {
                genPath = false;
                if (AgentTarget != null)
                {
                    if (AgentTarget.CurrentTile != Target)
                    {
                        targetMoveCheck = false;
                    }
                }
                foreach (var t in Path)
                {
                    if (!t.Walkable)
                    {
                        stillWalkableCheck = false;
                    }
                }

                if (!stillWalkableCheck && !targetMoveCheck)
                {
                    genPath = true;
                }
            }
            if (genPath)
            {
                var newPath = MapGenerator.AStarPathFinder(agent.CurrentTile, target, false);

                Path.Clear();

                foreach (var t in newPath)
                {
                    if (t != agent.CurrentTile)
                    {
                        Path.Add(t);
                    }
                }
            }

        }

    }
}
