using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    class Crosshair : GameObject
    {
        public Vector2 Position
        {
            get { return position; }
        }

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
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
