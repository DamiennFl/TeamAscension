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

namespace Ascension
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

        /// <summary>
        /// Creates an instance of EnemyA.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="velocity">Velocity.</param>
        /// <param name="health">Health.</param>
        /// <param name="bulletType">Bullet type.</param>
        /// <returns>EnemyA instance.</returns>
        public abstract Enemy CreateEnemyA(Vector2 position, Vector2 velocity, int health, string bulletType);

        /// <summary>
        /// Creates an instance of EnemyB.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="velocity">Velocity.</param>
        /// <param name="health">Health.</param>
        /// <param name="bulletType">Bullet type.</param>
        /// <returns>Instance of EnemyB</returns>
        public abstract Enemy CreateEnemyB(Vector2 position, Vector2 velocity, int health, string bulletType);

        /// <summary>
        /// Creates an instance of MidBoss.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="velocity">Velocity.</param>
        /// <param name="health">Health.</param>
        /// <param name="bulletType">Bullet type.</param>
        /// <returns>Instance of the mid boss.</returns>
        public abstract Enemy CreateMidBoss(Vector2 position, Vector2 velocity, int health, string bulletType);

        /// <summary>
        /// Creates an instance of FinalBoss.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="velocity">Velocity.</param>
        /// <param name="health">Health.</param>
        /// <param name="bulletType">Bullet type.</param>
        /// <returns>Instance of final boss.</returns>
        public abstract Enemy CreateFinalBoss(Vector2 position, Vector2 velocity, int health, string bulletType);
    }
}
