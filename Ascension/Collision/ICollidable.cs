// <copyright file="ICollidable.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;

namespace Ascension.Collision
{
    /// <summary>
    /// Interface for objects that can collide with other objects.
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// Gets the bounding rectangle for collision detection.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Gets the collision layer this object belongs to.
        /// </summary>
        string CollisionLayer { get; }

        /// <summary>
        /// Called when this object collides with another object.
        /// </summary>
        /// <param name="other">The other object involved in the collision.</param>
        void OnCollision(ICollidable other);
    }
}