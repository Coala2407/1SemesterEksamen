using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensSpil
{
	class Projectile : GameObject
	{

		private float movementSpeed = 500;
		private Vector2 targetCordinater;
        private Vector2 movement = Crosshair.currentPosition - GameWorld.player.Position;

        public Projectile(Vector2 position)
        {
            Console.WriteLine($"Spawn {position}");
            //this.sprite = sprite;
            ChangeSprite(Assets.BulletSprite);
            drawLayer = 0.8f;
        }

        public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}

        public override void OnCollision(GameObject otherObject)
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
            //Normalizes movement of the bullet, ensuring it moves in one direction
            if (movement != Vector2.Zero)
            {
                movement.Normalize();
            }
            //Gives the bullet movement
            position += movement * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
	}
}
