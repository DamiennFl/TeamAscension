using Microsoft.Xna.Framework;
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
        /// Initializes a new instance of the <see cref="BasicEnemyFactory"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="graphicsDevice">The graphics device for rendering.</param>
        public BasicEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {

        }

        /// <summary>
        /// Creates a basic enemy at the specified position.
        /// </summary>
        /// <param name="position">The spawn position for the basic enemy.</param>
        /// <returns>A new basic enemy instance.</returns>
        public override Enemy CreateEnemy(Vector2 position, string enemyType)
        {
            Texture2D basicTexture = this.ContentManager.Load<Texture2D>("ball");
            int speed = 40;
            switch (enemyType)
            {
                case "EnemyA":
                    return new EnemyA(speed, position, basicTexture);

                case "EnemyB":
                    return new EnemyB(speed, position, basicTexture);

                default:
                    throw new ArgumentException("Invalid enemy type specified.");
            }
        }
    }
}