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

        public void Add(Room room)
        {
            if (room != null)
            {
                rooms.Add(room);
            }
        }

        public void RandomizeWalls()
        {
            foreach (Room room in Rooms)
            {
                foreach (GameObject go in room.GameObjects)
                {
                    Wall wall = go as Wall;
                    if (wall != null)
                    {
                        wall.Randomize();
                    }
                }
            }
        }
    }
}
