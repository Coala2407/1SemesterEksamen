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
		private bool didLootDrop;
		private Vector2 position;
		private int lootType;


		public Chest(Vector2 position) : base(position)
		{
			this.position = position;
			//sprites = Assets.ChestSprites;
			lootType = GameWorld.rng.Next(1, 101);
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
			KeyboardState keyboard = Keyboard.GetState();

			if(keyboard.IsKeyDown(Keys.B) && isKeyDown == false && GameWorld.Player.ItemDetectionRange() == true)
			{
				ToggelChest();
				isKeyDown = true;
			}
			else if(keyboard.IsKeyUp(Keys.B) && isKeyDown == true && GameWorld.Player.ItemDetectionRange() == true)
			{
				isKeyDown = false;
			}
		}

		public void ToggelChest()
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

		public void LootDrop()
		{
			if(isOpen == true && didLootDrop == false && lootType <= 90)
			{
				Pistol pistol = new Pistol(new Vector2(this.position.X, this.position.Y - sprite.Height));
				GameWorld.AddGameObject(pistol, GameWorld.TheHall);
				didLootDrop = true;
			}

			if (isOpen == true && didLootDrop == false && lootType > 90)
			{

			}
		}
	}
}
