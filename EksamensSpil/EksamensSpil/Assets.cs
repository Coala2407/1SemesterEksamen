﻿using Microsoft.Xna.Framework.Content;
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
        //Sprites
        public static Texture2D PlayerSprite;
        public static Texture2D EnemySprite;
        public static Texture2D BossSprite;
        public static Texture2D[] ChestSprites;
        public static Texture2D[] WallSprites;
        public static Texture2D[] DoorSprites;
        public static Texture2D CrosshairSprite;
        public static Texture2D BulletSprite;
        public static Texture2D PistolSprite;
        public static Texture2D SwordSprite;
        public static Texture2D SwingEffectSprite;
        public static Texture2D JewelSprite;
        public static Texture2D Background1;
		public static Texture2D DoorKey;
        //UI Elements
        public static SpriteFont UIDefault;

        /// <summary>
        /// Load all game assets
        /// </summary>
        /// <param name="content"></param>
        public static void LoadAssets(ContentManager content)
        {
            PlayerSprite = content.Load<Texture2D>("archerguy");
            EnemySprite = content.Load<Texture2D>("kingguy");
            BossSprite = content.Load<Texture2D>("goldguy");
            CrosshairSprite = content.Load<Texture2D>("Corshair");
            BulletSprite = content.Load<Texture2D>("Bullet");
            PistolSprite = content.Load<Texture2D>("pistol");
            SwordSprite = content.Load<Texture2D>("sword1");
            SwingEffectSprite = content.Load<Texture2D>("swoosh");
            JewelSprite = content.Load<Texture2D>("Jewel");
			DoorKey = content.Load<Texture2D>("Key");
            Background1 = content.Load<Texture2D>("background1");

			DoorSprites = new Texture2D[3];
			for (int i = 0; i < DoorSprites.Length; ++i)
			{
				DoorSprites[i] = content.Load<Texture2D>($"door{i}");
			}
			ChestSprites = new Texture2D[2];
			for (int i = 0; i < ChestSprites.Length; ++i)
			{
				ChestSprites[i] = content.Load<Texture2D>($"chest{i}");
			}
            WallSprites = new Texture2D[1];
            for (int i = 0; i < WallSprites.Length; i++)
            {
                WallSprites[i] = content.Load<Texture2D>($"wall{i}");
            }

            //UI Elements
            UIDefault = content.Load<SpriteFont>("UIDefault");
        }
    }
}
