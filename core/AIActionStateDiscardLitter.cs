using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class AIActionStateDiscardLitter : AIActionState
    {

        public float Timer = 1;
        public float CurTimer = 1;
        public bool inBin = false;
        public override void OnStateEnter()
        {
            
            base.OnStateEnter();
        }


        public AIActionStateDiscardLitter(AIAgent owner) : base(owner)
        {

        }
        public override void OnStateUpdate(float dt)
        {
            CurTimer -= dt;
            if (CurTimer < 0)
            {
                FlagTaskComplete = true;
                CurTimer = Timer;

                if (inBin == false)
                {
                    Litter l = new Litter();
                    l.Position = Owner.Position;
                    l.CurrentTile = Owner.CurrentTile;
                    Game1.AddGameObject(l);
                }
                else
                {

                }
            }


            base.OnStateUpdate(dt);
        }

    }
}
