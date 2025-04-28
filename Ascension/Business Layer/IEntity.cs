// <copyright file="IEntity.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer
{
    /// <summary>
    /// Interface for all entities in the game.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// When the entity fires a bullet.
        /// </summary>
        event Action<Vector2, Vector2, bool, string> BulletFired;

        /// <summary>
        /// Gets or Sets Entity Health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or Sets a value indicating if the entity is invincible.
        /// </summary>
        bool IsInvincible { get; set; }

        /// <summary>
        /// Gets a value indicating whether the entity is dead.
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        /// Gets the entity's velocity.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Gets or sets the entity's bullet type.
        /// </summary>
        string BulletType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is a player.
        /// </summary>
        bool IsPlayer { get; set;  }

        /// <summary>
        /// Gets or sets the entity's shooting interval.
        /// </summary>
        float ShootInterval { get; set; }

        /// <summary>
        /// Activates the invincibility of the entity.
        /// </summary>
        void ActivateInvincibility();

        /// <summary>
        /// Fires a bullet with a given velocity.
        /// </summary>s
        /// <param name="velocity">Velocity.</param>
        void FireBullet(Vector2 velocity);

        /// <summary>
        /// Fires a bullet from a specific position with a given velocity.
        /// </summary>
        /// <param name="spawnPosition">Position bullet is spawned from.</param>
        /// <param name="velocity">Velocity bullet has.</param>
        void FireBulletFromPosition(Vector2 spawnPosition, Vector2 velocity);
    }
}
