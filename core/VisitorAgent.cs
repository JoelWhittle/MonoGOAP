using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class VisitorAgent : AIAgent
    {
        public VisitorAgent(Vector2 position, float health) : base(position, health)
        {
            CurHealth = health;
            MaxHealth = health;


            WorldState.Add("hungry", true);
            WorldState.Add("thirsty", false);
            WorldState.Add("bladder", false);
            WorldState.Add("injured", false);
            WorldState.Add("seeEnemy", false);
            WorldState.Add("killEnemy", false);
            WorldState.Add("carryingLitter", false);
            WorldState.Add("idle", true);
            WorldState.Add("left", false);

            Goals.Add(new AIGoalEat(this));
            Goals.Add(new AIGoalDrink(this));
            Goals.Add(new AIGoalBladder(this));
            Goals.Add(new AIGoalDiscardLitter(this));
            Goals.Add(new AIGoalIdle(this));
            Goals.Add(new AIGoalLeave(this));


            Actions.Add(new AIActionEatFood(this));
            Actions.Add(new AIActionDrink(this));

            Actions.Add(new AIActionBladder(this));
            Actions.Add(new AIActionDiscardLitter(this));

            Actions.Add(new AIActionRandomWander(this));
            Actions.Add(new AIActionLeave(this));

        }
    }
}
