﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
    public abstract class Character : GameObject
    {

        protected float movementSpeed = 500;
        protected int health = 1;
        protected Vector2 velocity;
        protected Weapon selectedWeapon;
        //Used for collisions on walls
        protected Vector2 positionPreMove;
        //Invinsibility frames
        protected float invisibilityTimer;
        protected float invisibilityTimeAfterDamage = 1f;

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

        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set { selectedWeapon = value; }
        }

        public abstract void Die();

        // This can be used by both Player and Enemy.
        public abstract void Attack();

        //TODO get & set metode?
        public virtual int UpdateHealth(int damage)
        {
            if (invisibilityTimer > invisibilityTimeAfterDamage)
            {
                if (damage > 0)
                {
                    Health -= damage;
                }
                else
                {
                    Health += damage;
                }

                //Reset timer, so they cant take damage again
                invisibilityTimer = 0;

                //HP 0, die
                if (Health == 0)
                {
                    Die();
                }
            }
            return Health;
        }

        public override void Update(GameTime gameTime)
        {
            positionPreMove = position;
            invisibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Move(GameTime gameTime)
        {
            //deltaTime based on gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Moves the player based on velocity, speed, and deltaTime
            position += ((velocity * movementSpeed) * deltaTime);
        }

        public abstract void Reload();

        // This can be used for Player and Enemy
        public override void OnCollision(GameObject otherObject)
        {
            Wall wall = otherObject as Wall;
            if (wall != null && !wall.IsHidden)
            {
                //Touched wall. Move to previous position before collision
                position = positionPreMove;
            }
        }
    }
}
