using Microsoft.Xna.Framework;
using System;

namespace Ascension
{
    public class ZigZagMovementPattern : IMovementPattern
    {
        public void Move(GameTime gameTime, IMovable movable)
        {

            Random random = new Random();
            float elapsedTime = 0f;
            float zigzagInterval = this.GetRandomInterval(random);
            Vector2 direction = this.GetRandomDirection(random);
            float speed = 100f; // Adjust the speed as needed

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += deltaTime;

            if (elapsedTime >= zigzagInterval)
            {
                direction = this.GetRandomDirection(random);
                zigzagInterval = this.GetRandomInterval(random);
                elapsedTime = 0f; // Reset elapsed time for the next interval
            }

            Vector2 movement = direction * speed * deltaTime;
            movable.Position += movement;

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
    }
}