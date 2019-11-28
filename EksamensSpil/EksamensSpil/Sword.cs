using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    class Sword : Weapon
    {

		private float range;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Sword(GameObject holder)
		{
			this.ammo = 1;
			this.attackSpeed = 0;
			this.clipSize = 1;
			this.reloadSpeed = 1.20f;
		}

		public override ShootResult Attack()
		{

			ShootResult shootResult = base.Attack();

			if(shootResult == ShootResult.Successfull)
			{
				--ammo;
				Console.WriteLine("Swing sword");
				return shootResult;
			}

			return shootResult;
		}

    }
}
