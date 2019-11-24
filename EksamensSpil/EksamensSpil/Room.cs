using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    class Room
    {
        List<GameObject> gameObjects = new List<GameObject>();

        bool isSpawnable;

        bool isExit;

        bool isBossRoom;

        bool isActive;

        float exitTimer;

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Room()
        { }

        /// <summary>
        /// Constructor. Most used. 
        /// </summary>
        /// <param name="gameObjects">Objects inside the room</param>
        /// <param name="isSpawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(List<GameObject> gameObjects, bool isSpawnable, bool isBossRoom)
        {
            GameObjects = gameObjects;
            this.isSpawnable = isSpawnable;
            this.isBossRoom = isBossRoom;
        }

        /// <summary>
        /// Make empty room
        /// </summary>
        /// <param name="isSpawnable">Can enemies spawn randomly inside the room?</param>
        /// <param name="isBossRoom">Is it a boss room?</param>
        public Room(bool isSpawnable, bool isBossRoom)
        {
            this.isSpawnable = isSpawnable;
            this.isBossRoom = isBossRoom;
        }

        /// <summary>
        /// Constructor for making the exit
        /// </summary>
        /// <param name="isExit"></param>
        public Room(bool isExit)
        {
            this.isExit = isExit;
        }
    }
}
