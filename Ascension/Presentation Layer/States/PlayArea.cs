using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Ascension
{
    public class PlayArea
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
        private Color BorderColor { get; set; } = Color.Red;

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

        private List<Rectangle> spawnAreaRectangles;

        public List<Rectangle> SpawnAreaRectangles
        {
            get
            {
                return this.spawnAreaRectangles;
            }
        }

        public PlayArea(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.backGround = contentManager.Load<Texture2D>("Backgrounds/Stage1");
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.AliceBlue });
            this.spawnAreaRectangles = new List<Rectangle>();
            this.InitializeSpawnRectangles();
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

        public void DrawSpawnRectangles(SpriteBatch spriteBatch)
        {
            foreach (Rectangle rectangle in this.spawnAreaRectangles)
            {
                spriteBatch.Draw(this.borderTexture, rectangle, Color.White);
            }
        }

        /// <summary>
        /// Draws the border for us.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderDraw(SpriteBatch spriteBatch)
        {
            // Top part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y, this.BorderRectangle.Width, this.BorderWidth), Color.Red);

            // Bottom part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y + this.BorderRectangle.Height - this.BorderWidth, this.BorderRectangle.Width, this.BorderWidth), Color.Red);

            // Left part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y, this.BorderWidth, this.BorderRectangle.Height), Color.Red);

            // Right part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.BorderRectangle.X + this.BorderRectangle.Width - this.BorderWidth, this.BorderRectangle.Y, this.BorderWidth, this.BorderRectangle.Height), Color.Red);
        }

        private void InitializeSpawnRectangles()
        {
            Rectangle topRectangle = new Rectangle(this.BorderRectangle.X, this.BorderRectangle.Y - 60, this.BorderRectangle.Width, 40);
            Rectangle leftRectangle = new Rectangle(this.BorderRectangle.X - 60, this.BorderRectangle.Y, 40, this.BorderRectangle.Height / 3);
            Rectangle rightRectangle = new Rectangle(this.BorderRectangle.Width + 60, this.BorderRectangle.Y, 40, this.BorderRectangle.Height / 3);
            this.spawnAreaRectangles.Add(topRectangle);
            this.spawnAreaRectangles.Add(leftRectangle);
            this.spawnAreaRectangles.Add(rightRectangle);
        }
    }
}
