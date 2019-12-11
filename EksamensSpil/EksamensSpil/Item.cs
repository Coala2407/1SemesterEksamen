using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	public class Item : GameObject
	{
        protected int healthChange;
        protected float reloadSpeedChange;
        protected float attackSpeedChange;
        protected float cooldownChange;
        protected int clipSizeChange;
        protected GameObject holder;

        public GameObject Holder
        {
            get { return holder; }
            set { holder = value; }
        }

        //Different stat changes for the user of the item
        public float ReloadSpeedChange
        {
            get { return reloadSpeedChange; }
            set { reloadSpeedChange = value; }
        }

        public float AttackSpeedChange
        {
            get { return attackSpeedChange; }
            set { attackSpeedChange = value; }
        }

        public float CooldownChange
        {
            get { return cooldownChange; }
            set { cooldownChange = value; }
        }

        public int ClipSizeChange
        {
            get { return clipSizeChange; }
            set { }
        }

        /// <summary>
        /// Use the item
        /// </summary>
		public void Use()
		{
		}

        public override void OnCollision(GameObject otherObject)
        {
            
        }

        /// <summary>
        /// The effect the item has
        /// </summary>
        public virtual void ItemEffect()
        {

        }

        public override void Update(GameTime gameTime)
		{
			//throw new NotImplementedException();
		}
	}
}
