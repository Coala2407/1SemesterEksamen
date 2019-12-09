﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	public class Door : GameObject
	{

		private bool isLocked;
        private bool isOpen;
		private Room room;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Door(Vector2 position, Room room)
		{
			this.position = position;
			this.room = room;
			Initialize();
		}

		public void Initialize()
		{
			drawLayer = 0.9f;
		}

        public bool IsOpen { get; set; }

        /// <summary>
        /// Takes the Player to the room asociated with the door object
        /// </summary>
        /// <param name="room"></param>
        private void GoToRoom()
		{
			GameWorld.ActiveRoom = room;
			GameWorld.Player.PositionX = 300;
			GameWorld.Player.PositionY = 200;
			GameWorld.ActiveRoom.Add(GameWorld.Player);
		}

		public override void OnCollision(GameObject otherObject)
		{
			Player player = otherObject as Player;

			if(player != null && IsOpen == true)
			{
				// Takes the player to a new room
				GoToRoom();
			}
		}

		public override void Update(GameTime gameTime)
		{
			ChangeTheSprite();
		}

		// Detects player distance from the door
		public bool RangeToOpen(Player player)
		{
			Vector2 vectorDirection = player.Position - this.position;

			float distance = vectorDirection.Length();

			if(distance < 100)
			{
				Console.WriteLine("Door can open");
				return true;
			}
			else
			{
				return false;
			}
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
				ChangeSprite(Assets.DoorSprites[1]);
			}
			else
			{
				ChangeSprite(Assets.DoorSprites[0]);
			}
		}
	}
}
    