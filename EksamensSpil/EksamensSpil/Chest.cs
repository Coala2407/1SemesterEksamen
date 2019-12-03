using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EksamensSpil
{
	class Chest : GameObject
	{

		private GameObject item;
		private bool isOpen;
		private bool isKeyDown;


		public Chest(Vector2 position)
		{
			this.position = position;
            initialize();
		}

        private void initialize()
        {
            ChangeSprite(Assets.ChestSprites[0]);
            int rng = GameWorld.rng.Next(1, 101);
            if (rng <= 90)
            {
                int rng2 = GameWorld.rng.Next(0, 2);
                if (rng2 == 0)
                {
                    item = new Pistol(this);
                    item.PositionY -= sprite.Height;
                }
                else
                {
                    item = new Sword(this);
                    item.PositionY -= sprite.Height;
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject otherObject)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			ChestStateInput();

			ChangeTheSprite();

			LootDrop();
		}

		// Different methodes made to toggel between two sprites

		public void ChestStateInput()
		{
			//KeyboardState keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();

			if(Keyboard.IsPressed(Keys.B) && isKeyDown == false && GameWorld.Player.ItemDetectionRange() == true)
			{
				ToggleChest();
				isKeyDown = true;
			}
            else if (!Keyboard.IsPressed(Keys.B) && isKeyDown == true && GameWorld.Player.ItemDetectionRange() == true)
            {
                isKeyDown = false;
            }
        }

		public void ToggleChest()
		{

			if(IsChestOpen() == false)
			{
				OpenChest();
			}
			else
			{
				CloseChest();
			}
			
		}

		public void OpenChest()
		{
			isOpen = true;
		}

		public void CloseChest()
		{
			isOpen = false;
		}

		public bool IsChestOpen()
		{
			return isOpen;
		}

		// switches between two sprites
		public void ChangeTheSprite()
		{
			if (isOpen == true)
			{
				//sprite = sprites[1];
				ChangeSprite(Assets.ChestSprites[1]);
			}
			else
			{
				//sprite = sprites[0];
				ChangeSprite(Assets.ChestSprites[0]);
			}
		}

		// Makes a random GameObject from the Item Class spawn when opened. Has a slight chance of nothing happening
		public void LootDrop()
		{
			if(isOpen == true && item != null)
			{
				GameWorld.AddGameObject(item, GameWorld.ActiveRoom);
			}
		}
	}
}
