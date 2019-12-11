using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensSpil
{
    public abstract class Character : GameObject
    {
        /// <summary>
        ///Default: 500
        /// </summary>
        protected float movementSpeed = 500;
        /// <summary>
        /// Default: 1
        /// </summary>
        protected int health = 1;
        /// <summary>
        /// Default: .25f
        /// </summary>
        protected float invinsibilityTimeAfterDamage = .25f;
        /// <summary>
        /// Default: true
        /// </summary>
        protected bool isAlive = true;

        protected Vector2 velocity;
        protected Weapon selectedWeapon;
        protected Vector2 positionPreMove;
        protected float invinsibilityTimer;
        protected bool takeDamage;


        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }
            }
        }

        public bool TakeDamage
        {
            get { return takeDamage; }
            set { takeDamage = value; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set { selectedWeapon = value; }
        }

        /// <summary>
        /// Character dies
        /// </summary>
        public abstract void Die();

        /// <summary>
        /// Character attacks
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// Character reloads selected weapon
        /// </summary>
        public abstract void Reload();

        /// <summary>
        /// Update health of character
        /// </summary>
        /// <param name="damage">Damage the character loses. Negative numbers give the character health.</param>
        /// <returns></returns>
        public virtual int UpdateHealth(int damage)
        {
            if (invinsibilityTimer > invinsibilityTimeAfterDamage)
            {
                if (damage > 0)
                {
                    Health -= damage;
                }
                else
                {
                    Health += Math.Abs(damage);
                }

                //Reset timer, so they cant take damage again
                invinsibilityTimer = 0;

                //HP 0, die
                if (Health == 0)
                {
                    Die();
                }
            }

            return Health;
        }

        public virtual void Move(GameTime gameTime)
        {
            //deltaTime based on gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Moves the player based on velocity, speed, and deltaTime
            position += ((velocity * movementSpeed) * deltaTime);
        }

        public override void Update(GameTime gameTime)
        {

            positionPreMove = position;
            invinsibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public override void OnCollision(GameObject otherObject)
        {
            Wall wall = otherObject as Wall;
            Door door = otherObject as Door;
            if (wall != null && !wall.IsHidden)
            {
                //Touched wall. Move to previous position before collision
                position = positionPreMove;
            }
            if (door != null && door.IsOpen == false)
            {
                //Touched wall. Move to previous position before collision
                position = positionPreMove;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                base.Draw(spriteBatch);
                if (selectedWeapon != null)
                {
                    selectedWeapon.Draw(spriteBatch);
                }
            }
        }
    }
}
