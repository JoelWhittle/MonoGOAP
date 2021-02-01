using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace MonoEngine.core
{
    public class RayCastResult
    {
        public Fixture Fixture;
        public Vector2 HitPoint;
        public Vector2 HitPointSurfaceNormal;
    }
}