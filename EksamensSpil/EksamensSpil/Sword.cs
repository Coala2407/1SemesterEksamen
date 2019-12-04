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

		public float Range;
		public float SwingTime;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Sword(GameObject holder)
		{
			this.holder = holder;
			initialize();
		}

		public Sword(Vector2 position)
		{
			this.position = position;
			initialize();
		}

		private void initialize()
		{
			this.ammo = 1;
			this.attackSpeed = (float)GameWorld.rng.Next(80, 151) / 100;
            this.clipSize = 1;
			this.SwingTime = (float)GameWorld.rng.Next(15, 51) / 100;
			this.Range = (float)GameWorld.rng.Next(80, 151);
			drawLayer = 0.2f;
            if (holder != null)
            {
                position = holder.Position;
            }
			ChangeSprite(Assets.SwordSprite);
		}

		public override ShootResult Attack()
		{

			ShootResult shootResult = base.Attack();

			if(shootResult == ShootResult.Successfull)
			{
				cooldown = attackSpeed;
				GameWorld.AddGameObject(new SwordSwing(this, Range), GameWorld.ActiveRoom);
				Console.WriteLine("Swing sword");
				return shootResult;
			}

			return shootResult;
		}

    }
}
