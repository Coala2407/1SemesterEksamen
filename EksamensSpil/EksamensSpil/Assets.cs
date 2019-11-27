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
        public static Texture2D[] ChestSprites;
        public static Texture2D[] WallSprites;
        public static Texture2D[] DoorSprites;
        public static Texture2D CrosshairSprite;

        public static void LoadAssets(ContentManager content)
        {
            PlayerSprite = content.Load<Texture2D>("archerguy");
            EnemySprite = content.Load<Texture2D>("kingguy");
            BossSprite = content.Load<Texture2D>("goldguy");
            CrosshairSprite = content.Load<Texture2D>("Corshair");
            WallSprites = new Texture2D[1];
            for (int i = 0; i < WallSprites.Length; i++)
            {
                WallSprites[i] = content.Load<Texture2D>($"wall{i}");
            }
            //EnemySprite = content.Load<Texture2D>("example2");
            //BossSprite = content.Load<Texture2D>("example3");
        }
    }
}
