using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    public static class UI
    {
        private static SpriteFont UIDefault = Assets.UIDefault;

        public static void AmmoAndClip(SpriteBatch spriteBatch)
        {
            Player player = GameWorld.Player;
            if (player.SelectedWeapon != null && player.SelectedWeapon is Pistol)
            {
                Vector2 pos = new Vector2(GameWorld.displayWidth - 250, GameWorld.displayHeight - 200);

                spriteBatch.DrawString(UIDefault, $"{player.SelectedWeapon.Ammo}/{player.SelectedWeapon.ClipSize}", pos, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.95f);
            }
        }

        public static void Health(SpriteBatch spriteBatch)
        {
            Player player = GameWorld.Player;
            Vector2 pos = new Vector2(75, 75);
            spriteBatch.DrawString(UIDefault, $"{player.Health}", pos, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.95f);
        }
    }
}
