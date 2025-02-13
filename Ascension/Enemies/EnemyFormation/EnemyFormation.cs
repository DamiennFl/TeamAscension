using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Ascension.Enemies.EnemyFormation
{
    /// <summary>
    /// EnemyFormation is a grouping of Enemies with the same Movement, in a formation.
    /// </summary>
    internal abstract class EnemyFormation
    {
        public Vector2 FormationStartPosition { get; set; }
        public List<Enemy> enemies = new List<Enemy>();

        protected EnemyFormation(Vector2 startPosition)
        {
            this.FormationStartPosition = startPosition;
        }

        public abstract void Update(GameTime gameTime);


    }
}