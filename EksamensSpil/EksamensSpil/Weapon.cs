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

	public enum ShootResult { Successfull, NotEnoughAmmo, CoolDownActive}

	public class Weapon : GameObject
	{

		protected float attackSpeed;
		protected float reloadSpeed;
		protected float cooldown;
		protected int ammo;
		protected int clipSize;
		protected bool canGunReload;


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
					if(cooldown < 0 && canGunReload != true)
					{
						cooldown = reloadSpeed;
					}
					canGunReload = true;
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

			ammo = clipSize;
			canGunReload = false;
	
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
			//if(cooldown >= 0)
			//{
			//	Console.WriteLine($"{cooldown}");
			//}
		}

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}

		public void ReloadCooldown(GameTime gameTime)
		{
			KeyboardState keyboard = Keyboard.GetState();


			if(keyboard.IsKeyDown(Keys.F))
			{
				canGunReload = false;
				Console.WriteLine("Reload Canceled");
			}

			if(canGunReload == true && ammo < clipSize && cooldown <= 0)
			{
				Reload();
				Console.WriteLine($"reloading in {cooldown}");
			}

			cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}
