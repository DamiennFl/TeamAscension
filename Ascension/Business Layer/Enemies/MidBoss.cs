// <copyright file="MidBoss.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Business_Layer.Bullets;
using Ascension.Business_Layer.Shooting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Creates instances of MidBoss, a type of Enemy.
    /// </summary>
    internal class MidBoss : Enemy
    {
        /// <summary>
        /// The content manager for loading assets.
        /// </summary>

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random random;

        /// <summary>
        /// Tracks individual timers for each shooting pattern.
        /// </summary>
        private Dictionary<IShootingPattern, float> shootingTimers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MidBoss"/> class.
        /// </summary>
        /// <param name="velocity">The speed of MidBoss.</param>
        /// <param name="position">The position of MidBoss.</param>
        /// <param name="health">The health of MidBoss.</param>
        /// <param name="texture">The texture of MidBoss A.</param>
        /// <param name="contentManager">"The content manager for loading assets.</param>
        /// <param name="bulletType">The type of bullet to shoot.</param>'
        /// <param name="shotsPerSecond">The shots per second of MidBoss.</param>
        public MidBoss(Vector2 velocity, Vector2 position, int health, Texture2D texture, ContentManager contentManager, string bulletType)
        : base(velocity, position, health, texture, bulletType)
        {
            this.random = new Random();
            this.shootingTimers = new Dictionary<IShootingPattern, float>();
            this.font = contentManager.Load<SpriteFont>("Fonts/Font");
        }

        /// <summary>
        /// Update method for updating MidBoss instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
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
        /// Draw method for drawing MidBoss instances.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch the sprite belongs to.</param>
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
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return ((float)this.random.NextDouble() * 1f) + 1.5f; // Random interval between 0 and 1 second
        }
    }
}