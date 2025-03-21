// <copyright file="BossEnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Ascension.Collision;

namespace Ascension.Enemies
{

    internal class BossEnemyFactory : EnemyFactory
    {
        /// <summary>
        /// Dictionary to hold enemy textures.
        /// </summary>
        private Dictionary<string, Texture2D> bossEnemyTextures;

        /// <summary>
        /// Initializes a new instance of the <see cref="BossEnemyFactory"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="graphicsDevice">The graphics device for rendering.</param>
        public BossEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager)
            : base(contentManager, graphicsDevice, collisionManager)
        {
            // Here we are creating the EnemyTextures dictionary and loading the textures.
            this.bossEnemyTextures = new Dictionary<string, Texture2D>
            {
                { "MidBoss", this.ContentManager.Load<Texture2D>("Enemies/MidBoss") },
                { "FinalBoss", this.ContentManager.Load<Texture2D>("Enemies/FinalBoss") },
            };
        }

        /// <summary>
        /// Creates a boss enemy at the specified position.
        /// </summary>
        /// <param name="position">The spawn position for the basic enemy.</param>
        /// <param name="bossType">The enemyType to create.</param>
        /// <returns>A new basic enemy instance.</returns>
        public override Enemy CreateEnemy(Vector2 position, string bossType)
        {
            int speed = 60;
            switch (bossType)
            {
                case "MidBoss":
                    return new MidBoss(speed, position, this.bossEnemyTextures[bossType], this.ContentManager, this.CollisionManager);

                case "FinalBoss":
                    return new FinalBoss(speed, position, this.bossEnemyTextures[bossType], this.ContentManager, this.CollisionManager);

                default:
                    throw new ArgumentException("Invalid enemy type specified.");
            }
        }
    }
}
