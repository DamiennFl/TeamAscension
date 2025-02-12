using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension
{
    public class Bullet
    {

        public int Damage { get; set; }

        public float Speed { get; set; }

        public Texture2D BulletTexture { get; set; }

        public Vector2 BulletPosition { get; set; }

        public bool IsActive { get; set; }

        public Bullet()
        {
            
        }

        public void BulletUpdate(GameTime gameTime)
        {
            this.BulletPosition += new Vector2(Speed, 0);
        }

        public void BulletDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.BulletTexture, this.BulletPosition, Color.White);
        }

        public void BulletMovement(float updatedBulletSpeed)
        {
            this.BulletPosition += new Vector2(updatedBulletSpeed, 0);
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
               this.IsActive = true;
               for (int i = 0; i < 10; i++)
               {
                    //this.BulletPosition += 1;
               }
            }


        }
    }
}
