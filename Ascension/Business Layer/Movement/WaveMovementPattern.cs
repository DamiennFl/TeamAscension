using Microsoft.Xna.Framework;
using System;
using System.Reflection.Metadata;

namespace Ascension
{
    /// <summary>
    /// CircularMovementPattern makes the enemy move in a circle.
    /// </summary>
    public class WaveMovementPattern : IMovementPattern
    {
        private float time; // Track elapsed time for the sine wave calculation
        private readonly float amplitude = 100f;
        private readonly float frequency = 5f;

        public void Move(GameTime gameTime, IMovable movable)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += deltaTime;

            Vector2 velocity = movable.Velocity;
            Vector2 direction = Vector2.Normalize(velocity); // Ensure direction is unit length

            // Compute forward movement
            movable.Position += velocity;

            // Compute sine wave displacement perpendicular to movement direction
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X); // Perpendicular vector
            float sineOffset = (float)Math.Sin(time * frequency) * amplitude;

            // Apply only the sine wave offset to position
            movable.Position += perpendicular * sineOffset * deltaTime; // Scale by deltaTime for smoothness
        }
    }
}