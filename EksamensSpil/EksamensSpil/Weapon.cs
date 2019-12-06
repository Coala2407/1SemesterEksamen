using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EksamensSpil
{

    public enum ShootResult { Successfull, NotEnoughAmmo, CoolDownActive }

    public abstract class Weapon : GameObject
    {

        protected float attackSpeed;
        protected float reloadSpeed;
        protected float cooldown;
        protected int ammo;
        protected int clipSize;
        protected bool canGunReload;
        protected GameObject holder;

        public GameObject Holder
        {
            get { return holder; }
            set { holder = value; }
        }

        public float ReloadSpeed
        {
            get { return reloadSpeed; }
            set { reloadSpeed = value; }
        }

        public float AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

        //public int ClipSize
        //{
        //    get { return clipSize; }
        //    set { clipSize = value; }
        //}

        public virtual ShootResult Attack(Vector2 targetCords)
        {

            if (ammo != 0 && cooldown <= 0)
            {
                return ShootResult.Successfull;
            }
            else
            {
                if (ammo <= 0 && cooldown <= 0)
                {
                    if (cooldown < 0 && canGunReload != true)
                    {
                        cooldown = reloadSpeed;

                    }
                    canGunReload = true;
                    return ShootResult.NotEnoughAmmo;
                }
                else
                {
                    return ShootResult.CoolDownActive;
                }
            }

        }

        public void Reload()
        {

            ammo = clipSize;
            canGunReload = false;

        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {

            }
        }

        public int Ammo
        {
            get
            {
                return ammo;
            }
            set
            {

            }
        }

        public int ClipSize
        {
            get
            {
                return clipSize;
            }
            set
            {
                clipSize = value;
            }
        }

        public override void OnCollision(GameObject otherObject)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

		// TODO: Meybe give Reload Timer it's own cooldown variable or make some other changes
		/// <summary>
		/// Adds a timer before the weapon reloads. The timer can be cancelled if gameObject(player) takes damage 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="otherObject"></param>
        public void ReloadCooldown(GameTime gameTime, GameObject otherObject)
        {

			Character character = otherObject as Character;
			Enemy enemy = otherObject as Enemy;

            if (character.takeDamage == true && holder != enemy)
            {
                Console.WriteLine("Reload Canceled");
				//cooldown = 0;
				canGunReload = false;
				character.takeDamage = false;
            }

            if (canGunReload == true && ammo < clipSize && cooldown <= 0)
            {
                Reload();
				canGunReload = false;
            }

            cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
