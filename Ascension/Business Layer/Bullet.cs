// <copyright file="Bullet.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Bullet class.
    /// </summary>
    public class Bullet : IMovable // Implement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// Bullet constructor.
        /// </summary>
        /// <param name="damage">Damage of bullet.</param>
        /// <param name="velocity">Velocity of bullet.</param>
        /// <param name="bulletPosition">Bullet position.</param>
        /// <param name="bulletTexture">Bullet texture.</param>
        public Bullet(int damage, Vector2 velocity, Vector2 bulletPosition, Texture2D bulletTexture)
        {
            this.Damage = damage;
            this.Velocity = velocity;
            this.BulletPosition = bulletPosition;
            this.BulletTexture = bulletTexture;
            this.IsActive = true; // activate as soon  as it is
        }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public Texture2D BulletTexture { get; set; }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public Vector2 BulletPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bullet is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this bullet was fired by the player.
        /// </summary>
        public bool IsPlayerBullet { get; set; }

        /// <summary>
        /// Gets the collision layer for the bullet.
        /// </summary>
        public string CollisionLayer => this.IsPlayerBullet ? "PlayerBullet" : "EnemyBullet";

        /// <summary>
        /// Gets the bounding rectangle for collision detection.
        /// </summary>
        /// <returns>A rectangle representing the bullet's collision bounds.</returns>
        public Rectangle Bounds => new Rectangle(
                (int)this.BulletPosition.X,
                (int)this.BulletPosition.Y,
                this.BulletTexture.Width,
                this.BulletTexture.Height);

        /// <summary>
        /// Handles collision with another object.
        /// </summary>
        /// <param name="other">The object that collided with the bullet.</param>
        public void OnCollision(ICollidable other)
        {
            // Deactivate the bullet when it hits something
            this.IsActive = false;
        }

        /// <summary>
        /// Bullet draw.
        /// </summary>
        /// <param name="spriteBatch">Bullet sprite.</param>
        public void BulletDraw(SpriteBatch spriteBatch)
        {
            if (this.IsActive)
            {
                spriteBatch.Draw(this.BulletTexture, this.BulletPosition, Color.White);
            }
        }

        /// <summary>
        /// Bullet update.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public void BulletUpdate(GameTime gameTime)
        {
            this.BulletPosition += this.Velocity; // Move bullet by velocity.

            if (this.BulletPosition.X < 40 || this.BulletPosition.X > 480 || this.BulletPosition.Y < 40 || this.BulletPosition.Y > 750) // If bullet is outside of border (40, 40, 460, 720) then deactivate it.
            {
                this.IsActive = false;
            }
        }
    }
}
