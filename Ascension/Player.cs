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
    public class Player
    {
        public int Health { get; set; }

        private Texture2D playerTexture;

        protected Vector2 playerPosition;

        public float playerSpeed;

        public Player(Texture2D texture, Vector2 position, float speed)
        {
            this.playerTexture = texture;
            this.playerPosition = position;
            this.playerSpeed = speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.playerTexture, this.playerPosition, null,
                Color.White,
                0f,
                new Vector2(this.playerTexture.Width / 4, this.playerTexture.Height / 4),
                new Vector2(0.25F, 0.25F),
                SpriteEffects.None,
                0f);
        }

        public void PlayerMovement(float updatedPlayerSpeed)
        {
            var kstate = Keyboard.GetState();

            // Vector to normalize diagonal movement
            Vector2 dir = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.W))
            {
                dir.Y -= 1;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                dir.Y += 1;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                dir.X -= 1;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                dir.X += 1;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                this.playerSpeed = 120f;
            }
            else
            {
                this.playerSpeed = 60f;
            }

            // If Vector has values, normalize movement.
            if (dir != Vector2.Zero)
            {
                dir.Normalize();
            }

            // Update position after normalizing
            this.playerPosition += dir * this.playerSpeed;
        }

        public void StayInBorder(Rectangle screenBounds, int borderWidth)
        {
            int radius = this.playerTexture.Width / 8;

            // Left border
            if (this.playerPosition.X - radius < screenBounds.X)
            {
                this.playerPosition.X = screenBounds.X + radius;
            }

            // Right border
            else if (this.playerPosition.X + radius > screenBounds.X + screenBounds.Width - (2 * borderWidth))
            {
                this.playerPosition.X = screenBounds.X + screenBounds.Width - (2 * borderWidth) - radius;
            }

            // Top border
            if (this.playerPosition.Y - radius < screenBounds.Y)
            {
                this.playerPosition.Y = screenBounds.Y + radius;
            }

            // Bottom border
            else if (this.playerPosition.Y + radius > screenBounds.Y + screenBounds.Height - (2 * borderWidth))
            {
                this.playerPosition.Y = screenBounds.Y + screenBounds.Height - (2 * borderWidth) - radius;
            }
        }
    }
}
