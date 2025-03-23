// <copyright file="IMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>
using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// IMovementPattern is an interface for Enemy Movement patterns.
    /// </summary>
    public interface IMovementPattern
    {
        public void Move(GameTime gameTime, IMovable movable);

        public bool IsComplete();
    }
}