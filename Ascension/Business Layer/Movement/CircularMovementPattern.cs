using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;
using System;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// SwoopMovementPattern is a smooth turning movement pattern.
    /// </summary>
    public class CircularMovementPattern : IMovementPattern
    {
        private Vector2 startPosition;
        private Vector2 center;
        private float radius;
        private float angle;
        private bool complete = false;
        private Vector2 linearDirection;
        private float linearSpeed = 10f; // Adjust the speed as needed
        public float Duration { get; set; }


        public CircularMovementPattern()
        {
            // Initialize the linear direction to a random unit vector
            Random random = new Random();
            float angle = (float)(random.NextDouble() * MathF.PI * 2);
            this.linearDirection = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        public void Move(GameTime gameTime, IMovable movable)
        {
            if (this.complete)
            {
                return;
            }

            this.startPosition = movable.Position;
            this.radius = 90; // Default radius, can be adjusted as needed
            this.center = this.startPosition + new Vector2(0, this.radius);

            float elapsedTime = 0f;

            // Calculate the angle increment based on the duration
            float angleIncrement = (MathF.PI * 2); // Full circle (2 * PI) divided by duration
            this.angle += angleIncrement * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.angle >= MathF.PI * 2)
            {
                this.angle -= MathF.PI * 2;
            }

            float newX = this.center.X + (this.radius * MathF.Cos(this.angle));
            float newY = this.center.Y + (this.radius * MathF.Sin(this.angle));

            // Calculate the linear displacement
            float linearDisplacement = this.linearSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 linearMovement = this.linearDirection * linearDisplacement;

            // Apply both circular and linear movements
            movable.Position = new Vector2(newX, newY) + linearMovement;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.complete = true;
        }

        public bool IsComplete()
        {
            return this.complete;
        }
    }
}