using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

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
        public Texture2D backGround;

        /// <summary>
        /// sprite batch.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// border texture.
        /// </summary>
        private Texture2D borderTexture;

        /// <summary>
        /// graphics device.
        /// </summary>
        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// Content manager.
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Gets the Border width.
        /// </summary>
        public int BorderWidth
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// List of rectangles.
        /// </summary>
        private List<Rectangle> spawnAreaRectangles;

        /// <summary>
        /// Gets the list of rectangles.
        /// </summary>
        public List<Rectangle> SpawnAreaRectangles
        {
            get
            {
                return this.spawnAreaRectangles;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayArea"/> class.
        /// </summary>
        /// <param name="graphicsDevice">graphics device.</param>
        /// <param name="contentManager">content manager.</param>
        public PlayArea(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
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

        /// <summary>
        /// This draws the spawn rectangles.
        /// </summary>
        /// <param name="spriteBatch">sprite batch.</param>
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

        /// <summary>
        /// This will white out the screen given an event.
        /// </summary>
        public async void WhiteOutScreen()
        {
            this.backGround = new Texture2D(this.graphicsDevice, 1, 1);
            this.backGround.SetData(new[] { Color.White });
            await Task.Delay(135);
            this.backGround = this.contentManager.Load<Texture2D>("Backgrounds/Stage1");
        }

        /// <summary>
        /// Initializes the spawn rectangles.
        /// </summary>
        private void InitializeSpawnRectangles()
        {
            Rectangle leftRectangle = new Rectangle(this.BorderRectangle.X - 60, this.BorderRectangle.Y, 40, this.BorderRectangle.Height / 3);
            Rectangle rightRectangle = new Rectangle(this.BorderRectangle.Width + 60, this.BorderRectangle.Y, 40, this.BorderRectangle.Height / 3);
            this.spawnAreaRectangles.Add(leftRectangle);
            this.spawnAreaRectangles.Add(rightRectangle);
        }
    }
}
