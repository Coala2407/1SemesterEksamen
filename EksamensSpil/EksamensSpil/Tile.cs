using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
	class Tile : GameObject
	{

		private bool canBeRandomized;
		private bool isWall;

		/// <summary>
		/// Places the tiles in an area randomly
		/// </summary>
		public void Randomized()
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
