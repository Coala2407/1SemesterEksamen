using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensSpil
{
    class JewelItem : Item
    {
        public JewelItem(Vector2 position)
        {
            this.position = position;
            ChangeSprite(Assets.JewelSprite);
            initialize();
        }

        public JewelItem(GameObject holder)
        {

            this.holder = holder;         
            initialize();
        }

        private void initialize()
        {
            
            drawLayer = 0.21f;
            if (holder != null)
            {
                position = holder.Position;
            }         
        }

        public override void ItemEffect()
        {        
            GameWorld.Player.Health += 2;     
        }

        public override void Update(GameTime gameTime)
        {
            //ItemEffect(otherObject);
        }
    }
}
