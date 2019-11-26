using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	class Weapon : GameObject
	{

		protected float attackSpeed;
		protected float reloadSpeed;
		protected int ammo;
		protected int clipSize;


		public void attack()
		{

		}

		public void Reload()
		{

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
			throw new NotImplementedException();
		}

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}
