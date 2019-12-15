using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	public abstract class Item : GameObject
	{
        protected GameObject holder;
        public GameObject Holder
        {
            get { return holder; }
            set { holder = value; }
        }

        public override void OnCollision(GameObject otherObject)
        {
            
        }

        /// <summary>
        /// The effect the item has
        /// </summary>
        public abstract void ItemEffect();

        public override void Update(GameTime gameTime)
		{
			//throw new NotImplementedException();
		}
	}
}
