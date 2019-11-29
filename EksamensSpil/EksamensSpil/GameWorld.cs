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

        public static void RemoveGameObject(GameObject gameObject)
        {
            RemoveGameObjects.Add(gameObject);
        }

        //Active room. Only objects from the active room, Player and Crosshair get updated
        public static Room ActiveRoom;

        //Levels
        public static Level Level;

        //Rooms

        public static Room TheRoom;
        public static Room TheHall;

        //Player
        public static Player Player;

        //Crosshair
        public static Crosshair Crosshair;

        //Debug hitboxes
#if DEBUG
        Texture2D collisionTexture;
        private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.GetCollisionBox();
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
#endif

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
            Level = new Level();

            //Make rooms
            TheRoom = new Room(false, false, "The Room");
            TheHall = new Room(false, false, "The Hall");

            //Add rooms to level
            Level.Add(TheRoom);
            Level.Add(TheHall);

            ActiveRoom = TheHall;
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

            //Make Player and Crosshair
            Crosshair = new Crosshair();
            Player = new Player(new Vector2(200, 400));

            //Add object to room
            TheHall.Add(new Enemy(new Vector2(600, 200)));
            TheHall.Add(new Enemy(new Vector2(900, 500), true));
            for (int i = 0; i < 10; i++)
            {
                TheHall.Add(new Wall(new Vector2(0, i * 64), false, Wall.WallMode.Randomized));
            }
            TheHall.Add(new Wall(new Vector2(200, 600)));
            TheHall.Add(new Wall(new Vector2(280, 600), true, Wall.WallMode.Toggled));
            TheHall.Add(new Pistol(new Vector2(1000, 1000)));
            TheHall.Add(new Pistol(new Vector2(600, 100)));
            //Make walls random
            Level.RandomizeWalls();


            //Load Debug hitbox
#if DEBUG
            collisionTexture = Content.Load<Texture2D>("whitepixel");
#endif
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


            //Update Player and Crosshair
            Player.Update(gameTime);
            Crosshair.Update(gameTime);

            foreach (GameObject go in ActiveRoom.GameObjects)
            {
                //Update all objects in active room
                go.Update(gameTime);
                //Check player collision
                Player.CheckCollision(go);
                foreach (GameObject other in ActiveRoom.GameObjects)
                {
                    go.CheckCollision(other);
                }
            }

            //Add new objects to rooms
            foreach (GameObject go in NewGameObjects)
            {
                go.Room.Add(go);
            }
            NewGameObjects.Clear();
            //Remove gameobjects from rooms
            foreach (GameObject go in RemoveGameObjects)
            {
                if (go.Room != null)
                {
                    go.Room.Remove(go);
                }
                else
                {
                    ActiveRoom.Remove(go);
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

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            //Draw Player, Player selected weapon, and croshair
            Player.Draw(spriteBatch);
#if DEBUG
            DrawCollisionBox(Player);
#endif
            if (Player.SelectedWeapon != null)
            {
                Player.SelectedWeapon.Draw(spriteBatch);
            }
            Crosshair.Draw(spriteBatch);

            //Draws all objects in active room
            foreach (GameObject go in ActiveRoom.GameObjects)
            {
                //Update all objects in active room
                go.Draw(spriteBatch);
#if DEBUG
                DrawCollisionBox(go);
#endif
            }

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
