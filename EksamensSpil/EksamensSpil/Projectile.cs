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
        Weapon weapon;
        Enemy enemy;
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


        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (this.shooter.GetType() == otherObject.GetType())
            {
                return;
            }
            else
            {
                Character c = otherObject as Character;
                if (c != null)
                {
                    c.UpdateHealth(1);
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
        }

        public override void Update(GameTime gameTime)
        {
            //Normalizes movement of the bullet
            if (movement != Vector2.Zero)
            {
                movement.Normalize();
            }
            //Gives the bullet movement
            position += movement * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
