using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensSpil
{
    class Pistol : Weapon
    {

		private float projectileSpeed;
        private Texture2D bulletSprite;
        Projectile projectile;
        GameWorld gameWorld;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Pistol()
		{
			this.ammo = 6;
			this.attackSpeed = 0.20f;
			this.clipSize = 6;
			this.reloadSpeed = 0.70f;
		}

		public override ShootResult Attack()
		{

			ShootResult shootResult = base.Attack();

			if (shootResult == ShootResult.Successfull)
			{
				--ammo;
				cooldown = attackSpeed;
				Console.WriteLine($"Shoot pistol: {ammo}");
                projectile = new Projectile(new Vector2(100, 100));
                return shootResult;
			}

			return shootResult;
		}

	}
}
