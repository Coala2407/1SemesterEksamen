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

		private bool isLocked;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Door(Vector2 position)
		{

		}

		public void initialize()
		{
			drawLayer = 1.0f;
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
			ChangeTheSprite();
		}

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}

		public void ToggleDoor()
		{

			if (IsDoorOpen() == false)
			{
				OpenDoor();
			}
			else
			{
				CloseDoor();
			}

		}

		public void OpenDoor()
		{
			IsOpen = true;
		}

		public void CloseDoor()
		{
			IsOpen = false;
		}

		public bool IsDoorOpen()
		{
			return IsOpen;
		}

		// switches between two sprites
		public void ChangeTheSprite()
		{
			if (IsOpen == true)
			{
				ChangeSprite(null);
			}
			else
			{
				ChangeSprite(Assets.DoorSprites[0]);
			}
		}
	}
}
    