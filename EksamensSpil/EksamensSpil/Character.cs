using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    abstract class Character : GameObject
    {
        protected float movementSpeed;
        protected int health;


        public abstract void Die();
        public abstract void Attack();
        public abstract int UpdateHealth(int change);
        public abstract void Move();
        public abstract void Reload();

    }
}
