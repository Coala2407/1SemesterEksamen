using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
    abstract class Character : GameObject
    {

        protected float movementSpeed;
        protected int health;

        public abstract void Die();

        // This can be used by both Player and Enemy.
        public abstract void Attack();

        //TODO get & set metode?
        public abstract int UpdateHealth(int change);

        public virtual void Move()
        {

        }

        public abstract void Reload();

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
