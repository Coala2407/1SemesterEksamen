using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    public class Level
    {
        List<Room> rooms = new List<Room>();

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        /// <summary>
        /// Add a room to the level
        /// </summary>
        /// <param name="room"></param>
        public void Add(Room room)
        {
            if (room != null)
            {
                rooms.Add(room);
            }
        }
        /// <summary>
        /// Randomize all the non-fixed walls in the level
        /// </summary>
        public void RandomizeWalls()
        {
            foreach (Room room in Rooms)
            {
                foreach (Wall wall in room.GameObjects.Where(x => x is Wall))
                {
                    wall.Randomize();
                }
            }
        }
    }
}
