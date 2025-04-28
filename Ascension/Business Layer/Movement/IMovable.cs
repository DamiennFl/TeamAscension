// <copyright file="IMovable.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;

namespace Ascension
{
    /// <summary>
    /// Interface for movable objects.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Gets or sets the position of the movable object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the movable object.
        /// </summary>
        Vector2 Velocity { get; set; }
    }
}