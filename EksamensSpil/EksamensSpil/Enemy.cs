﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
    public class Enemy : Character
    {
        private int lootDropChance;
        private bool isBoss;
        private float stopThreshold = 400f;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Enemy(Vector2 position)
        {
            this.position = position;
            sprite = Assets.EnemySprite;
            initialize();
        }

        public Enemy(Vector2 position, bool isBoss)
        {
            this.position = position;
            this.isBoss = isBoss;
            initialize();
        }

        private void initialize()
        {
            selectedWeapon = new Pistol(this);
            GameWorld.AddGameObject(selectedWeapon, GameWorld.ActiveRoom);

            movementSpeed = 0.1f;
            if (isBoss)
            {
                health = 50;
                selectedWeapon.ReloadSpeed = 0f;
                selectedWeapon.AttackSpeed = .05f;
                selectedWeapon.Size *= 2;
                Pistol p = selectedWeapon as Pistol;
                if (p != null)
                {
                    p.ProjectileSpeed = 1200f;
                }
                stopThreshold = 1000f;
                ChangeSprite(Assets.BossSprite);
            }
            else
            {
                health = 2;
                ChangeSprite(Assets.EnemySprite);
            }
        }







        public override void Die()
        {
            GameWorld.RemoveGameObject(this);
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        //TODO Do we need this?
        public override void Reload()
        {

        }





        public override void Update(GameTime gameTime)
        {
            //Get from superclass
            base.Update(gameTime);
            velocity = GameWorld.Player.Position - position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += velocity * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Stops the enemy from moving once it reaches a certain threshold from the player
            float distance = Vector2.Distance(GameWorld.Player.Position, position);
            if (distance <= stopThreshold)
            {
                movementSpeed = 0f;
            }
            else
            {
                movementSpeed = 0.2f;
            }


            if (selectedWeapon != null)
            {
                selectedWeapon.ReloadCooldown(gameTime, this);
                selectedWeapon.PositionY = position.Y + 20;
                selectedWeapon.PositionX = position.X;
                // rotation (Look at player)
                // rotation (Look at mouse)
                selectedWeapon.LookAt(GameWorld.Player.Position);
                //Flip the weapon depending on crosshair weapon
                selectedWeapon.SpriteFlippedX = GameWorld.Player.PositionX < position.X;

            }
            Attack();
            spriteFlippedY = GameWorld.Player.PositionX < position.X;

        }
        public override void Attack()
        {
            if (movementSpeed == 0f)
            {
                selectedWeapon.Attack(GameWorld.Player.Position);
            }


        }

        public void Update(Player player)
        {
        }


        public override void OnCollision(GameObject otherObject)
        {
            base.OnCollision(otherObject);

            Enemy enemy = otherObject as Enemy;
            if (enemy != null && enemy != this)
            {
                position = positionPreMove;
            }
        }
    }
}
