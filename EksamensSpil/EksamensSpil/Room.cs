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

        bool isSpawnable;

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
        /// <param name="isSpawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(List<GameObject> gameObjects, bool isSpawnable, bool isBossRoom, string name)
        {
            GameObjects = gameObjects;
            this.isSpawnable = isSpawnable;
            this.isBossRoom = isBossRoom;
            this.name = name;
            initialize();
        }

        /// <summary>
        /// Make empty room
        /// </summary>
        /// <param name="isSpawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(bool isSpawnable, bool isBossRoom, string name)
        {
            this.isSpawnable = isSpawnable;
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

        //Fix this shit
        private void initialize()
        {
            //Left wall
            for (int i = 0; i < Math.Ceiling((double)GameWorld.displayHeight / 64); i++)
            {
                Add(new Wall(new Vector2(0, i * 64), false, Wall.WallMode.Fixed));
            }
            //Right wall
            for (int i = 0; i < Math.Ceiling((double)GameWorld.displayHeight / 64); i++)
            {
                Add(new Wall(new Vector2(GameWorld.displayWidth - 56, i * 64), false, Wall.WallMode.Fixed));
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
    }
}
