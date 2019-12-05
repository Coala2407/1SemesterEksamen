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
        Weapon weapon;
        Enemy enemy;
        private GameObject shooter;

        public Projectile(Pistol pistol, float movementSpeed)
        {
            shooter = pistol.Holder;

            foreach (GameObject gameObject in GameWorld.ActiveRoom.GameObjects)
            {
                if (GameWorld.Player.SelectedWeapon != null)
                {
                    if (GameWorld.Player.SelectedWeapon.Holder != null)
                    {
                        this.position = pistol.Position;
                        this.movementSpeed = movementSpeed;
                        movement = Crosshair.currentPosition - GameWorld.Player.Position;
                        Console.WriteLine($"Spawn {position}");
                        ChangeSprite(Assets.BulletSprite);
                        rotation = Helper.CalculateAngleBetweenPositions(position, Crosshair.currentPosition);
                        drawLayer = 0.8f;
                    }
                }

                Enemy enemy = gameObject as Enemy;
                if (enemy != null)
                {

                    if (enemy.SelectedEnemyWeapon.Holder != null)
                    {
                        this.position = pistol.Position;
                        this.movementSpeed = movementSpeed;
                        movement = GameWorld.Player.Position - pistol.Position;
                        Console.WriteLine($"Spawn {position}");
                        ChangeSprite(Assets.BulletSprite);
                        rotation = Helper.CalculateAngleBetweenPositions(position, GameWorld.Player.Position);
                        drawLayer = 0.82f;
                    }
                }
            }


        }

        
        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject otherObject)
        {
            Wall wall = otherObject as Wall;
            Enemy enemy = otherObject as Enemy;
            Enemy shooterEnemy = otherObject as Enemy;
            if(shooterEnemy != null && shooterEnemy == this.shooter)
            {
                return;
            }
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
            /*if(GameWorld.Player.SelectedWeapon != null)
            {

              if (enemy != null && GameWorld.Player.SelectedWeapon.Holder == GameWorld.Player)
              {
                GameWorld.RemoveGameObject(this);
                //Damage enemy
                enemy.UpdateHealth(1);
              }

              if(enemy != null)
              {
                    if (GameWorld.Player != null && enemy.SelectedEnemyWeapon.Holder == enemy)
                    {
                        GameWorld.RemoveGameObject(this);
                        //Damage player
                        //GameWorld.Player.UpdateHealth(1);
                    }
              }
            
            }*/

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
