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
        private Vector2 movement;

        public Projectile(Pistol pistol, float movementSpeed)
        {
            this.position = pistol.Position;
            this.movementSpeed = movementSpeed;
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
            Wall wall = otherObject as Wall;
            Enemy enemy = otherObject as Enemy;
            //Projectile hit wall
            if (wall != null)
            {
                if (!wall.IsHidden)
                {
                    //Wall is visible.
                    GameWorld.RemoveGameObject(this);
                }
            }
            //Projectile hit enemy
            if (enemy != null)
            {
                GameWorld.RemoveGameObject(this);
                //Damage enemy
                enemy.UpdateHealth(1);
            }

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
