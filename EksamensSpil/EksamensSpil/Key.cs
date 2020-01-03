using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
	public class Key : GameObject
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="position"></param>
		public Key(Vector2 position) : base(position)
		{
			this.position = position;
			ChangeSprite(Assets.DoorKey);
		}


		public override void OnCollision(GameObject otherObject)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			
		}
	}
}
