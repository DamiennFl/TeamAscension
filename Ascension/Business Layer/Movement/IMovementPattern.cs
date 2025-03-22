// <copyright file="IMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>
using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// IMovementPattern is an interface for Enemy Movement patterns.
    /// </summary>
    internal interface IMovementPattern
    {
        public void Move(GameTime gameTime, IMovable movable, float duration);

        public bool IsComplete();
    }
}