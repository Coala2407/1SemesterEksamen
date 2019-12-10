using System;
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
        private int lootDropChance = 15;
        private bool isBoss;
        private float stopThreshold = 600f;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Enemy(Vector2 position)
        {
            this.position = position;
            sprite = Assets.EnemySprite;
            Initialize();
        }

        public Enemy(Vector2 position, bool isBoss)
        {
            this.position = position;
            this.isBoss = isBoss;
            Initialize();
        }

        public void Initialize()
        {
            selectedWeapon = new Pistol(this);
            //GameWorld.AddGameObject(selectedWeapon, GameWorld.ActiveRoom);
            movementSpeed = 0.1f;
            if (isBoss)
            {
                health = 20;
                selectedWeapon.ReloadSpeed = 1f;
                selectedWeapon.AttackSpeed = .1f;
                selectedWeapon.Size *= 2;
                selectedWeapon.ClipSize = 25;
                Pistol p = selectedWeapon as Pistol;
                if (p != null)
                {
                    p.ProjectileSpeed = 2800f;
                }
                stopThreshold = 1000f;
                lootDropChance = 5;
                ChangeSprite(Assets.BossSprite);
                drawLayer = .05f;
            }
            else
            {
                health = 2;
                drawLayer = .06f;
                ChangeSprite(Assets.EnemySprite);
            }
        }


        public override void Die()
        {
            int rng = GameWorld.rng.Next(1, 101);
            if (rng <= lootDropChance)
            {
                GameWorld.AddGameObject(selectedWeapon, GameWorld.ActiveRoom);
                selectedWeapon.Holder = null;
            }
            //GameWorld.RemoveGameObject(this);
            isAlive = false;
            foreach (Room room in GameWorld.Level.Rooms)
            {
                if (room != GameWorld.ActiveRoom)
                {
                    room.RandomizeWalls();
                }
            }
        }

        //TODO Do we need this?
        public override void Reload()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                return;
            }
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

        public override void OnCollision(GameObject otherObject)
        {
            base.OnCollision(otherObject);

            Enemy enemy = otherObject as Enemy;
            if (enemy != null && enemy != this && !enemy.isBoss)
            {
                position = positionPreMove;
            }
        }
    }
}
