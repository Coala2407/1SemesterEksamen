﻿using Microsoft.Xna.Framework;
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
            health = 999;

            //Invinsibility for testing
            invinsibilityTimeAfterDamage = 1f;
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
            if (weapon != null && weapon.Holder == null)
            {
                if (!weapons.Contains(weapon) && weapons.Count < 3)
                {
                    weapons.Add(weapon);
                    weapon.Holder = this;
                    touchedWeapon = null;

                    CycleWeapons();
                }
            }
        }

        // TODO: need to add a position for where the weapons spawn
        public void DropWeapon(Weapon weapon)
        {
            if (weapon != null)
            {
                weapons.Remove(weapon);
                SelectedWeapon = null;
                weapon.Holder = null;
                weapon.IsHidden = false;

                CycleWeapons();
            }
        }

        /// <summary>
        /// Methode to open chests
        /// </summary>
        /// <param name="chest"></param>
        public void OpenChest(Chest chest)
        {
            if (chest != null)
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
            GameWorld.RemoveGameObject(this);
        }

        public override void Attack()
        {
            if (this.selectedWeapon != null)
            {
                selectedWeapon.Attack(GameWorld.Crosshair.Position);
            }
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
            //Get from superclass
            base.Update(gameTime);
            Move(gameTime);
            HandleInput();
            ItemDetectionRange();

            if (selectedWeapon != null)
            {
                selectedWeapon.ReloadCooldown(gameTime, this);
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
            //Get wall collisions from superclass
            base.OnCollision(otherObject);

            //Extra collisions:
            Weapon weapon = otherObject as Weapon;
            Chest chest = otherObject as Chest;
            if ((weapon != null && weapon.Holder == null))
            {
                //Is touching a weapon on the ground. Ready to pick it up in handle input
                touchedWeapon = weapon;
            }
            if (chest != null)
            {
                //Is touching a chest. Ready to open it in handle input
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
            //Collision with weapons and chests
            else if (touchedWeapon != null && touchedWeapon == otherObject)
            {
                //Is no longer touching the weapon
                touchedWeapon = null;
            }
            else if (touchedChest != null && touchedChest == otherObject)
            {
                //Is no longer touching the chest
                touchedChest = null;
            }
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
                selectedWeapon.IsHidden = true;
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
            else if (weapons.Count > 0)
            {
                selectedWeapon = weapons[0];
            }
            if (selectedWeapon != null)
            {
                selectedWeapon.IsHidden = false;
                selectedWeapon.Position = position;
            }
        }
    }
}

