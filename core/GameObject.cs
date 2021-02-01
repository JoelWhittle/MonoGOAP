using Microsoft.Xna.Framework;
using MonoEngine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine
{
    public class GameObject
    {
        public Tile CurrentTile;
        public Vector2 Position;
        public string Name = "GameObject";
        public bool flagForRemoval = false;
        public virtual void Update(float dt)
        {

        }
    }
}
