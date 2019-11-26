using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    public abstract class GameObject
    {
        protected Vector2 position;

        protected float drawLayer;

		//TODO Placed name here to avoid repeating the same variable. 
		protected string name;

		protected Vector2 origin;

        protected Texture2D sprite;

        protected Texture2D[] sprites;

        protected Room room;

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void OnCollision(GameObject otherObject);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, drawLayer);
            }
        }

        public Rectangle GetCollisionBox()
        {
            if (sprite != null)
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
            else
            {
                return new Rectangle();
            }
        }

        public virtual void CheckCollision(GameObject otherObject)
        {
            if (GetCollisionBox().Intersects(otherObject.GetCollisionBox()))
            {
                OnCollision(otherObject);
            }
        }
    }
}
