using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
