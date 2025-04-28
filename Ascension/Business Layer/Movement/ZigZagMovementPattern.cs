// <copyright file="ZigZagMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// Movement pattern that moves the enemy in a zigzag pattern.
    /// </summary>
    public class ZigZagMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Interval for changing direction.
        /// </summary>
        private const float ChangeInterval = 1f; // Change direction every second

        /// <summary>
        /// The elapsed time is used to track how long the enemy has been moving in the current direction.
        /// </summary>
        private float elapsedTime = 0f;

        /// <summary>
        /// Random number generator for generating random angles.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Moves the enemy in a zigzag pattern.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="movable">Movable.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Change direction every second
            if (this.elapsedTime >= ChangeInterval)
            {
                this.elapsedTime = 0f; // Reset timer

                // Generate a random direction while maintaining speed
                float speed = movable.Velocity.Length();
                float angle = (float)(this.random.NextDouble() * Math.PI * 2); // Random angle (0 to 360 degrees)

                // Convert angle to velocity vector
                Vector2 newVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
                movable.Velocity = newVelocity;
            }

            // Apply movement
            movable.Position += movable.Velocity;
        }
    }
}