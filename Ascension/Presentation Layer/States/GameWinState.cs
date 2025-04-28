// <copyright file="GameWinState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// GameWinState is the state of the game when the player wins.
    /// </summary>
    public class GameWinState : State
    {
        /// <summary>
        /// The font used to display the win message.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWinState"/> class.
        /// </summary>
        /// <param name="game">Game.</param>
        /// <param name="graphicsDevice">Graphics device.</param>
        /// <param name="contentManager">Content manager.</param>
        public GameWinState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            this.font = this.contentManager.Load<SpriteFont>("Fonts/Font");
        }

        /// <summary>
        /// Draws the game win state.
        /// </summary>
        /// <param name="gameTime">Gametime.</param>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.DrawString(this.font, "Congrats You Have Won!!!", new Vector2(100, 100), Color.White);

            spriteBatch.End();
        }

        /// <summary>
        /// PostUpdate method for GameWinState.
        /// </summary>
        /// <param name="gameTime">Gametime.</param>
        public override void PostUpdate(GameTime gameTime)
        {
        }

        /// <summary>
        /// Update method for GameWinState.
        /// </summary>
        /// <param name="gameTime">Gameitme.</param>
        public override void Update(GameTime gameTime)
        {
        }
    }
}
