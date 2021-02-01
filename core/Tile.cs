using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class Tile : SpriteObject
    {
        public int GridX;
        public int GridY;
        public TileType MyType;
        public float MovementCost = 2;
        public bool Walkable = true;

        public float g;
        public Tile parent = null;

       public  float GetF(Tile target) 
        { return g + GetH(target); 
        }
       public  float GetH(Tile target)
        {
            var n = Math.Abs(target.GridX - GridX)  + Math.Abs(target.GridY - GridY);
            return n;
        }
        public List<Tile> Neighbours = new List<Tile>();
        public enum TileType
        {
            Grass,
            Water,
            Mud,
            Path
        }

        public  Tile(Vector2 position, TileType myType) 
        {
            Name = "Tile";
            Position = position;
            MyType = myType;
            Size = new Vector2(50, 50);
            CurrentTile = this;

            switch (myType)
            {
                case TileType.Grass:
                 MyTexture = TextureManager.TextureDictionary["GrassSprite"];

                    break;
                case TileType.Mud:
                    MyTexture = TextureManager.TextureDictionary["MudSprite"];
                    MovementCost = 100;

                    break;
                case TileType.Water:
                    MyTexture = TextureManager.TextureDictionary["WaterSprite"];
                    MovementCost = 99999;
                    Walkable = false;
                    break;
                case TileType.Path:
                    MyTexture = TextureManager.TextureDictionary["RoadSprite"];
                    MovementCost = 0.5f;
                    break;
            }

        }
      

    }
}
