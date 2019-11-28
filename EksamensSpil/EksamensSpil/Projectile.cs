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

		private float movementSpeed = 100;
        private Vector2 movement;

        public Projectile(Pistol pistol)
        {
            this.position = pistol.Position;
            movement = Crosshair.currentPosition - position;
            Console.WriteLine($"Spawn {position}");
            ChangeSprite(Assets.BulletSprite);
            rotation = Helper.CalculateAngleBetweenPositions(position, Crosshair.currentPosition);
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
