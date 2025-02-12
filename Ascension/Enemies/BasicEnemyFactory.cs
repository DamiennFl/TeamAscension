﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, Texture2D> EnemyTextures;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEnemyFactory"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="graphicsDevice">The graphics device for rendering.</param>
        public BasicEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            // Here we are creating the EnemyTextures dictionary and loading the textures.
            this.EnemyTextures = new Dictionary<string, Texture2D>
            {
                { "EnemyA", this.ContentManager.Load<Texture2D>("Enemies/SpriteA") },
                { "EnemyB", this.ContentManager.Load<Texture2D>("Enemies/SpriteB") },
            };
        }

        /// <summary>
        /// Creates a basic enemy at the specified position.
        /// </summary>
        /// <param name="position">The spawn position for the basic enemy.</param>
        /// <returns>A new basic enemy instance.</returns>
        public override Enemy CreateEnemy(Vector2 position, string enemyType)
        {
            int speed = 40;
            switch (enemyType)
            {
                case "EnemyA":
                    return new EnemyA(speed, position, this.EnemyTextures[enemyType], this.ContentManager);

                case "EnemyB":
                    return new EnemyB(speed, position, this.EnemyTextures[enemyType], this.ContentManager);

                default:
                    throw new ArgumentException("Invalid enemy type specified.");
            }
        }
    }
}