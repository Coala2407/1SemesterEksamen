using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace EksamensSpil
{
    public class Player : Character
    {

        private List<Weapon> weapons = new List<Weapon>();
        private List<Item> items = new List<Item>();
        private Weapon selectedWeapon;
        private Item selectedItem;
        private Room previousRoom;
        private bool hasJustClicked;
        private float detectionDistance = 60;

        //Collisions
        private bool collideBottom;
        private bool collideTop;
        private bool collideLeft;
        private bool collideRight;
        //For picking up weapons and items
        private bool isTouchingWeapon;
        private bool isTouchingItem;
		private bool isTouchingChest;
        private Weapon touchedWeapon;
        private Item touchedItem;
		private Chest touchedChest;
        private Wall touchedWall;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Player(Vector2 position)
        {
            this.position = position;
            // Sets default Player sprite
            ChangeSprite(Assets.PlayerSprite);
            drawLayer = 0.1f;
            //Just a pistol for now. Will be random later.
            //selectedWeapon = new Pistol(this);
        }


        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set { selectedWeapon = value; }
        }

        /// <summary>
        /// Method to use item.
        /// </summary>
        public void UseItem()
        {

        }

        /// <summary>
        /// Method to pick up items
        /// </summary>
        /// <param name="item"></param>
        public void PickUpItem(Item item)
        {

        }

        /// <summary>
        /// Method to pick up weapons
        /// </summary>
        /// <param name="weapon"></param>
        public void PickUpWeapon(Weapon weapon)
        {
            if (weapon != null)
            {
                if (!weapons.Contains(weapon))
                {
                    weapons.Add(weapon);
                }
                SelectedWeapon = weapon;
                weapon.Holder = this;
                GameWorld.RemoveGameObject(weapon);
            }
        }

		// TODO: need to add a position for where the weapons spawn
		public void DropWeapon(Weapon weapon)
		{
			if(weapon != null)
			{
				GameWorld.AddGameObject(weapon, GameWorld.ActiveRoom);
				weapon.PositionX = this.PositionX;
				weapon.PositionY = this.PositionY - this.sprite.Height;
			}
			weapon.Holder = null;
			CycleWeapons();

			if(weapons.Contains(weapon))
			{
				weapons.Remove(weapon);
			}
		}

		/// <summary>
		/// Methode to open chests
		/// </summary>
		/// <param name="chest"></param>
		public void OpenChest(Chest chest)
		{
			if(chest != null)
			{
				chest.ToggleChest();
			}
		}

        /// <summary>
        /// User input to Player
        /// </summary>
        /// <param name="gameTime"></param>
        public void HandleInput()
        {
            //Stop moving when you're not pressing a key
            velocity = Vector2.Zero;
            MouseState mouse = Mouse.GetState();

            if (Keyboard.IsPressed(Keys.W))
            {
                velocity.Y = -1;
            }
            if (Keyboard.IsPressed(Keys.S))
            {
                velocity.Y = +1;
            }
            if (Keyboard.IsPressed(Keys.A))
            {
                velocity.X = -1;
            }
            if (Keyboard.IsPressed(Keys.D))
            {
                velocity.X = +1;
            }
            if (Keyboard.HasBeenPressed(Keys.E))
            {

                PickUpWeapon(touchedWeapon);
                
				OpenChest(touchedChest);
				
            }
            if (Keyboard.HasBeenPressed(Keys.Tab))
            {
                CycleWeapons();
            }
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Attack();
            }
			if(Keyboard.HasBeenPressed(Keys.Back))
			{
				DropWeapon(selectedWeapon);
			}
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }

        public override void Attack()
        {
            if (this.selectedWeapon != null)
            {
                selectedWeapon.Attack();
            }
        }

        public override int UpdateHealth(int change)
        {
            throw new NotImplementedException();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            HandleInput();
            ItemDetectionRange();

            if (selectedWeapon != null)
            {
                selectedWeapon.ReloadCooldown(gameTime);
                selectedWeapon.PositionY = position.Y + 20;
                selectedWeapon.PositionX = position.X;
                // rotation (Look at mouse)
                selectedWeapon.LookAt(GameWorld.GetMousePosition());
                //Flip the weapon depending on crosshair weapon
                selectedWeapon.SpriteFlippedX = GameWorld.Crosshair.PositionX < position.X;
            }
            spriteFlippedY = GameWorld.Crosshair.PositionX < position.X;
        }

        public override void OnCollision(GameObject otherObject)
        {

			Weapon weapon = otherObject as Weapon;
			Chest chest = otherObject as Chest;
			

            
            if (weapon != null)
            {
                //Is touching a weapon. Ready to pick it up in handle input
                touchedWeapon = weapon;
            }

            Wall wall = otherObject as Wall;

            if (wall != null && !wall.IsHidden)
            {
                //touchedWall = wall;
                Rectangle otherBox = wall.GetCollisionBox();
                Rectangle thisBox = GetCollisionBox();
                if (thisBox.Bottom >= otherBox.Top && thisBox.Top < otherBox.Top)
                {
                    if (velocity.Y > 0)
                    {
                        velocity.Y = 0;
                    }
                }
                if (thisBox.Left <= otherBox.Right && thisBox.Right > otherBox.Right)
                {
                    if (velocity.X < 0)
                    {
                        velocity.X = 0;
                    }
                }
                //collideBottom = thisBox.Bottom >= otherBox.Top && thisBox.Top < otherBox.Top;
                //collideTop = thisBox.Top <= otherBox.Bottom && thisBox.Bottom > otherBox.Bottom;
                //collideLeft = thisBox.Left <= otherBox.Right && thisBox.Right > otherBox.Right;
                //collideRight = thisBox.Right >= otherBox.Left && thisBox.Left < otherBox.Left;

            }

			if(chest != null && weapon == null)
			{
				isTouchingChest = true;
				touchedChest = chest;
			}

        }

        public override void CheckCollision(GameObject otherObject)
        {
            //Regular collision with objects
            if (GetCollisionBox().Intersects(otherObject.GetCollisionBox()))
            {
                OnCollision(otherObject);
            }
            //Collision with weapons
            else if (touchedWeapon != null && touchedWeapon == otherObject)
            {
                //Is no longer touching the weapon
                touchedWeapon = null;
            }

			else if(touchedChest != null && touchedChest == otherObject)
			{
				touchedChest = null;
			}
            //else if (touchedWall != null && touchedWall == otherObject)
            //{
            //    //Is no longer touching the weapon
            //    touchedWall = null;
            //}

        }

        public bool ItemDetectionRange()
        {
            Vector2 directionVector = new Vector2(0, 0);

            foreach (GameObject gameObject in GameWorld.ActiveRoom.GameObjects)
            {
                if (gameObject is Weapon || gameObject is Item || gameObject is Chest)
                {
                    directionVector = gameObject.Position - GameWorld.Player.position;
                }
            }

            float distance = directionVector.Length();

            if (distance < detectionDistance)
            {
                Console.WriteLine($"Player within distance {distance}");
                return true;
            }
            else
            {
                return false;
            }

        }

        private void CycleWeapons()
        {
            if (weapons.Count > 0 && selectedWeapon != null)
            {
                selectedWeapon.Reload();
                int selectedWeaponIndex = weapons.IndexOf(selectedWeapon);
                if (selectedWeaponIndex + 1 == weapons.Count)
                {
                    selectedWeapon = weapons[0];
                }
                else
                {
                    selectedWeapon = weapons[selectedWeaponIndex + 1];
                }
            }
        }
    }
}

