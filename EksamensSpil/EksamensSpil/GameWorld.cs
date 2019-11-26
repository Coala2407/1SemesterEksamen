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
		//TODO: Test
		Player player;
        //To get random numbers
        public static Random rng = new Random();
        //To add and remove objects in runtime
        public static List<GameObject> NewGameObjects = new List<GameObject>();
        public static List<GameObject> RemoveGameObjects = new List<GameObject>();
        public void AddGameObject(GameObject gameObject, Room room)
        {
            gameObject.Room = room;
            NewGameObjects.Add(gameObject);
        }

        //Levels
        Level level;
        //Rooms
        Room theRoom;
        Room theHall;

        //Player
        Player player;

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

            IsMouseVisible = true;

            //Make player
            player = new Player();

            //Make levels
            level = new Level();

            //Make rooms
            theRoom = new Room(false, false, "The Room");
            theHall = new Room(false, false, "The Hall");

            //Active room
            theHall.IsActive = true;

            //Add object to room
            theHall.Add(new Enemy());

            //Add rooms to level
            level.Rooms.Add(theRoom);
            level.Rooms.Add(theHall);
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
			// TODO: use this.Content to load your game content here

			player = new Player(new Vector2(200, 400));
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

			// TODO: Add your update logic here

			//TODO: Test Player

			player.Update(gameTime);
            
            //Update the player, each frame
            //player.Update(gameTime);

            foreach (Room room in level.Rooms  /*active level*/)
            {
                if (room.IsActive /*active room*/)
                {
                    foreach (GameObject go in room.GameObjects)
                    {
                        //Update all objects in active room, each frame
                        go.Update(gameTime);
                    }
                    break;
                }
            }
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

			spriteBatch.Begin();

			//TODO: Tester rotation
			player.Draw(spriteBatch);

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
