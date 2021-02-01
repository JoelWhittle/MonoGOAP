using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class Rectangle
    {
        public Vector2 Position;
        public Vector2 Size;

        public bool ContainsPoint(GameObject go)
        {
            bool returnBool = false;
            Vector2 p = go.Position;

            if (p.X >= Position.X - Size.X && p.X <= Position.X + Size.X && p.Y >= Position.Y - Size.Y && p.Y <= Position.Y + Size.Y)
            {
                returnBool = true;
            }


            return returnBool;
        }

        public bool AABB(Rectangle other)
        {
            bool b = false;
            //Position.X -= (Size.X / 2);
            //Position.Y -= (Size.Y / 2);
            //other.Position.X -= (other.Size.X / 2);
            //other.Position.Y -= (other.Size.Y / 2);




            //return Position.X + Size.X < other.Position.X + other.Size.X &&
            //    Position.Y  + Size.Y < other.Position.Y + other.Size.Y &&
            //    other.Position.X + other.Size.X < Position.X + Size.X &&
            //    other.Position.Y + other.Size.Y < Position.Y + Size.Y;

            if((other.Position.X - other.Size.X > Position.X - Size.X && other.Position.X - other.Size.X < Position.X + Size.X )|| other.Position.X + other.Size.X > Position.X - Size.X && other.Position.X + other.Size.X < Position.X + Size.X)
            {
                var p = "waa";
                if ((other.Position.Y - other.Size.Y > Position.Y - Size.Y && other.Position.Y - other.Size.Y < Position.Y + Size.Y) || other.Position.Y + other.Size.Y > Position.Y - Size.Y && other.Position.Y + other.Size.Y < Position.Y + Size.Y)
                {
                    b = true;
                }
            }

            return b;
        }
    }
    public class Quadtree
    {
        public Rectangle Rect;
        public int Cap;

        public bool Subdivided = false;

        public Quadtree NW;
        public Quadtree NE;
        public Quadtree SW;
        public Quadtree SE;

        public List<GameObject> Occupants = new List<GameObject>();

        public Quadtree(Rectangle rectangle, int capacity)
        {
            Rect = rectangle;
            Cap = capacity;
        }

        public List<GameObject> CircleQuery(Vector2 position, int range)
        {
            List<GameObject> q = new List<GameObject>();

            Rectangle r = new Rectangle();
            r.Position = position;
            r.Size = new Vector2(range, range);
            var tmp = Query(r);

            foreach(var go in tmp)
            {
                if(Vector2.Distance(go.Position, position) < range)
                {
                    q.Add(go);
                }
            }


            return q;
        }


        public List<GameObject> Query(Rectangle range)
        {
            List<GameObject> q = new List<GameObject>();

            if(!Rect.AABB(range))
                {
                return q;
                }
            else
            {
                foreach(var go in Occupants)
                {
                    if(range.ContainsPoint(go))
                    {
                        q.Add(go);
                    }
                }

                if(Subdivided)
                {
                  q.AddRange (NW.Query(range));
                    q.AddRange(NE.Query(range));
                    q.AddRange(SW.Query(range));
                    q.AddRange(SE.Query(range));

                }
            }

            return q;
        }

        public bool AddObject(GameObject go)
        {
            if(!Rect.ContainsPoint(go))
            {
                return false;
            }
            if (Occupants.Count() < Cap)
            {
                Occupants.Add(go);
                return true;
            }
            else
            {
                if (!Subdivided)
                {
                    {
                        Subdivide(Rect);
                    }
                }

                if (NW.AddObject(go))
                {
                    return true;
                }
                else
                if (NE.AddObject(go))
                {
                    return true;

                }
                else if (SW.AddObject(go))
                {
                    return true;
                }
                else if (SE.AddObject(go))
                {
                    return true;
                }
                else
                {


                    return false;
                }

            }
        }

        public void Subdivide(Rectangle oldRect)
        {
            Subdivided = true;
            Rectangle nwRect = new Rectangle();
            nwRect.Position = new Vector2(oldRect.Position.X - (oldRect.Size.X / 2), oldRect.Position.Y - (oldRect.Size.Y / 2));
            nwRect.Size = new Vector2(oldRect.Size.X / 2, oldRect.Size.Y / 2);


            Rectangle neRect = new Rectangle();
            neRect.Position = new Vector2(oldRect.Position.X + (oldRect.Size.X / 2), oldRect.Position.Y - (oldRect.Size.Y / 2));
            neRect.Size = new Vector2(oldRect.Size.X / 2, oldRect.Size.Y / 2);

            Rectangle swRect = new Rectangle();
            swRect.Position = new Vector2(oldRect.Position.X - (oldRect.Size.X / 2), oldRect.Position.Y + (oldRect.Size.Y / 2));
            swRect.Size = new Vector2(oldRect.Size.X / 2, oldRect.Size.Y / 2);

            Rectangle seRect = new Rectangle();
            seRect.Position = new Vector2(oldRect.Position.X + (oldRect.Size.X / 2), oldRect.Position.Y + (oldRect.Size.Y / 2));
            seRect.Size = new Vector2(oldRect.Size.X / 2, oldRect.Size.Y / 2);


            NW = new Quadtree(nwRect, Cap);
            NE = new Quadtree(neRect, Cap);
            SW = new Quadtree(swRect, Cap);
            SE = new Quadtree(seRect, Cap);

        }


        public void Draw()
        {

        }

    }
}
