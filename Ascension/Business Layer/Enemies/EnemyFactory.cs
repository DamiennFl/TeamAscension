// <copyright file="EnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Enemies
{
    /// <summary>
    /// Abstract factory base class for creating enemy instances.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EnemyFactory"/> class.
    /// </remarks>
    /// <param name="contentManager">The content manager for loading assets.</param>
    /// <param name="graphicsDevice">The graphics device for rendering.</param>
    internal abstract class EnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        /// <summary>
        /// Content manager for loading game assets.
        /// </summary>
        private readonly ContentManager contentManager = contentManager;

        /// <summary>
        /// Graphics device for rendering.
        /// </summary>
        private readonly GraphicsDevice graphicsDevice = graphicsDevice;

        /// <summary>
        /// Gets the content manager for loading game assets.
        /// </summary>
        protected ContentManager ContentManager => this.contentManager;

        // Abstract Factory Methods to create Enemies. FACTORY METHOD PATTERN
        public abstract Enemy CreateEnemyA(Vector2 position, int speed);
        public abstract Enemy CreateEnemyB(Vector2 position, int speed);
        public abstract Enemy CreateMidBoss(Vector2 position, int speed);
        public abstract Enemy CreateFinalBoss(Vector2 position, int speed);
    }
}
