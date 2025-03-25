// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Abstract class for enemies.
    /// </summary>
    public abstract class Enemy : IMovable, ICollidable
    {
        /// <summary>
        /// The texture for the enemy.
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// Gets or sets the scale at which the enemy is rendered.
        /// </summary>
        protected float Scale { get; set; } = 0.4f; // Default scale for all enemies

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="velocity">Speed of the enemy.</param>
        /// <param name="position">Postion of the enemy.</param>
        /// <param name="texture">Texture of the enemy.</param>
        /// <param name="collisionManager">The collision manager.</param>
        public Enemy(Vector2 velocity, Vector2 position, Texture2D texture)
        {
            this.Velocity = velocity;
            this.texture = texture;
            this.Position = position;
        }

        /// <summary>
        /// Event for when a bullet is fired
        /// </summary>
        public event Action<Vector2, Vector2, bool, string> BulletFired;

        /// <summary>
        /// Gets or sets queue of movement patterns.
        /// </summary>
        public IMovementPattern MovementPattern { get; set; }

        /// <summary>
        /// Gets the bounding box of the enemy.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                // Calculate scaled dimensions
                int scaledWidth = (int)(this.texture.Width * this.Scale);
                int scaledHeight = (int)(this.texture.Height * this.Scale);

                // Position is the center point, calculate top-left for the rectangle
                int x = (int)this.Position.X - (scaledWidth / 2);
                int y = (int)this.Position.Y - (scaledHeight / 2);

                return new Rectangle(x, y, scaledWidth, scaledHeight);
            }
        }

        /// <summary>
        /// Gets the collision layer identifier.
        /// </summary>
        public string CollisionLayer => "Enemy";

        /// <summary>
        /// Gets or sets the speed of the enemy.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the position of the enemy.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">Sprites.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Shoot method calls event.
        /// </summary>
        /// <param name="velo">bulet velocity.</param>
        /// <param name="isPlayerBullet">if it is a user's bullet.</param>
        /// <param name="bulletTexture">texture of the bullet.</param>
        public virtual void Shoot(Vector2 velo, bool isPlayerBullet, string bulletTexture)
        {
            this.BulletFired?.Invoke(this.Position, velo, isPlayerBullet, bulletTexture); // check this
        }
    }
}