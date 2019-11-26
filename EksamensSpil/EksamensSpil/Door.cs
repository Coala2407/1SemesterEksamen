using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	class Door : GameObject
	{

		private bool isOpen;
		private bool isLocked;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Door()
		{

		}

		/// <summary>
		/// Takes the Player to the room asociated with the door object
		/// </summary>
		/// <param name="room"></param>
		private void GoToRoom(Room room)
		{

		}

		public override void OnCollision(GameObject otherObject)
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}
	}
}
    