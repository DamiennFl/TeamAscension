// <copyright file="ConcreteEnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension
{
    /// <summary>
    /// ConcreteEnemyFactory is the factory for creating enemies.
    /// </summary>
    internal class ConcreteEnemyFactory : EnemyFactory
    {
        /// <summary>
        /// Dictionary to hold enemy textures.
        /// </summary>
        private Dictionary<string, Texture2D> enemyTextures;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteEnemyFactory"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="graphicsDevice">The graphics device for rendering.</param>
        public ConcreteEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            // Here we are creating the EnemyTextures dictionary and loading the textures.
            this.enemyTextures = new Dictionary<string, Texture2D>
            {
                { "EnemyA", this.ContentManager.Load<Texture2D>("Enemies/SpriteA") },
                { "EnemyB", this.ContentManager.Load<Texture2D>("Enemies/SpriteB") },
                { "MidBoss", this.ContentManager.Load<Texture2D>("Enemies/MidBoss") },
                { "FinalBoss", this.ContentManager.Load<Texture2D>("Enemies/FinalBoss") },
            };
        }

        // TODO: Add relevant enemies to a Formation object, instead of having a Factory in each Formation
        public override Enemy CreateEnemyA(Vector2 position, Vector2 velocity, int health, string bulletType, float shotsPerSecond)
        {
            return new EnemyA(velocity, position, health, this.enemyTextures["EnemyA"], this.ContentManager, bulletType, shotsPerSecond);
        }

        public override Enemy CreateEnemyB(Vector2 position, Vector2 velocity, int health, string bulletType, float shotsPerSecond)
        {
            return new EnemyB(velocity, position, health, this.enemyTextures["EnemyB"], this.ContentManager, bulletType, shotsPerSecond);
        }

        public override Enemy CreateMidBoss(Vector2 position, Vector2 velocity, int health, string bulletType, float shotsPerSecond)
        {
            return new MidBoss(velocity, position, health, this.enemyTextures["MidBoss"], this.ContentManager, bulletType, shotsPerSecond);
        }

        public override Enemy CreateFinalBoss(Vector2 position, Vector2 velocity, int health, string bulletType, float shotsPerSecond)
        {
            return new FinalBoss(velocity, position, health, this.enemyTextures["FinalBoss"], this.ContentManager, bulletType, shotsPerSecond);
        }
    }
}