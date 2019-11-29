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

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Enemy(Vector2 position)
        {
            this.position = position;
            sprite = Assets.EnemySprite;
            ChangeSprite(Assets.EnemySprite);
            rotation = Helper.CalculateAngleBetweenPositions(position, GameWorld.Player.Position);
            initialize();
        }

        public Enemy(Vector2 position, bool isBoss)
        {
            this.position = position;
            this.isBoss = isBoss;
            if (isBoss)
            {
                sprite = Assets.BossSprite;
            }
            else
            {
                sprite = Assets.EnemySprite;

            }
            initialize();
        }

        private void initialize()
        {
            movementSpeed = 0.1f;
            if (isBoss)
            {
                health = 10;
            }
        }

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
            //throw new NotImplementedException();
            velocity = GameWorld.Player.Position - position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += velocity * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
    }
}
