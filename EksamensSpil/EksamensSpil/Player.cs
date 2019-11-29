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


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Player(Vector2 position)
        {
            this.position = position;
            // Sets default Player sprite
            ChangeSprite(Assets.PlayerSprite);

            //Just a pistol for now. Will be random later.
            selectedWeapon = new Pistol(this);
        }


        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
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
            //Temp. T test random walls
            if (keyState.IsKeyDown(Keys.R))
            {
                if (!hasJustClicked)
                {
                    GameWorld.level.RandomizeWalls();
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

            if (selectedWeapon != null)
            {
                selectedWeapon.ReloadCooldown(gameTime);
                selectedWeapon.PositionY = position.Y + 20;
                selectedWeapon.PositionX = position.X + 20;
                //// rotation (Look at mouse)
                selectedWeapon.LookAt(GameWorld.GetMousePosition());
            }
        }
    }
}

