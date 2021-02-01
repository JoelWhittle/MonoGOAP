using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoEngine.core;
using System.Collections.Generic;
using System.Linq;


using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Debug;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using System;

namespace MonoEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        public static Quadtree quadtree;

        public static  GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static MonoEngine.core.FSM MainGameFSM = new MonoEngine.core.FSM();
        public Color ScreenClearColor = Color.Aquamarine;

        public static  MainGameState mMainGameState;
        public static List<GameObject> GameObjects = new List<GameObject>();

        public static PhysicsManager mPhysicsManager = new PhysicsManager();


        public static List<IDrinkable> Drinkables = new List<IDrinkable>();
        public static List<IEatable> Eatables = new List<IEatable>();

        public static List<IBladderable> Toilets = new List<IBladderable>();
        public static List<IBin> Bins = new List<IBin>();
        public static List<ILitter> Litter = new List<ILitter>();

        public static Random mRandom = new Random();


        Vector2 position;
        Texture2D texture;

        public Game1()
        {
            mPhysicsManager.InitialiseWorld();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            MonoEngine.core.MainGameState mainGameState = new MonoEngine.core.MainGameState();
            mMainGameState = mainGameState;
            MonoEngine.core.SplashState splashScreenState = new MonoEngine.core.SplashState();

            MonoEngine.core.MainMenuState mainMenuState = new MonoEngine.core.MainMenuState();
            MonoEngine.core.State gameOverState = new MonoEngine.core.State();
            MonoEngine.core.State gameWinState = new MonoEngine.core.State();

            MainGameFSM.Context = this;
            MainGameFSM.AddState(splashScreenState, "SPLASH");
            MainGameFSM.AddState(mainGameState, "MAINGAME");
            MainGameFSM.AddState(mainMenuState, "MAINMENU");
            MainGameFSM.AddState(gameOverState, "GAMEOVER");
            MainGameFSM.AddState(gameWinState, "GAMEWIN");

            MainGameFSM.ChangeState("SPLASH");



         
            // test.ChangeState("1");

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           // this.IsMouseVisible = true;
            position = new Vector2(graphics.GraphicsDevice.Viewport.
                   Width / 2,
                                graphics.GraphicsDevice.Viewport.
                                Height / 2);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadTexture("testSprite",Content.Load<Texture2D>("testSprite"));
            TextureManager.LoadTexture("GrassSprite", Content.Load<Texture2D>("GrassSprite"));
            TextureManager.LoadTexture("MudSprite", Content.Load<Texture2D>("MudSprite"));
            TextureManager.LoadTexture("WaterSprite", Content.Load<Texture2D>("WaterSprite"));
            TextureManager.LoadTexture("RoadSprite", Content.Load<Texture2D>("RoadSprite"));

            TextureManager.LoadTexture("trump", Content.Load<Texture2D>("trump"));

            TextureManager.LoadTexture("burgerstand", Content.Load<Texture2D>("burgerstand"));
            TextureManager.LoadTexture("drinkstand", Content.Load<Texture2D>("drinkstand"));

            TextureManager.LoadTexture("toilet", Content.Load<Texture2D>("toilet"));
            TextureManager.LoadTexture("litter", Content.Load<Texture2D>("litter"));
            TextureManager.LoadTexture("bin", Content.Load<Texture2D>("bin"));


            TextureManager.LoadTexture("cursor", Content.Load<Texture2D>("cursor"));
            texture = TextureManager.TextureDictionary["cursor"];

            // TODO: use this.Content to load your game content here



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();
            // Update our sprites position to the current cursor 
            
            position.X = state.X;
            position.Y = state.Y;

            core.InputManager.CurrentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here
           // body.ApplyForce(new Vector2(4, -0),  body.Position);
            //body.ApplyTorque(0.5f);


           //if( body.AngularVelocity > 10)
               // {
               // body.AngularVelocity = 10;
           // }
                mPhysicsManager.Tick((float)gameTime.ElapsedGameTime.TotalSeconds);

            GameObjects = GameObjects.Where(o => o.flagForRemoval == false).ToList();
     




            MainGameFSM.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //
            core.InputManager.OldKeyState = core.InputManager.CurrentKeyState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            Window.Title = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();

            GraphicsDevice.Clear(ScreenClearColor);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (var go in GameObjects.Where(o => o is IRenderable ))
            {
                 ((IRenderable)go).Draw(spriteBatch);

            }
            //foreach (var go in GameObjects.Where(o => o is IRenderable))
            //{
            //    if (go is PhysicsObject)
            //    {
            //        Vector2 position = ((PhysicsObject)go).mBody.Position * mPhysicsManager.unitToPixel;
            //        Vector2 scale = new Vector2(((PhysicsObject)go).Size.X / (float)((PhysicsObject)go).GetTexture().Width, ((PhysicsObject)go).Size.Y / (float)((PhysicsObject)go).GetTexture().Height);
            //        spriteBatch.Draw(((PhysicsObject)go).GetTexture(), position, null, Color.White, ((PhysicsObject)go).mBody.Rotation, new Vector2(((PhysicsObject)go).GetTexture().Width / 2.0f, ((PhysicsObject)go).GetTexture().Height / 2.0f), scale, SpriteEffects.None, 0);

            //    }

            //}

            // Vector2 position = body.Position * unitToPixel;
            //  Vector2 scale = new Vector2(50 / (float)TextureManager.TextureDictionary["testSprite"].Width, 50 / (float)TextureManager.TextureDictionary["testSprite"].Height);
            //  spriteBatch.Draw(TextureManager.TextureDictionary["testSprite"], position, null, Color.White, body.Rotation, new Vector2(TextureManager.TextureDictionary["testSprite"].Width / 2.0f, TextureManager.TextureDictionary["testSprite"].Height / 2.0f), scale, SpriteEffects.None, 0);


            spriteBatch.Draw(texture, position, origin: new Vector2(16,
                                        16));

            spriteBatch.End();



            base.Draw(gameTime);
        }


        public static  void AddGameObject(GameObject go)
        {
            GameObjects.Add(go);
            if(go is IPhysicsObject)
            {
                ((PhysicsObject)go).mBody = BodyFactory.CreateRectangle(Game1.mPhysicsManager.PhysicsWorld, ((PhysicsObject)go).Size.X, ((PhysicsObject)go).Size.Y, 1f
                    ) ;
                ((PhysicsObject)go).mBody.BodyType = BodyType.Dynamic;
                ((PhysicsObject)go).mBody.Mass /= 10000;
                ((PhysicsObject)go).mBody.Position = ((PhysicsObject)go).Position;
                ((PhysicsObject)go).mBody.LocalCenter = new Vector2(((PhysicsObject)go).Size.X / 2, ((PhysicsObject)go).Size.Y / 2);
               // ((PhysicsObject)go).mBody.ApplyTorque(100000);

            }

            if(go is IDrinkable)
            {
                Drinkables.Add(((IDrinkable)go));
            }
            if (go is IEatable)
            {
                Eatables.Add(((IEatable)go));
            }

            if (go is IBladderable)
            {
                Toilets.Add(((IBladderable)go));
            }
            if (go is IBin)
            {
                Bins.Add(((IBin)go));
            }
            if (go is ILitter)
            {
                Litter.Add(((ILitter)go));
            }
        }

    }
}
