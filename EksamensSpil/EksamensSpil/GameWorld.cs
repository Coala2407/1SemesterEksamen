using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EksamensSpil
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;


        SpriteBatch spriteBatch;
        //To get random numbers
        public static Random rng = new Random();
        //To add and remove objects in runtime
        public static List<GameObject> NewGameObjects = new List<GameObject>();
        public static List<GameObject> RemoveGameObjects = new List<GameObject>();
        public static void AddGameObject(GameObject gameObject, Room room)
        {
            gameObject.Room = room;
            NewGameObjects.Add(gameObject);
        }

        //Active room. Only objects from the active room, player and crosshair get updated
        public static Room ActiveRoom;

        //Levels
        public static Level level;

        //Rooms

        public static Room theRoom;
        public static Room theHall;

        //Player
        public static Player player;

        //Crosshair
        Crosshair crosshair;

        //Enemy
        Enemy enemy;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
			//Screen setup
			graphics.PreferredBackBufferWidth = /*GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width*/ 1920;
			graphics.PreferredBackBufferHeight = /*GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height*/ 1080;
			graphics.ApplyChanges();
			//graphics.ToggleFullScreen();

			//Make levels
			level = new Level();

            //Make rooms
            theRoom = new Room(false, false, "The Room");
            theHall = new Room(false, false, "The Hall");

            //Add rooms to level
            level.Add(theRoom);
            level.Add(theHall);

            ActiveRoom = theHall;
            //Run, game, Run!
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
            Assets.LoadAssets(Content);
            //Assets loaded. All sprites etc. are accessable from this point:

            //Make player and crosshair
            crosshair = new Crosshair();
            player = new Player(new Vector2(200, 400));

            //Add object to room
            theHall.Add(new Enemy(new Vector2(600, 200)));
            theHall.Add(new Enemy(new Vector2(900, 500), true));
            for (int i = 0; i < 10; i++)
            {
                theHall.Add(new Wall(new Vector2(0, i * 64), false, Wall.WallMode.Randomized));
            }
            theHall.Add(new Wall(new Vector2(200, 600)));
            theHall.Add(new Wall(new Vector2(280, 600), true, Wall.WallMode.Toggled));
            theHall.Add(new Pistol(new Vector2(1000, 1000)));
            //Make walls random
            level.RandomizeWalls();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            
            //Update player and crosshair
            player.Update(gameTime);
            crosshair.Update(gameTime);
            

            foreach (GameObject go in ActiveRoom.GameObjects)
            {
                //Update all objects in active room
                go.Update(gameTime);
            }

            //Add new objects to rooms
            foreach (GameObject go in NewGameObjects)
            {
                go.Room.Add(go);
            }
            NewGameObjects.Clear();


            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            //Draw player and croshair
            player.Draw(spriteBatch);
            crosshair.Draw(spriteBatch);

            //Draws all objects in active room
            foreach (GameObject go in ActiveRoom.GameObjects)
            {
                //Update all objects in active room
                go.Draw(spriteBatch);
            }



            //TODO: Tester rotation
            player.Draw(spriteBatch);

            crosshair.Draw(spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        // Borrowed from another projekt
        public static Vector2 GetMousePosition()
        {
            return Mouse.GetState().Position.ToVector2();
        }



    }
}
