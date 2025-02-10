using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Creates an enemy at the specified position.
        /// </summary>
        /// <param name="position">The spawn position for the enemy.</param>
        /// <returns>A new enemy instance.</returns>
        public abstract Enemy CreateEnemy(Vector2 position);
    }
}
