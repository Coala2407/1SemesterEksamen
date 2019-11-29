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
            this.position = holder.Position;
            initialize();
        }

        private void initialize()
        {
            this.ammo = 6;
            this.attackSpeed = 0.20f;
            this.clipSize = 6;
            this.reloadSpeed = 0.70f;
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
                GameWorld.AddGameObject(new Projectile(this), GameWorld.ActiveRoom);
                return shootResult;
            }

            return shootResult;
        }

    }
}
