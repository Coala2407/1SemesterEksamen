using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
	class Character : GameObject
	{

		protected float movementSpeed;
		protected int health;


		//TODO bool meybe?
		public void Die()
		{

		}

		// This can be used by both Player and Enemy.
		public void Attack()
		{

		}

		//TODO get & set metode?
		public int UpdateHealth(int change)
		{
			return health;
		}

		public void Move()
		{

		}

		//TODO meybe keep this method in here?
		public void Reload()
		{

		}

		// This can be used for Player and Enemy
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
