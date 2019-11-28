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
					ReloadCooldown();
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

			Console.WriteLine($"reloading in {cooldown}");

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

		public void ReloadCooldown()
		{
			KeyboardState keyboard = Keyboard.GetState(); 

			canGunReload = true;

			if(canGunReload == true)
			{
				cooldown = reloadSpeed;
			}

			if(keyboard.IsKeyDown(Keys.F))
			{
				canGunReload = false;
				Console.WriteLine("Reload Canceled");
			}

			if(cooldown <= 0 && canGunReload == true)
			{
				Reload();
			}

		}
	}
}
