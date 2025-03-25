// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using Ascension.Business_Layer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Abstract class for enemies.
    /// </summary>
    public abstract class Enemy : IMovable, ICollidable, IEntity
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
        public event Action<Vector2, Vector2, bool, Texture2D> BulletFired;

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

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            Texture2D texture = this.texture;
            Rectangle bounds = this.Bounds;
            Color color = Color.Red;

            // Draw top line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, bounds.Width, 1), color);
            // Draw bottom line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Bottom, bounds.Width, 1), color);
            // Draw left line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, 1, bounds.Height), color);
            // Draw right line
            spriteBatch.Draw(texture, new Rectangle(bounds.Right, bounds.Top, 1, bounds.Height), color);
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
        /// Gets or sets the health of the enemy.
        /// </summary>
        public int Health { get; set; } = 10;

        /// <summary>
        /// Gets or Sets a value indicating whether the enemy is invincible.
        /// </summary>
        public bool IsInvincible { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether the enemy is dead.
        /// </summary>
        public bool IsDead => this.Health <= 0;

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
        public virtual void Shoot(Vector2 velo, bool isPlayerBullet, Texture2D bulletTexture)
        {
            this.BulletFired?.Invoke(this.Position, velo, isPlayerBullet, bulletTexture); // check this
        }

        /// <summary>
        /// Activates the invincibility of the enemy, if Need be.
        /// </summary>
        public void ActivateInvincibility()
        {
        }
    }
}