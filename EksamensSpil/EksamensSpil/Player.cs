using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
    class Player : Character
    {

		private List<Weapon> weapons = new List<Weapon>();
		private List<Item> items = new List<Item>();
		private Weapon selectedWeapon;
		private Item selectedItem;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Player()
		{

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
		public void HandleInput(GameTime gameTime)
		{

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
    }
}
