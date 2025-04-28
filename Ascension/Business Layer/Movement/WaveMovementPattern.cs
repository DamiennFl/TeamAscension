// <copyright file="WaveMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// CircularMovementPattern makes the enemy move in a circle.
    /// </summary>
    public class WaveMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Frequency of the sine wave.
        /// </summary>
        private readonly float frequency = 5f;

        /// <summary>
        /// Amplitude and frequency of the sine wave.
        /// </summary>
        private readonly float amplitude = 100f;

        /// <summary>
        /// The time variable is used to track the elapsed time for the sine wave calculation.
        /// </summary>
        private float time; // Track elapsed time for the sine wave calculation

        /// <summary>
        /// Moves the enemy in a wave pattern.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="movable">Movable.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.time += deltaTime;

            Vector2 velocity = movable.Velocity;
            Vector2 direction = Vector2.Normalize(velocity); // Ensure direction is unit length

            // Compute forward movement
            Vector2 newPosition = movable.Position + velocity;

            // Compute sine wave displacement perpendicular to movement direction
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X); // Perpendicular vector
            float sineOffset = (float)Math.Sin(this.time * this.frequency) * this.amplitude;

            // Apply only the sine wave offset to position
            newPosition += perpendicular * sineOffset * deltaTime; // Scale by deltaTime for smoothness

            // Update the position
            movable.Position = newPosition;
        }
    }
}