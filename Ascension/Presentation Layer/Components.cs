// <copyright file="Components.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// The components.
    /// </summary>
    internal abstract class Components
    {
        /// <summary>
        /// Draw Method, will be overwritten per component.
        /// </summary>
        /// <param name="gameTime">.</param>
        /// <param name="spriteBatch">..</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Update Method, will be overwritten per component.
        /// </summary>
        /// <param name="gameTime">.</param>
        public abstract void Update(GameTime gameTime);
    }
}
