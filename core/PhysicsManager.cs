using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class PhysicsManager : IPhysics
    {
        public World PhysicsWorld;
        Body body;
        public  float unitToPixel = 32f;
        public float pixelToUnit; 
        public void InitialiseWorld()
        {
            PhysicsWorld = new World(new Vector2(0, 9.8f));
            pixelToUnit = 1 / unitToPixel;
        }
        public void Tick(float dt)
        {
            PhysicsWorld.Step(dt);

        }
        public void AddObjectToWorld(Body body)
        {

        }

        public World GetWorld()
        {
            return PhysicsWorld;
        }

        public  void RemoveObjectFromWorld(Body mBody)
        {
            PhysicsWorld.RemoveBody(mBody);
        }
      public PhysicsObject GetObject(Body body)
        {
            List<PhysicsObject> physicsObjects = Game1.GameObjects.Where(o => o is IPhysicsObject).Cast<PhysicsObject>().ToList();
            return physicsObjects.Where(o => o.mBody == body).First();
        }

    }
}
