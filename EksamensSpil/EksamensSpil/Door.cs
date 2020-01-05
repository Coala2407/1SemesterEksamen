using System;
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
		//private bool isOpen;
		private Room previousRoom;
		private bool startTimer = false;
		private float timer;

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


		public float Timer
		{
			get { return timer; }
			set
			{
				timer = value;
			}
		}

		public bool StartTimer
		{
			get { return startTimer; }
			set
			{
				startTimer = value;
			}
		}

        public bool IsOpen { get; set; }


        /// <summary>
        /// Takes the Player to the room associated with the door object
        /// </summary>
        /// <param name="room"></param>
        private void GoToRoom()
        {
            this.IsOpen = false;
			startTimer = false;
			previousRoom = GameWorld.ActiveRoom;
            GameWorld.ActiveRoom = room;
            GameWorld.ActiveRoom.Remove(GameWorld.Player);
            room.Add(GameWorld.Player);
			MovePlayer();
            if (room.IsRespawnable)
            {
                room.RespawnEnemies();
            }
        }

		/// <summary>
		/// Moves the player to the door connected to the previous room
		/// </summary>
		public void MovePlayer()
		{

			foreach (GameObject gameObject in GameWorld.ActiveRoom.GameObjects)
			{

				if (gameObject is Door)
				{
					Door door = gameObject as Door;

					// if the door is in the left side of the room.
					if (door.room == previousRoom && door.PositionX <= 960)
					{
						GameWorld.Player.PositionX = door.PositionX + this.sprite.Width;
						GameWorld.Player.PositionY = door.PositionY;
					}

					// if the door is in the right side of the room.
					else if (door.room == previousRoom && door.PositionX > 960)
					{
						GameWorld.Player.PositionX = door.PositionX - this.sprite.Width;
						GameWorld.Player.PositionY = door.PositionY;
					}

				}
				
			}

			
		}

        public override void OnCollision(GameObject otherObject)
        {
            Player player = otherObject as Player;

            if (player != null && IsOpen == true)
            {
                // Takes the player to a new room
                GoToRoom();
            }
        }

        public override void Update(GameTime gameTime)
        {
			// Timer in boss room
			if(startTimer == true)
			{
				timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			}

			if(GameWorld.ActiveRoom == GameWorld.BossRoom)
			{
				DefeatBoss();
			}

            ChangeTheSprite();
        }

        // Detects player distance from the door
        public bool RangeToOpen()
        {
            Vector2 vectorDirection = GameWorld.Player.Position - this.position;

            float distance = vectorDirection.Length();

            if (distance < 140)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// Sets and starts the timer for when the player can escape the boss room.
		/// </summary>
		public void EscapeTimer()
		{
			startTimer = true;
			timer = 15;						
		}

		/// <summary>
		/// Checks if the boss(es) in the room is dead
		/// </summary>
		/// <returns></returns>
		public bool DefeatBoss()
		{
			foreach(GameObject gameObject in GameWorld.ActiveRoom.GameObjects)
			{
				if(gameObject is Enemy)
				{
					Enemy enemy = gameObject as Enemy;

					if(enemy.IsBoss == true && enemy.IsAlive == true)
					{
						return false;
					}

					else
					{
						return true;
					}

				}
			}

			return true;
		}

		// A series of methodes that toggether makes it possible to toggle between true and false.
        public void ToggleDoor()
        {
			if(GameWorld.ActiveRoom != GameWorld.BossRoom || startTimer == true && timer <= 0 || DefeatBoss() == true)
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

        // switches between three sprites
        public void ChangeTheSprite()
        {
			// Locket door for bossRoom
			if(GameWorld.ActiveRoom == GameWorld.BossRoom && startTimer == false && DefeatBoss() == false || 
			   GameWorld.ActiveRoom == GameWorld.BossRoom && startTimer == true && timer > 0 && DefeatBoss() == false)
			{
				ChangeSprite(Assets.DoorSprites[2]);
			}
			// open door
            else if (IsOpen == true)
            {
                ChangeSprite(Assets.DoorSprites[1]);
            }
			// closed door
            else
            {
                ChangeSprite(Assets.DoorSprites[0]);
            }
        }
    }
}
