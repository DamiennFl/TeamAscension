// <copyright file="Bullet.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Bullet class.
    /// </summary>
    public abstract class Bullet : IMovable, ICollidable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// Bullet constructor.
        /// </summary>
        /// <param name="damage">Damage of bullet.</param>
        /// <param name="velocity">Velocity of bullet.</param>
        /// <param name="bulletPosition">Bullet position.</param>
        /// <param name="bulletTexture">Bullet texture.</param>
        /// <param name ="movementPattern">Movement of bullet.</param>
        public Bullet(int damage, Vector2 velocity, Vector2 bulletPosition)
        {
            this.Damage = damage;
            this.Velocity = velocity;
            this.Position = bulletPosition;
            this.IsActive = true; // activate as soon  as it is
        }

        /// <summary>
        /// Gets or sets the damage of the bullet.
        /// </summary>
        public int Damage { get; set; } = 1;

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public Texture2D BulletTexture { get; set; }

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
        public string CollisionLayer => IsPlayerBullet ? "PlayerBullet" : "EnemyBullet";

        /// <summary>
        /// Gets the bounding rectangle for collision detection.
        /// </summary>
        /// <returns>A rectangle representing the bullet's collision bounds.</returns>
        public Rectangle Bounds => new Rectangle(
                (int)this.Position.X,
                (int)this.Position.Y,
                this.BulletTexture.Width,
                this.BulletTexture.Height);

        /// <summary>
        /// position of bullet.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets movement of bullet.
        /// </summary>
        public IMovementPattern MovementPattern { get; set; }

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
                spriteBatch.Draw(this.BulletTexture, this.Position, Color.White);
                DrawBounds(spriteBatch);
            }
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            Texture2D texture = this.BulletTexture;
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
        /// Bullet update.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public void BulletUpdate(GameTime gameTime)
        {
            this.MovementPattern.Move(gameTime, this);

            if (this.Position.X < 40 || this.Position.X > 480 || this.Position.Y < 40 || this.Position.Y > 750) // If bullet is outside of border (40, 40, 460, 720) then deactivate it.
            {
                this.IsActive = false;
            }
        }
    }
}