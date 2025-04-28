// <copyright file="GoMiddleMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// Movement pattern that moves the enemy to the middle of the screen and stops there.
    /// </summary>
    internal class GoMiddleMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Target position to move to.
        /// </summary>
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

            if (Vector2.Distance(position, this.targetPosition) < 1f)
            {
                // Stop the enemy when it reaches the target position
                velocity = Vector2.Zero;
            }
            else
            {
                // Move towards the target position
                if (position.X < this.targetPosition.X)
                {
                    velocity.X = 1;
                }
                else if (position.X > this.targetPosition.X)
                {
                    velocity.X = -1;
                }
                else
                {
                    velocity.X = 0;
                }

                if (position.Y < this.targetPosition.Y)
                {
                    velocity.Y = 1;
                }
                else if (position.Y > this.targetPosition.Y)
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
