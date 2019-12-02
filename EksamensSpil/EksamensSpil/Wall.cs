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
        private bool isHidden;

        public bool IsHidden
        {
            get { return isHidden; }
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
                this.isHidden = true;
            }
            initialize();
        }

        public Wall(Vector2 position, bool isHidden, WallMode mode = WallMode.Fixed)
        {
            this.position = position;
            if (isHidden)
            {
                this.isHidden = true;
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
            sprites = Assets.WallSprites;
            sprite = sprites[0];
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
                    isHidden = false;
                }
                else if (randomInt == 1)
                {
                    isHidden = true;
                }
            }
            else if (canBeToggled)
            {
                if (isHidden)
                {
                    isHidden = false;
                }
                else
                {
                    isHidden = true;
                }
            }
        }

        //Determines wether the wall is fixed, randomizable or toggleable. 
        public enum WallMode
        {
            Fixed = 0,
            Randomized = 1,
            Toggled = 2
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isHidden)
            {
                base.Draw(spriteBatch);
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
    }
}
