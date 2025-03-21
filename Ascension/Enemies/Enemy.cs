﻿// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    /// <summary>
    /// Abstract class for enemies.
    /// </summary>
    internal abstract class Enemy
    {
        /// <summary>
        /// The texture for the enemy.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected Texture2D texture;

        /// <summary>
        /// Queue of movement patterns.
        /// </summary>
        protected Queue<IMovementPattern> movementPatterns = new Queue<IMovementPattern>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="speed">Speed of the enemy.</param>
        /// <param name="position">Postion of the enemy.</param>
        /// <param name="texture">Texture of the enemy.</param>
        /// <param name="enemyType">Type of enemy (A or B).</param>
        public Enemy(int speed, Vector2 position, Texture2D texture, string enemyType)
        {
            this.Speed = speed;
            this.texture = texture;
            this.Position = position;
            this.EnemyType = enemyType;
        }

        /// <summary>
        /// Gets or sets the type of enemy.
        /// </summary>
        public string EnemyType { get; set; }

        /// <summary>
        /// Gets or sets the speed of the enemy.
        /// </summary>
        public int Speed { get; set; }

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
        /// Shoot method.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Adds a movement pattern to the enemy.
        /// </summary>
        /// <param name="pattern">Movement pattern.</param>
        public void AddMovementPattern(IMovementPattern pattern)
        {
            this.movementPatterns.Enqueue(pattern);
        }

        /// <summary>
        /// Clears all movement patterns.
        /// </summary>
        public void ClearMovementPatterns()
        {
            this.movementPatterns.Clear();
        }

        /// <summary>
        /// Updates the movement patterns.
        /// </summary>
        /// <param name="gameTime">Current game time.</param>
        protected void UpdateMovementPatterns(GameTime gameTime)
        {
            if (this.movementPatterns.Count > 0)
            {
                var currentPattern = this.movementPatterns.Peek();
                currentPattern.Update(gameTime, this);

                if (currentPattern.IsComplete())
                {
                    this.movementPatterns.Dequeue();
                }
            }
        }
    }
}