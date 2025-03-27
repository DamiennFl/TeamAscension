using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension
{
    internal class GoMiddleMovementPattern : IMovementPattern
    {
        private readonly Vector2 targetPosition = new Vector2(280, 250);

        /// <summary>
        /// Moves the enemy to the middle of the screen and stops there.
        /// </summary>
        /// <param name="gameTime">Current gametime.</param>
        /// <param name="movable">Movable object.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            Vector2 velocity = movable.Velocity;
            Vector2 position = movable.Position;

            if (Vector2.Distance(position, targetPosition) < 1f)
            {
                // Stop the enemy when it reaches the target position
                velocity = Vector2.Zero;
            }
            else
            {
                // Move towards the target position
                if (position.X < targetPosition.X)
                {
                    velocity.X = 1;
                }
                else if (position.X > targetPosition.X)
                {
                    velocity.X = -1;
                }
                else
                {
                    velocity.X = 0;
                }

                if (position.Y < targetPosition.Y)
                {
                    velocity.Y = 1;
                }
                else if (position.Y > targetPosition.Y)
                {
                    velocity.Y = -1;
                }
                else
                {
                    velocity.Y = 0;
                }
            }

            movable.Velocity = velocity;
            movable.Position += velocity;
        }
    }
}
