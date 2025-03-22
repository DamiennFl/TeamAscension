// <copyright file="IMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>
using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// IMovementPattern is an interface for Enemy Movement patterns.
    /// </summary>
    internal interface IMovementPattern
    {
        /// <summary>
        /// Updates the Enemy movement based on the pattern.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with game run-time.</param>
        /// <param name="enemy">Enemy to update.</param>
        void Update(GameTime gameTime, Enemy enemy)
        {
        }

        bool IsComplete();
    }
}
