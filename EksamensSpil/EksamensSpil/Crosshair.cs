using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    class Crosshair : GameObject
    {

        public static Vector2 currentPosition;
        public Crosshair()
        {
            drawLayer = 1;
            ChangeSprite(Assets.CrosshairSprite);
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //Updates the current position of the mouse
            MouseState currentMouseState = Mouse.GetState();
            position = new Vector2(currentMouseState.X, currentMouseState.Y);
            currentPosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            //Flip the players selected weapon
            if (GameWorld.player.SelectedWeapon != null)
            {
                GameWorld.player.SelectedWeapon.SpriteFlippedY = currentPosition.X < GameWorld.player.Position.X;
            }
        }

        public override void OnCollision(GameObject otherObject)
        {

        }
    }
}
