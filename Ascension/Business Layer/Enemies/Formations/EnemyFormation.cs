using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    /// <summary>
    /// EnemyFormation is a grouping of Enemies with the same Movement, in a formation.
    /// </summary>
    internal abstract class EnemyFormation
    {
        /// <summary>
        /// The list of enemies in this EnemyFormation.
        /// </summary>
        protected List<Enemy> enemies;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyFormation"/> class.
        /// </summary>
        /// <param name="startPosition">The startPosition vector for this EnemyFormation.</param>
        protected EnemyFormation(Vector2 startPosition)
        {
            this.enemies = new List<Enemy>();
            this.FormationStartPosition = startPosition;
        }

        /// <summary>
        /// Gets or sets the starting position vector for the enemies in this Formation.
        /// </summary>
        public Vector2 FormationStartPosition { get; set; }

        /// <summary>
        /// Update method for updating the EnemyFormation.
        /// </summary>
        /// <param name="gameTime">GameTime object to syn with game run-time.</param>
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        // TODO: Remove dead enemies
    }
}