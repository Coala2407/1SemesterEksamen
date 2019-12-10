using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    public class Room
    {
        List<GameObject> gameObjects = new List<GameObject>();

        bool isRespawnable;

        bool isExit;

        bool isBossRoom;

        float exitTimer;

        string name;

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        /// <summary>
        /// Constructor. Most used. 
        /// </summary>
        /// <param name="gameObjects">Objects inside the room</param>
        /// <param name="isRespawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(List<GameObject> gameObjects, bool isRespawnable, bool isBossRoom, string name)
        {
            GameObjects = gameObjects;
            this.isRespawnable = isRespawnable;
            this.isBossRoom = isBossRoom;
            this.name = name;
            initialize();
        }

        /// <summary>
        /// Make empty room
        /// </summary>
        /// <param name="isRespawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(bool isRespawnable, bool isBossRoom, string name)
        {
            this.isRespawnable = isRespawnable;
            this.isBossRoom = isBossRoom;
            this.name = name;
            initialize();
        }

        /// <summary>
        /// Constructor for making the exit
        /// </summary>
        /// <param name="isExit"></param>
        public Room(bool isExit)
        {
            this.isExit = isExit;
        }

        private void initialize()
        {
            //Left wall
            for (int i = 0; i < Math.Ceiling((double)GameWorld.displayHeight / 64); i++)
            {
                Add(new Wall(new Vector2(0, i * 64), false, Wall.WallMode.Fixed));
            }
            //Right wall
            for (int i = 1; i < Math.Ceiling((double)GameWorld.displayHeight / 64 - 1); i++)
            {
                Add(new Wall(new Vector2(GameWorld.displayWidth - 64, i * 64), false, Wall.WallMode.Fixed));
            }
            //Top
            for (int i = 1; i < Math.Ceiling((double)GameWorld.displayWidth / 64); i++)
            {
                Add(new Wall(new Vector2(i * 64, 0), false, Wall.WallMode.Fixed));
            }
            //Bottom
            for (int i = 1; i < Math.Ceiling((double)GameWorld.displayWidth / 64); i++)
            {
                Add(new Wall(new Vector2(i * 64, GameWorld.displayHeight - 56), false, Wall.WallMode.Fixed));
            }
        }

        public bool IsRespawnable
        {
            get { return isRespawnable; }
            set { isRespawnable = value; }
        }

        /// <summary>
        /// Add an object to the room
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            if (gameObject != null)
            {
                GameObjects.Add(gameObject);
            }
        }

        public void Remove(GameObject gameObject)
        {
            if (gameObject != null)
            {
                GameObjects.Remove(gameObject);
            }
        }

        public void RespawnEnemies()
        {
            foreach (Enemy en in gameObjects.Where(x => x.GetType().Name == "Enemy"))
            {
                en.IsAlive = true;
                en.Initialize();
            }
        }

        public void RandomizeWalls()
        {
            foreach (Wall wall in gameObjects.Where(x => x.GetType().Name == "Wall"))
            {
                wall.Randomize();
            }
        }
    }
}
