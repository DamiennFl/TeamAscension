// <copyright file="EnemyA.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Ascension.Business_Layer.Shooting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Creates instances of EnemyA, a type of Enemy.
    /// </summary>
    internal class EnemyA : Enemy
    {
        /// <summary>
        /// The content manager for loading assets.
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random random;

        /// <summary>
        /// Tracks individual timers for each shooting pattern.
        /// </summary>
        private Dictionary<IShootingPattern, float> shootingTimers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyA"/> class.
        /// </summary>
        /// <param name="velocity">The speed of EnemyA.</param>
        /// <param name="position">The position of EnemyA.</param>
        /// <param name="texture">The texture of Enemy A.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="bulletType">The type of bullet to shoot.</param>
        public EnemyA(Vector2 velocity, Vector2 position, int health, Texture2D texture, ContentManager contentManager, string bulletType)
        : base(velocity, position, health, texture, bulletType)
        {
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootingTimers = new Dictionary<IShootingPattern, float>();
            this.font = contentManager.Load<SpriteFont>("Fonts/Font");
        }

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.Position,
                null,
                Color.White,
                0f,
                new Vector2(this.texture.Width / 2, this.texture.Height / 2),
                new Vector2(this.Scale, this.Scale),
                SpriteEffects.None,
                0f);
            spriteBatch.DrawString(this.font, string.Empty + this.Health, new Vector2(this.Bounds.X, this.Bounds.Y), Color.Red);
        }

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">Current game time.</param>
        public override void Update(GameTime gameTime)
        {
            this.MovementPattern.Move(gameTime, this);

            // Initialize timers for each shooting pattern if not already done
            foreach (var pattern in this.ShootingPatterns.Keys)
            {
                if (!this.shootingTimers.ContainsKey(pattern))
                {
                    this.shootingTimers[pattern] = 0f; // Initialize timer for this pattern
                }
            }

            // Update timers and check if it's time to shoot
            foreach (var pattern in this.ShootingPatterns)
            {
                IShootingPattern shootingPattern = pattern.Key;
                string intervalString = pattern.Value;

                // Update the timer for this pattern
                this.shootingTimers[shootingPattern] += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Determine the shoot interval
                float shootInterval;
                if (intervalString.Equals("Random", StringComparison.OrdinalIgnoreCase))
                {
                    shootInterval = this.GetRandomShootInterval();
                }
                else if (float.TryParse(intervalString, out float parsedInterval))
                {
                    shootInterval = parsedInterval;
                }
                else
                {
                    continue; // Skip invalid intervals
                }

                // Check if it's time to shoot
                if (this.shootingTimers[shootingPattern] >= shootInterval)
                {
                    shootingPattern.Shoot(this); // Perform the shooting action
                    this.shootingTimers[shootingPattern] = 0f; // Reset the timer for this pattern
                }
            }
        }

        /// <summary>
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return ((float)this.random.NextDouble() * 2f); // Random interval between 1 and 3 seconds
        }
    }
}