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
			this.position = holder.Position;
			Initialize();
		}

		public Sword(Vector2 position)
		{
			this.position = position;
			Initialize();
		}

		private void Initialize()
		{
			this.ammo = 1;
			this.attackSpeed = 1.20f;
			this.clipSize = 1;
			this.SwingTime = 0.30f;
			this.Range = 100;
			drawLayer = 0.1f;
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
