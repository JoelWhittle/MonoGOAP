using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public interface IPhysics
    {
        void InitialiseWorld();
        void Tick(float dt);
        void AddObjectToWorld(Body body);
        
    }
}
