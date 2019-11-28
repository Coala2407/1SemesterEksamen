using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{

	public enum ShootResult { Successfull, NotEnoughAmmo, CoolDownActive}

	public class Weapon : GameObject
	{

		protected float attackSpeed;
		protected float reloadSpeed;
		protected float cooldown;
		protected int ammo;
		protected int clipSize;


		public virtual ShootResult Attack()
		{

			if(ammo != 0 && cooldown <= 0)
			{
				return ShootResult.Successfull;
			}
			else
			{
				if(ammo <= 0 && cooldown <= 0)
				{
					Reload();
					return ShootResult.NotEnoughAmmo;
				}
				else
				{
					return ShootResult.CoolDownActive;
				}
			}

		}

		public void Reload()
		{
			cooldown = reloadSpeed;

			Console.WriteLine($"reloading in {cooldown}");

			ammo = clipSize;
	
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{

			}
		}

		public int Ammo
		{
			get
			{
				return ammo;
			}
			set
			{

			}
		}

		public int ClipSize
		{
			get
			{
				return clipSize;
			}
			set
			{

			}
		}

		public override void OnCollision(GameObject otherObject)
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
			cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			//if(cooldown >= 0)
			//{
			//	Console.WriteLine($"{cooldown}");
			//}
		}

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}
	}
}
