using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace Ascension.Enemies.EnemyMovement
{
    internal class LinearMovementPattern : IMovementPattern
    {
        private bool complete = false;

        public void Move(GameTime gameTime, IMovable movable)
        {
            int timeMoved = 0;
            Vector2 velocity = movable.Velocity;

            movable.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeMoved += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            this.complete = true;
        }

        public bool IsComplete()
        {
            return this.complete;
        }
    }
}