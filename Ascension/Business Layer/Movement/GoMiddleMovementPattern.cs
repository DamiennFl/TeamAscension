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
        /// <summary>
        /// Moves the enemy to the middle of the screen and stays there.
        /// </summary>
        /// <param name="gameTime">Current gametime.</param>
        /// <param name="movable">Movable object.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            Vector2 velocity = movable.Velocity;
            Vector2 position = movable.Position;
            if (position.X < 400)
            {
                velocity.X = 1;
            }
            else if (position.X > 400)
            {
                velocity.X = -1;
            }

            if (position.Y < 300)
            {
                velocity.Y = 1;
            }
            else if (position.Y > 300)
            {
                velocity.Y = -1;
            }

            movable.Velocity = velocity;
            movable.Position = position;
        }
    }
}
