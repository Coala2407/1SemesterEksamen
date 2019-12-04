using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensSpil
{
    class Pistol : Weapon
    {

        private float projectileSpeed;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        public Pistol(Vector2 position)
        {
            this.position = position;
            initialize();
        }

        public Pistol(GameObject holder)
        {
            this.holder = holder;
            initialize();
        }

        private void initialize()
        {
            int randomAmmo = GameWorld.rng.Next(1, 11);
            this.ammo = randomAmmo;
            this.attackSpeed = (float)GameWorld.rng.Next(30, 81) / 100;
            this.clipSize = randomAmmo;
            this.reloadSpeed = (float)GameWorld.rng.Next(30, 81) / 100;
            this.projectileSpeed = GameWorld.rng.Next(250, 801);
            drawLayer = 0.2f;
            if (holder != null)
            {
                position = holder.Position;
            }
            ChangeSprite(Assets.PistolSprite);
        }

        public override ShootResult Attack()
        {

            ShootResult shootResult = base.Attack();

            if (shootResult == ShootResult.Successfull)
            {
                ammo--;
                cooldown = attackSpeed;
                Console.WriteLine($"Shoot pistol: {ammo}");
                GameWorld.AddGameObject(new Projectile(this, projectileSpeed), GameWorld.ActiveRoom);
                return shootResult;
            }
            return shootResult;
        }

    }
}
