// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Ascension.Business_Layer.Movement;
using Ascension.Collision;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Ascension.Business_Layer;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    /// <summary>
    /// Abstract class for enemies.
    /// </summary>
    public abstract class Enemy : IMovable
    {
        /// <summary>
        /// The texture for the enemy.
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// Queue of movement patterns.
        /// </summary>
        public IMovementPattern MovementPattern { get; set; }

        protected CollisionManager collisionManager;

        /// <summary>
        /// Event for when a bullet is fired
        /// </summary>
        public event Action<Vector2, Vector2, bool> BulletFired;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="speed">Speed of the enemy.</param>
        /// <param name="position">Postion of the enemy.</param>
        /// <param name="texture">Texture of the enemy.</param>
        /// <param name="enemyType">Type of enemy (A or B).</param>
        public Enemy(Vector2 velocity, Vector2 position, Texture2D texture)
        {
            this.Velocity = velocity;
            this.texture = texture;
            this.Position = position;
            this.collisionManager = collisionManager;
        }

        /// <summary>
        /// Gets or sets the speed of the enemy.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the position of the enemy.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">Sprites.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Shoot method calls event.
        /// </summary>
        /// <param name="velo">bulet velocity.</param>
        /// <param name="isPlayerBullet">if it is a user's bullet.</param>
        public virtual void Shoot(Vector2 velo, bool isPlayerBullet)
        {
            this.BulletFired?.Invoke(this.Position, velo, isPlayerBullet);
        }

    }
}