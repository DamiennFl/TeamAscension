// <copyright file="GameOverState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// GameOverState class that handles the game over screen.
    /// </summary>
    internal class GameOverState : State
    {
        /// <summary>
        /// The font used for the game over screen.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverState"/> class.
        /// </summary>
        /// <param name="game">Game.</param>
        /// <param name="graphicsDevice">Graphics device./param>
        /// <param name="contentManager">Content manager.</param>
        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            this.font = this.contentManager.Load<SpriteFont>("Fonts/Font");
        }

        /// <summary>
        /// Draws the game over screen.
        /// </summary>
        /// <param name="gameTime">Gametime.</param>
        /// <param name="spriteBatch">SpriteBatch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.DrawString(this.font, "Game Over", new Vector2(100, 100), Color.White);

            spriteBatch.End();
        }

        /// <summary>
        /// Handles the input for the game over screen.
        /// </summary>
        /// <param name="gameTime">Game over.</param>
        public override void PostUpdate(GameTime gameTime)
        {
        }

        /// <summary>
        /// Updates the game over screen.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
        }
    }
}
