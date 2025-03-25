using Microsoft.Xna.Framework;
using System;

namespace Ascension
{
    public class ZigZagMovementPattern : IMovementPattern
    {
        private float elapsedTime = 0f;
        private const float ChangeInterval = 1f; // Change direction every second
        private Random random = new Random();

        public void Move(GameTime gameTime, IMovable movable)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Change direction every second
            if (elapsedTime >= ChangeInterval)
            {
                elapsedTime = 0f; // Reset timer

                // Generate a random direction while maintaining speed
                float speed = movable.Velocity.Length();
                float angle = (float)(random.NextDouble() * Math.PI * 2); // Random angle (0 to 360 degrees)

                // Convert angle to velocity vector
                Vector2 newVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
                movable.Velocity = newVelocity;
            }

            // Apply movement
            movable.Position += movable.Velocity;
        }
    }
}