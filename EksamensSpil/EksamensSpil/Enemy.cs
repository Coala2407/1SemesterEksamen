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
        }

        //TODO Do we need this?
        public override void Attack()
        {

        }

        public override void Die()
        {
            throw new NotImplementedException();
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
        }

        public override int UpdateHealth(int change)
        {
            throw new NotImplementedException();
        }
    }
}
