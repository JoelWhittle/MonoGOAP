using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class JanitorAgent : AIAgent
    {
        public JanitorAgent(Vector2 position, float health) : base(position, health)
        {
            CurHealth = health;
            MaxHealth = health;


            WorldState.Add("seeLitter", true);
            WorldState.Add("seeFullBin", true);

            WorldState.Add("idle", true);
          

            Goals.Add(new AIGoalPickupLitter(this));
            Goals.Add(new AIGoalIdle(this));



            Actions.Add(new AIActionPickupLitter(this));
            Actions.Add(new AIActionRandomWander(this));


        }

        public override void UpdateWorldState(float dt)
        {
            WorldState["seeLitter"] = false;
            foreach(var l in Game1.Litter)
            {
                if(Vector2.Distance(Position, ((GameObject)l).CurrentTile.Position) < 10000)
                    {
                    WorldState["seeLitter"] = true;

                    }
            }
        }
    }
}
