using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    class Level
    {
        List<Room> rooms = new List<Room>();

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }
    }
}
