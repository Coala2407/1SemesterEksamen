using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
    class Enemy : Character
    {

        private Weapon weapon;
        private int lootDropChance;
        private bool isBoss;
        private float movementSpeed = 0.2f;
        Vector2 direction;
        const float stopThreshold = 300f;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Enemy(Vector2 position)
        {
            this.position = position;         
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
            movementSpeed = 0.1f;
            if (isBoss)
            {
                health = 100;
                ChangeSprite(Assets.BossSprite);
            }
            else
            {
                health = 2;
                ChangeSprite(Assets.EnemySprite);
            }
        }


        //TODO Do we need this?
        public override void Attack()
        {

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
            positionPreMove = position;
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

            spriteFlippedY = GameWorld.Player.PositionX < position.X;
        }

        public void Update(Player player)
        {
        }

        public override int UpdateHealth(int damage)
        {
            if (damage > 0)
            {
                Health -= damage;
            }
            else
            {
                Health += damage;
            }

            //HP 0, die
            if (Health == 0)
            {
                Die();
            }

            return Health;
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
