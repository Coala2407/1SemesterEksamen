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

        private float movementSpeed;
        private Vector2 targetCords;
        private Vector2 movement;
        private Pistol pistol;
        private GameObject shooter;

        public Projectile(Pistol pistol, float movementSpeed, Vector2 targetCords)
        {
            this.pistol = pistol;
            this.movementSpeed = movementSpeed;
            this.targetCords = targetCords;
            position = pistol.Position;
            shooter = pistol.Holder;
            movement = targetCords - position;
            ChangeSprite(Assets.BulletSprite);
            rotation = Helper.CalculateAngleBetweenPositions(position, targetCords);
            drawLayer = 0.8f;
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (shooter == null)
            {
                return;
            }
            if (this.shooter.GetType() == otherObject.GetType())
            {
                return;
            }
            else
            {
                Character c = otherObject as Character;
                if (c != null && c.IsAlive)
                {
                    c.UpdateHealth(1);
                    c.TakeDamage = true;                
                    GameWorld.RemoveGameObject(this);
                }
            }

            //Projectile hit wall
            Wall w = otherObject as Wall;
            if (w != null)
            {
                if (!w.IsHidden)
                {
                    //Wall is visible.
                    GameWorld.RemoveGameObject(this);
                }
            }

            // Projectile hit closed door

            Door door = otherObject as Door;
            if (door != null && door.IsOpen == false)
            {
                GameWorld.RemoveGameObject(this);
            }
        }

		public void Move(GameTime gameTime)
		{
			//Normalizes movement of the bullet
			if (movement != Vector2.Zero)
			{
				movement.Normalize();
			}
			//Gives the bullet movement
			position += movement * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}

        public override void Update(GameTime gameTime)
        {
			Move(gameTime);
        }

	}
}
