﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
	class Item : GameObject
	{

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

		public void Use()
		{

		}


		public override void OnCollision(GameObject otherObject)
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}