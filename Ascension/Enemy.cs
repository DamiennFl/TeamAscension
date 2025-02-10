// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    ///  Abstract base class for all enemies.
    /// </summary>
    public abstract class Enemy
    {
        /// <summary>
        /// Gets or sets the texture of the enemy.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the position of the enemy.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the speed of the enemy.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Gets or sets the health of the enemy.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Abstract Draw method to be implemented by derived classes.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Abstract Update method to be implemented by derived classes.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);
    }
}