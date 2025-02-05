// <copyright file="Game1.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:XmlCommentAnalysisDisabled", Justification = "Documentation not required.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Formatting not required.")]

    /// <summary>
    /// Game1 class.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// The graphics device manager.
        /// </summary>
        private Texture2D ballTexture;

        /// <summary>
        /// The ball position.
        /// </summary>
        private Vector2 ballPosition;

        /// <summary>
        /// The border rectangle.
        /// </summary>
        private Rectangle borderRect = new Rectangle(40, 40, 460, 720);

        /// <summary>
        /// The border width.
        /// </summary>
        private int borderWidth = 5;

        /// <summary>
        /// The border color.
        /// </summary>
        private Color borderColor = Color.Black;

        /// <summary>
        /// The ball speed.
        /// </summary>
        private float ballSpeed;

        /// <summary>
        /// The border texture.
        /// </summary>
        private Texture2D borderTexture;

        /// <summary>
        /// The graphics device manager.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// The sprite batch.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 800;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.ballPosition = new Vector2(this.graphics.PreferredBackBufferWidth / 4, this.graphics.PreferredBackBufferHeight / 2);
            this.ballSpeed = 100f;
            this.borderTexture = new Texture2D(this.GraphicsDevice, 100, 100);

            base.Initialize();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.ballTexture = this.Content.Load<Texture2D>("ball");
            this.borderTexture = new Texture2D(this.GraphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        /// <param name="gameTime">Time of update.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                                 Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // TODO: Add your update logic here

            // The time since Update was called last.
            float updatedBallSpeed = this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            this.BallMovement(updatedBallSpeed);
            this.StayInBorder();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);

            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderRect.Width, this.borderWidth), this.borderColor);
            this.spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y + this.borderRect.Height - this.borderWidth, this.borderRect.Width, this.borderWidth), this.borderColor);
            this.spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);
            this.spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X + this.borderRect.Width - this.borderWidth, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);

            this.spriteBatch.Draw(
                this.ballTexture,
                this.ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(this.ballTexture.Width / 4, this.ballTexture.Height / 4),
                new Vector2(0.25F, 0.25F),
                SpriteEffects.None,
                0f);

            // TODO: Add your drawing code here
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Moves the ball.
        /// </summary>
        /// <param name="updatedBallSpeed">Updated ball speed.</param>
        private void BallMovement(float updatedBallSpeed)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                this.ballPosition.Y -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                this.ballPosition.Y += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                this.ballPosition.X -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                this.ballPosition.X += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                this.ballSpeed = 150f;
            }
            else
            {
                this.ballSpeed = 100f;
            }
        }

        /// <summary>
        /// Keeps the ball in the border.
        /// </summary>
        private void StayInBorder()
        {
            int xPadding = this.graphics.PreferredBackBufferWidth / 25;
            int yPadding = this.graphics.PreferredBackBufferHeight / 20;

            if (this.ballPosition.X > (this.graphics.PreferredBackBufferWidth / 2) - (this.ballTexture.Width / 2))
            {
                this.ballPosition.X = (this.graphics.PreferredBackBufferWidth / 2) - (this.ballTexture.Width / 2);
            }
            else if (this.ballPosition.X < ((this.ballTexture.Width / 2) + xPadding))
            {
                this.ballPosition.X = (this.ballTexture.Width / 2) + xPadding;
            }

            if (this.ballPosition.Y > this.graphics.PreferredBackBufferHeight - (this.ballTexture.Height / 2) - yPadding)
            {
                this.ballPosition.Y = this.graphics.PreferredBackBufferHeight - (this.ballTexture.Height / 2) - yPadding;
            }
            else if (this.ballPosition.Y < ((this.ballTexture.Height / 2) + yPadding))
            {
                this.ballPosition.Y = (this.ballTexture.Height / 2) + yPadding;
            }
        }
    }
}