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

        protected float drawLayer = 0.01f;

        protected float rotation;

        private float size = 1;

        //TODO: Placed name here to avoid repeating the same variable. 
        protected string name;

        protected Vector2 origin;

        protected Texture2D sprite;

        protected Texture2D[] sprites;

        protected Room room;

        protected bool spriteFlippedX;
        protected bool spriteFlippedY;

		protected bool isOpen;
        protected bool isHidden;

        public GameObject(Vector2 position)
        {
            this.position = position;
        }

        public GameObject() { }

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public bool SpriteFlippedX
        {
            get { return spriteFlippedX; }
            set { spriteFlippedX = value; }
        }

        public bool SpriteFlippedY
        {
            get { return spriteFlippedY; }
            set { spriteFlippedY = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
        }

        public float Size { get => size; set => size = value; }
        public bool IsHidden { get; set; }
		public bool IsOpen { get; set; }


        //Not really needed anymore.
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void OnCollision(GameObject otherObject);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null && !IsHidden)
            {
                SpriteEffects effect = SpriteEffects.None;
                if (spriteFlippedX)
                {
                    effect = SpriteEffects.FlipVertically;
                }
                else if (spriteFlippedY)
                {
                    effect = SpriteEffects.FlipHorizontally;

                }
                spriteBatch.Draw(sprite, position, null, Color.White, MathHelper.ToRadians(rotation), origin, size, effect, drawLayer);
            }
        }

        public Rectangle GetCollisionBox()
        {
            if (sprite != null)
            {
                return new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, sprite.Width, sprite.Height);
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

        // Rotates a sprite towards a location
        public void LookAt(Vector2 positionToLookAt)
        {
            rotation = Helper.CalculateAngleBetweenPositions(this.position, positionToLookAt);
        }

        // sets sprites for GameObjects and defines the origin point. Can also be used to change gameObjects sprite in run time
        public void ChangeSprite(Texture2D sprite)
        {
            if (sprite != null)
            {
                this.sprite = sprite;
                this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            }
        }

		// Borrowed from another projekt
		public Vector2 ForwardVector
		{
			get
			{
				return new Vector2(
					Helper.Cos(this.rotation),
					Helper.Sin(this.rotation)
				);
			}
		}
    }
}
