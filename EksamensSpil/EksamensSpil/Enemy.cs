using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
    class Enemy : Character
    {

		private Weapon weapon;
		private int lootDropChance;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Enemy()
		{

		}

		//TODO Do we need this?
		public override void Attack()
		{

		}

        public override void Die()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        //TODO Do we need this?
        public override void Reload()
		{

		}

        public override int UpdateHealth(int change)
        {
            throw new NotImplementedException();
        }
    }
}
