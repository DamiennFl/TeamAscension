using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    public class LinearMovementPattern : IMovementPattern
    {
        private bool complete = false;

        public void Move(GameTime gameTime, IMovable movable, float duration)
        {
            int timeMoved = 0;
            Vector2 velocity = movable.Velocity;
            while (timeMoved < duration)
            {
                movable.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeMoved += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            this.complete = true;
        }

        public bool IsComplete()
        {
            return this.complete;
        }
    }
}