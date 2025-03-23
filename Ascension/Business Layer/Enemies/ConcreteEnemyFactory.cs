﻿// <copyright file="ConcreteEnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ascension.Collision;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Enemies
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
        public ConcreteEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager)
            : base(contentManager, graphicsDevice, collisionManager)
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
        public override Enemy CreateEnemyA(Vector2 position, Vector2 velocity)
        {
            return new EnemyA(velocity, position, this.enemyTextures["SpriteA"], this.ContentManager);
        }

        public override Enemy CreateEnemyB(Vector2 position, Vector2 velocity)
        {
            return new EnemyB(velocity, position, this.enemyTextures["SpriteB"], this.ContentManager);
        }

        public override Enemy CreateMidBoss(Vector2 position, Vector2 velocity)
        {
            return new MidBoss(velocity, position, this.enemyTextures["MidBoss"], this.ContentManager);
        }

        public override Enemy CreateFinalBoss(Vector2 position, Vector2 velocity)
        {
            return new MidBoss(velocity, position, this.enemyTextures["FinalBoss"], this.ContentManager);
        }
    }
}