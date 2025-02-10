// <copyright file="Button.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Ascension.Content.Controls
{
    /// <summary>
    /// The button.
    /// </summary>
    internal class Button : Components
    {
        /// <summary>
        /// The font used for the button text.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// The texture of the button.
        /// </summary>
        private readonly Texture2D texture;

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        private MouseState currentMouse;

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        private MouseState previousMouse;

        /// <summary>
        /// A value indicating whether the mouse is hovering over the button.
        /// </summary>
        private bool isHovering;

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Gets a value indicating whether the button is clicked.
        /// </summary>
        public bool IsClicked { get; private set; }

        /// <summary>
        /// Gets or sets the color of the pen.
        /// </summary>
        public Color PenColor { get; set; }

        /// <summary>
        /// Gets or sets the position of the button.
        /// </summary>
        public Vector2 Pos { get; set; }

        /// <summary>
        /// Gets the rectangle representing the button's bounds.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)this.Pos.X, (int)this.Pos.Y, this.texture.Width, this.texture.Height);
            }
        }

        /// <summary>
        /// Gets or sets the text displayed on the button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="texture">The texture of the button.</param>
        /// <param name="font">The font of the button.</param>
        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
            this.PenColor = Color.Black;
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="spriteBatch">The sprite batch used to draw the button.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (this.isHovering)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(this.texture, this.Rectangle, color);

            if (!string.IsNullOrEmpty(this.Text))
            {
                var x = (this.Rectangle.X + (this.Rectangle.Width / 2)) - (this.font.MeasureString(this.Text).X / 2);
                var y = (this.Rectangle.Y + (this.Rectangle.Height / 2)) - (this.font.MeasureString(this.Text).Y / 2);

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.PenColor);
            }
        }

        /// <summary>
        /// Updates the button state.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.previousMouse = this.currentMouse;

            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(this.currentMouse.X, this.currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(this.Rectangle))
            {
                this.isHovering = true;

                if (this.currentMouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
                {
                    this.Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
