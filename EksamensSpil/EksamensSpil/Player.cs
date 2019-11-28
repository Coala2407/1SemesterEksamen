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
			MouseState mouse = Mouse.GetState();
			KeyboardState keyState = Keyboard.GetState();
			if (keyState.IsKeyDown(Keys.W))
			{
				GameWorld.ActiveRoom = GameWorld.theHall;
			}
			else if (keyState.IsKeyDown(Keys.S))
			{
				GameWorld.ActiveRoom = GameWorld.theRoom;
			}
			else if (keyState.IsKeyDown(Keys.R))
			{
				GameWorld.level.RandomizeWalls();
			}
			if(mouse.LeftButton == ButtonState.Pressed)
			{
				selectedWeapon.Attack();
			}

			// =================================
			// rotation (Look at mouse)

			LookAt(GameWorld.GetMousePosition());
		}

		public override void Die()
		{
			throw new NotImplementedException();
		}

		public override void Attack()
		{
			throw new NotImplementedException();
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
			selectedWeapon.ReloadCooldown(gameTime);
			HandleInput();
		}
	}

}
