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
        //For picking up weapons and items
        private bool isTouchingWeapon;
        private bool isTouchingItem;
        private Weapon touchedWeapon;
        private Item touchedItem;


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
                GameWorld.RemoveGameObject(weapon);
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
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity.Y = -1;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity.Y = +1;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X = -1;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X = +1;
            }
            if (keyState.IsKeyDown(Keys.E))
            {
                if (isTouchingWeapon)
                {
                    PickUpWeapon(touchedWeapon);
                }
            }
            if (keyState.IsKeyDown(Keys.Tab))
            {
                CycleWeapons();
            }
            //Temp. To test random walls
            if (keyState.IsKeyDown(Keys.R))
            {
                if (!hasJustClicked)
                {
                    GameWorld.Level.RandomizeWalls();
                    hasJustClicked = true;
                }
            }
            else
            {
                hasJustClicked = false;
            }
            //End of test
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Attack();
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
            ItemDetectionRange();
            Move(gameTime);
            HandleInput();

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
            if (weapon != null)
            {
                isTouchingWeapon = true;
                touchedWeapon = weapon;
            }
        }

        public bool ItemDetectionRange()
        {
            Vector2 directionVector = new Vector2(0, 0);

            foreach (GameObject gameObject in GameWorld.TheHall.GameObjects)
            {
                if (gameObject is Weapon || gameObject is Item)
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

