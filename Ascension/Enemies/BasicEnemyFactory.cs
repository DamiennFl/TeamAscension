// <copyright file="BasicEnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ascension.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Enemies
{
    /// <summary>
    /// BasicEnemyFactory is the factory for creating enemies.
    /// </summary>
    internal class BasicEnemyFactory : EnemyFactory
    {
        /// <summary>
        /// Dictionary to hold enemy textures.
        /// </summary>
        private Dictionary<string, Texture2D> enemyTextures;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEnemyFactory"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="graphicsDevice">The graphics device for rendering.</param>
        public BasicEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager)
            : base(contentManager, graphicsDevice, collisionManager)
        {
            // Here we are creating the EnemyTextures dictionary and loading the textures.
            this.enemyTextures = new Dictionary<string, Texture2D>
            {
                { "EnemyA", this.ContentManager.Load<Texture2D>("Enemies/SpriteA") },
                { "EnemyB", this.ContentManager.Load<Texture2D>("Enemies/SpriteB") },
            };
        }

        /// <summary>
        /// Creates a basic enemy at the specified position.
        /// </summary>
        /// <param name="position">The spawn position for the basic enemy.</param>
        /// <param name="enemyType">The enemyType to create.</param>
        /// <returns>A new basic enemy instance.</returns>
        public override Enemy CreateEnemy(Vector2 position, string enemyType)
        {
            int speed = 40;
            switch (enemyType)
            {
                case "EnemyA":
                    return new EnemyA(speed, position, this.enemyTextures[enemyType], this.ContentManager, this.CollisionManager);

                case "EnemyB":
                    return new EnemyB(speed, position, this.enemyTextures[enemyType], this.ContentManager, this.CollisionManager);

                default:
                    throw new ArgumentException("Invalid enemy type specified.");
            }
        }
    }
}