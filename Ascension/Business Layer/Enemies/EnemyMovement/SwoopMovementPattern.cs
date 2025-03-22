using Microsoft.Xna.Framework;
using System;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// SwoopMovementPattern is a smooth turning movement pattern.
    /// </summary>
    internal class SwoopMovementPattern : IMovementPattern
    {
        private float time;
        private float duration;
        private float angularSpeed; // Radians per second
        private float radius;
        private bool swoopLeft;
        private bool isComplete;
        private Vector2 center;
        private float startAngle;
        private Vector2 startPosition;

        public SwoopMovementPattern(Vector2 startPosition, float turnRadius, float duration, bool swoopLeft)
        {
            this.time = 0f;
            this.duration = duration;
            this.radius = turnRadius;
            this.swoopLeft = swoopLeft;
            this.isComplete = false;
            this.startPosition = startPosition;

            // Adjust angular speed for clockwise or counterclockwise motion (180-degree turn over `duration`)
            this.angularSpeed = MathF.PI / (duration * 2);

            // Define center of circular motion (left or right)
            float direction = swoopLeft ? -1 : 1;
            this.center = startPosition + new Vector2(direction * turnRadius, turnRadius);

            // Start angle (facing left or right depending on the swoop direction)
            this.startAngle = swoopLeft ? 0 : 270; // 0 degrees for leftward, 270 degrees for rightward
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            if (isComplete) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += deltaTime;

            if (time >= duration)
            {
                isComplete = true;
            }

            // Compute new angle based on elapsed time (clockwise or counterclockwise motion)
            float angle = startAngle + (swoopLeft ? -1 : 1) * angularSpeed * time;

            // Compute new position using circular motion (ensuring downward movement)
            float newX = center.X + radius * MathF.Cos(-angle);
            float newY = center.Y + radius * MathF.Sin(-angle); // Negative to move downward

            enemy.Position = new Vector2(newX, newY);
        }

        public bool IsComplete()
        {
            return this.isComplete;
        }
    }
}
