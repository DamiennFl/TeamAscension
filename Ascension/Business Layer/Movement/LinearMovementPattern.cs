// <copyright file="LinearMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// Movement pattern that moves the enemy in a straight line.
    /// </summary>
    public class LinearMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Moves the enemy in a straight line.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="movable">Movable.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            Vector2 velocity = movable.Velocity;
            movable.Position += velocity;
        }
    }
}