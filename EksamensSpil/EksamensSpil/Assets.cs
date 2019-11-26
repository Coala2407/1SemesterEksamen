using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
    public static class Assets
    {
        public static Texture2D PlayerSprite;
        public static Texture2D EnemySprite;
        public static Texture2D BossSprite;
        public static Texture2D ChestOpenSprite;
        public static Texture2D ChestClosedSprite;
        public static Texture2D TileGroundSprite;
        public static Texture2D TileWallSprite;
        public static Texture2D DoorOpenSprite;
        public static Texture2D DoorClosedSprite;

        public static void LoadAssets(ContentManager content)
        {
            PlayerSprite = content.Load<Texture2D>("characters");
            //EnemySprite = content.Load<Texture2D>("example2");
            //BossSprite = content.Load<Texture2D>("example3");
        }
    }
}
