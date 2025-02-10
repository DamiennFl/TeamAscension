// <copyright file="State.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Content.States
{
    /// <summary>
    /// State.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Field to load in our content.
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// Field to load in our graphics device.
        /// </summary>
        protected GraphicsDevice graphicsDevice;

        /// <summary>
        /// Field to load in our game.
        /// </summary>
        protected Game1 game;

        /// <summary>
        /// Helps us Draw our State.
        /// </summary>
        /// <param name="gameTime">.</param>
        /// <param name="spriteBatch">..</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// For Removing components.
        /// </summary>
        /// <param name="gameTime">.</param>
        public abstract void PostUpdate(GameTime gameTime);

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="game">game.</param>
        /// <param name="graphicsDevice">GD.</param>
        /// <param name="content">content.</param>
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
        }

        /// <summary>
        /// Our Update call.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of the game timing state.</param>
        public abstract void Update(GameTime gameTime);
    }
}
