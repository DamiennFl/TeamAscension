// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ascension.Business_Layer;
using Ascension.Business_Layer.Shooting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        /// Gets or sets a value indicating whether the enemy is a player.  
        /// </summary>
        public bool IsPlayer { get; set; } = false;

        /// <summary>
        /// Gets or sets the time interval between shots.
        /// </summary>
        public float ShootInterval { get; set; }

        /// <summary>
        /// Gets the number of shots per second.
        /// </summary>
        protected string ShotsPerSecond { get; private set; }

        /// <summary>
        /// Gets or sets the shooting patterns of the enemy.
        /// </summary>
        public Dictionary<IShootingPattern, string> ShootingPatterns { get; set; }

        /// <summary>
        /// The font used for rendering text.
        /// </summary>
        protected SpriteFont font;

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
        /// <param name="bulletType">The bullet type.</param>
        public Enemy(Vector2 velocity, Vector2 position, int health, Texture2D texture, string bulletType)
        {
            this.Velocity = velocity;
            this.texture = texture;
            this.Position = position;
            this.BulletType = bulletType;
            this.Health = health;
            this.ShootInterval = 1f;
            this.ShootingPatterns = new Dictionary<IShootingPattern, string>();
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
        /// Draws the bounding box of the enemy.
        /// </summary>
        /// <param name="spriteBatch">Sprite.</param>
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
        /// Gets or sets a value indicating the bullet type.
        /// </summary>
        public string BulletType { get; set; }

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
        public int Health { get; set; }

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
        /// <param name="bulletType">type of bullet.</param>
        public virtual void Shoot(Vector2 velo, bool isPlayerBullet, string bulletType)
        {
            this.BulletFired?.Invoke(this.Position, velo, isPlayerBullet, bulletType);
        }

        /// <summary>
        /// Shoots a bullet (default behavior).
        /// </summary>
        public virtual void Shoot()
        {
            foreach (IShootingPattern shootingPattern in this.ShootingPatterns.Keys)
            {
                shootingPattern.Shoot(this);
            }
        }

        /// <summary>
        /// Fires a bullet with a given velocity.
        /// </summary>
        /// <param name="velocity">velocity.</param>
        public void FireBullet(Vector2 velocity)
        {
            this.BulletFired?.Invoke(this.Position, velocity, this.IsPlayer, this.BulletType);
        }

        /// <summary>
        /// Fires a bullet from a specific position with a given velocity.
        /// </summary>
        /// <param name="spawnPosition">position.</param>
        /// <param name="velocity">velocity.</param>
        public void FireBulletFromPosition(Vector2 spawnPosition, Vector2 velocity)
        {
            this.BulletFired?.Invoke(spawnPosition, velocity, this.IsPlayer, this.BulletType);
        }

        /// <summary>
        /// Activates the invincibility of the enemy, if Need be.
        /// </summary>
        public void ActivateInvincibility()
        {
        }
    }
}