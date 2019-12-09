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
    class Wall : GameObject
    {

        private bool canBeRandomized;
        private bool canBeToggled;

        //Determines wether the wall is fixed, randomizable or toggleable. 
        public enum WallMode
        {
            Fixed = 0,
            Randomized = 1,
            Toggled = 2
        }

        public Wall(Vector2 position)
        {
            this.position = position;
            initialize();
        }

        public Wall(Vector2 position, bool isHidden)
        {
            this.position = position;
            if (isHidden)
            {
                this.IsHidden = true;
            }
            initialize();
        }

        public Wall(Vector2 position, bool isHidden, WallMode mode = WallMode.Fixed)
        {
            this.position = position;
            if (isHidden)
            {
                this.IsHidden = true;
            }
            if (mode == WallMode.Randomized)
            {
                canBeRandomized = true;
            }
            else if (mode == WallMode.Toggled)
            {
                canBeToggled = true;
            }
            initialize();
        }

        private void initialize()
        {
            sprite = Assets.WallSprites[0];
            drawLayer = 0.9f;
        }

        /// <summary>
        /// Randomizes tiles
        /// </summary>
        public void Randomize()
        {
            if (canBeRandomized)
            {
                int randomInt = GameWorld.rng.Next(0, 2);
                if (randomInt == 0)
                {
                    IsHidden = false;
                }
                else if (randomInt == 1)
                {
                    IsHidden = true;
                }
            }
            else if (canBeToggled)
            {
                if (IsHidden)
                {
                    IsHidden = false;
                }
                else
                {
                    IsHidden = true;
                }
            }
        }


        public override void OnCollision(GameObject otherObject)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
