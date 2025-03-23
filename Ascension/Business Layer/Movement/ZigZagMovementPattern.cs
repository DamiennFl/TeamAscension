using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using System;

namespace Ascension.Business_Layer.Movement
{
    public class ZigZagMovementPattern : IMovementPattern
    {
        private bool complete;

        public void Move(GameTime gameTime, IMovable movable, float duration)
        {
            if (this.complete)
            {
                return;
            }

            Random random = new Random();
            float elapsedTime = 0f;
            float zigzagInterval = GetRandomInterval(random);
            Vector2 direction = GetRandomDirection(random);
            float speed = 100f; // Adjust the speed as needed

            while (elapsedTime < duration)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                elapsedTime += deltaTime;

                if (elapsedTime >= zigzagInterval)
                {
                    direction = GetRandomDirection(random);
                    zigzagInterval = GetRandomInterval(random);
                    elapsedTime = 0f; // Reset elapsed time for the next interval
                }

                Vector2 movement = direction * speed * deltaTime;
                movable.Position += movement;
            }

            this.complete = true;
        }

        private float GetRandomInterval(Random random)
        {
            return (float)(random.NextDouble() * 0.5 + 0.5); // Random interval between 0.5 and 1 second
        }

        private Vector2 GetRandomDirection(Random random)
        {
            float angle = (float)(random.NextDouble() * MathF.PI * 2);
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        public bool IsComplete()
        {
            return this.complete;
        }
    }
}