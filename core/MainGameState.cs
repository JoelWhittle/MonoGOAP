using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class MainGameState : State
    {
        public Random rand;
        public AIAgent agent;

        public  float TimeOfDay = 0;

        public override void OnStateEnter()
        {
            Context.Context.ScreenClearColor = Color.Black;

            rand = new Random();
            MapGenerator.GenerateMap(30,1);
            Random rnd = new Random();


            for (int i = 0; i < 100
                ; i++)
            {
                
                VisitorAgent go = new VisitorAgent(new Vector2(50, -80), 1);
                go.MaxHealth = 1;
                go.Name = "test";
                go.MyTexture = TextureManager.TextureDictionary["testSprite"];
                go.Size = new Vector2(20, 20);
                Game1.AddGameObject(go);
                agent = go;
                bool foundTile = false;

                while (!foundTile)
                {
                    int x = rnd.Next(1, 39); // creates a number between 1 and 12
                    int y = rnd.Next(1, 39);
                    go.CurrentTile = MapGenerator.Map[x, y];
                    foundTile = go.CurrentTile.Walkable;
                   
                }
                go.Position = go.CurrentTile.Position;
               
                //AIActionMoveTo action = new AIActionMoveTo();
              //  action.Target = MapGenerator.Map[x, y];
               // action.MyAgent = go;
                go.Home = go.CurrentTile;
                //  action.SetTarget(MapGenerator.Map[10,10], go);
               // go.JobQueue.Add(action);
                if(i < 100)
                {
                    go.Team = 1;
                }
                else
                {
                    go.Team = 2;
                    go.MyTexture = TextureManager.TextureDictionary["trump"];

                }

            }

            DrinkObject drink = new DrinkObject();
            drink.MyTexture = TextureManager.TextureDictionary["drinkstand"];

            drink.CurrentTile = MapGenerator.Map[10, 10];
            drink.Position = new Vector2(500, 500);
            Game1.AddGameObject(drink);




            drink = new DrinkObject();
            drink.MyTexture = TextureManager.TextureDictionary["drinkstand"];

            drink.CurrentTile = MapGenerator.Map[30, 30];
            drink.Position = new Vector2(1500, 1500);
            Game1.AddGameObject(drink);



            EatableObject eat = new EatableObject();
            eat.MyTexture = TextureManager.TextureDictionary["burgerstand"];

            eat.CurrentTile = MapGenerator.Map[20, 20];
            eat.Position = new Vector2(1000, 1000);
            Game1.AddGameObject(eat);





            eat = new EatableObject();
            eat.MyTexture = TextureManager.TextureDictionary["burgerstand"];

            eat.CurrentTile = MapGenerator.Map[15, 15];
            eat.Position = new Vector2(750, 750);
            Game1.AddGameObject(eat);


            Tile t = MapGenerator.GetTileAtPosition(new Vector2(0, 0));
             t = MapGenerator.GetTileAtPosition(new Vector2(120, 60));
             t = MapGenerator.GetTileAtPosition(new Vector2(320, 418));






            ToiletObject to = new ToiletObject();
            to.MyTexture = TextureManager.TextureDictionary["toilet"];

            to.CurrentTile = MapGenerator.Map[15, 25];
            to.Position = new Vector2(750, 1250);
            Game1.AddGameObject(to);






            Bin bin = new Bin();

            bin.CurrentTile = MapGenerator.Map[12, 12];
            bin.Position = new Vector2(600, 600);
            Game1.AddGameObject(bin);





            JanitorAgent j = new JanitorAgent(new Vector2(50, -80), 1);
            j.MaxHealth = 1;
            j.Name = "test";
            j.MyTexture = TextureManager.TextureDictionary["trump"];
            j.Size = new Vector2(20, 20);
            Game1.AddGameObject(j);
           bool  found = false;

            while (!found)
            {
                int x = rnd.Next(1, 39); // creates a number between 1 and 12
                int y = rnd.Next(1, 39);
                j.CurrentTile = MapGenerator.Map[x, y];
                found = j.CurrentTile.Walkable;

            }
            j.Position = j.CurrentTile.Position;

            //AIActionMoveTo action = new AIActionMoveTo();
            //  action.Target = MapGenerator.Map[x, y];
            // action.MyAgent = go;
            j.Home = j.CurrentTile;




            JanitorAgent j2 = new JanitorAgent(new Vector2(50, -80), 1);
            j2.MaxHealth = 1;
            j2.Name = "test";
            j2.MyTexture = TextureManager.TextureDictionary["trump"];
            j2.Size = new Vector2(20, 20);
            Game1.AddGameObject(j2);
            bool found3 = false;

            while (!found3)
            {
                int x = rnd.Next(1, 39); // creates a number between 1 and 12
                int y = rnd.Next(1, 39);
                j2.CurrentTile = MapGenerator.Map[x, y];
                found3 = j2.CurrentTile.Walkable;

            }
            j2.Position = j2.CurrentTile.Position;

            //AIActionMoveTo action = new AIActionMoveTo();
            //  action.Target = MapGenerator.Map[x, y];
            // action.MyAgent = go;
            j2.Home = j2.CurrentTile;

        }
        public override void OnStateExit()
        {

        }
        public override void OnStateUpdate(float dt)
        {

            TimeOfDay += dt;
            if(TimeOfDay > 720)
            {
                TimeOfDay = 0;
            }
            
            //quad tree

            Game1.quadtree = new Quadtree(new Rectangle(), 4);
            Game1.quadtree.Rect.Position = new Vector2(1000, 1000);
            Game1.quadtree.Rect.Size = new Vector2(2000, 2000);





            foreach (var go in Game1.GameObjects)//.Where(o=> o is Tile == false))
            {
                Game1.quadtree.AddObject(go);




            }

            //
            //foreach (GameObject go in Game1.GameObjects)
            //{
            //    go.Update((float)dt);
            //}
            for (int i = 0; i < Game1.GameObjects.Count(); i++)
            {
                Game1.GameObjects[i].Update((float)dt);
            }

            //  Camera.Position.X = agent.Position.X;
            //  Camera.Position.Y = -agent.Position.Y;

            if (InputManager.CurrentKeyState.IsKeyDown(Keys.Left))
            {
                Camera.Position.X -= (float)(300 * dt);
            }
            if (InputManager.CurrentKeyState.IsKeyDown(Keys.Right))
            {
                Camera.Position.X += (float)(300 * dt);

            }
            if (InputManager.CurrentKeyState.IsKeyDown(Keys.Up))
            {
                Camera.Position.Y += (float)(300 * dt);

            }
            if (InputManager.CurrentKeyState.IsKeyDown(Keys.Down))
            {
                Camera.Position.Y -= (float)(300 * dt);

            }


            var mouseState = Mouse.GetState();


            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // Do cool stuff here


                var mousePosition = new Point((mouseState.X + (int)Camera.GetPosition().X) , (mouseState.Y - (int)Camera.GetPosition().Y )
                  
                   );
                Tile tile = MapGenerator.GetTileAtPosition(new Vector2(mousePosition.X, mousePosition.Y));

                if(tile.Walkable)
                {

                    ToiletObject to = new ToiletObject();
                    to.MyTexture = TextureManager.TextureDictionary["toilet"];

                    to.CurrentTile =tile;
                    to.Position = tile.Position;
                    Game1.AddGameObject(to);
                }
            }



        }
    }
}
