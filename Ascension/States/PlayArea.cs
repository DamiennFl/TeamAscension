using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.States
{
    internal class PlayArea
    {
        /// <summary>
        /// Gets the Border Rectangle.
        /// </summary>
        public Rectangle BorderRectangle
        {
            get
            {
                return new Rectangle(40, 40, 460, 720);
            }
        }

        /// <summary>
        /// Gets or sets the Border Color.
        /// </summary>
        private Color BorderColor { get; set; } = Color.Black;

        /// <summary>
        /// Background texture.
        /// </summary>
        private Texture2D backGround;

        private SpriteBatch spriteBatch;

        private Texture2D borderTexture;

        public int BorderWidth
        {
            get
            {
                return 4;
            }
        }


        public PlayArea(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.backGround = content.Load<Texture2D>("Backgrounds/Stage1");
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.AliceBlue });
        }


        /// <summary>
        /// Draws the background for us.
        /// </summary>
        /// <param name="spriteBatch">Our spriteBatch.</param>
        public void DrawBackground(SpriteBatch spriteBatch)
        {
            int adjustedX = this.BorderRectangle.X + this.BorderWidth;
            int adjustedY = this.BorderRectangle.Y + this.BorderWidth;
            int adjustedWidth = this.BorderRectangle.Width - (2 * this.BorderWidth);
            int adjustedHeight = this.BorderRectangle.Height - (2 * this.BorderWidth);
            Rectangle rect = new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight);
            spriteBatch.Draw(this.backGround, rect, Color.White);
        }

        /// <summary>
        /// Draws the border for us.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderDraw(SpriteBatch spriteBatch)
        {
            // Top part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y, this.BorderRectangle.Width, this.BorderWidth), this.BorderColor);

            // Bottom part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y + this.BorderRectangle.Height - this.BorderWidth, this.BorderRectangle.Width, this.BorderWidth), this.BorderColor);

            // Left part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y, this.BorderWidth, this.BorderRectangle.Height), this.BorderColor);

            // Right part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X + this.BorderRectangle.Width - this.BorderWidth, this.BorderRectangle.Y, this.BorderWidth, this.BorderRectangle.Height), this.BorderColor);
        }

        /// <summary>
        /// Draw a buffer for enemies. to spawn in.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderBuffer(SpriteBatch spriteBatch)
        {
            // Top part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X - 40, this.BorderRectangle.Y - 40, this.BorderRectangle.Width + 90, this.BorderWidth + 40), this.BorderColor);

            // Bottom part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X - 30, this.BorderRectangle.Y + this.BorderRectangle.Height - this.BorderWidth, this.BorderRectangle.Width + 30, this.BorderWidth + 40), this.BorderColor);

            // Left part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X - 60, this.BorderRectangle.Y - 30, this.BorderWidth + 60, this.BorderRectangle.Height + 60), this.BorderColor);

            // Right part of rectagnle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X + this.BorderRectangle.Width - this.BorderWidth, this.BorderRectangle.Y - 30, this.BorderWidth + 300, this.BorderRectangle.Height + 30), this.BorderColor);
        }
    }
}
