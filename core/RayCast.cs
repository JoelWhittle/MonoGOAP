using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
   public static class RayCast
    {
        public static RayCastResult FindClosest(this World world, Vector2 startPoint, Vector2 endPoint)
        {
            var result = new RayCastResult();

            world.RayCast((f, p, n, fr) =>
            {
            

                result.Fixture = f;
                result.HitPoint = p;
                result.HitPointSurfaceNormal = n;
                return fr;
            }, startPoint, endPoint);

            return result;
        }
    }
}
