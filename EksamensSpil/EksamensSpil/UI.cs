using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    /// <summary>
    /// UI Elements in the game
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Default font
        /// </summary>
        private static SpriteFont UIDefault = Assets.UIDefault;

        /// <summary>
        /// Show ammo and clip for the player's selected pistol
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void AmmoAndClip(SpriteBatch spriteBatch)
        {
            Player player = GameWorld.Player;
            if (player.SelectedWeapon != null && player.SelectedWeapon is Pistol)
            {
                Vector2 pos = new Vector2(GameWorld.displayWidth - 350, GameWorld.displayHeight - 200);

                spriteBatch.DrawString(UIDefault, $"{player.SelectedWeapon.Ammo}/{player.SelectedWeapon.ClipSize}", pos, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.95f);
            }
        }

        /// <summary>
        /// Show the player's health
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Health(SpriteBatch spriteBatch)
        {
            Player player = GameWorld.Player;
            Vector2 pos = new Vector2(75, 75);
            spriteBatch.DrawString(UIDefault, $"{player.Health}", pos, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.95f);
        }

		/// <summary>
		/// Show the time left before the player can leave a boss room
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void Timer(SpriteBatch spriteBatch)
		{
			if(GameWorld.ActiveRoom == GameWorld.BossRoom)
			{
				foreach (GameObject gameObject in GameWorld.ActiveRoom.GameObjects)
				{
					if (gameObject is Door)
					{
						Door door = gameObject as Door;

						if (door.StartTimer == true && door.Timer > 0)
						{
							Vector2 pos = new Vector2(990, 75);
							spriteBatch.DrawString(UIDefault, $"{(int)door.Timer}", pos, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.95f);
						}
					}
				}
			}
			
		}
    }
}
