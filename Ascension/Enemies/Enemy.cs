using System.Collections.Generic;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Enemies
{
    /// <summary>
    /// Abstract Enemy class.
    /// </summary>
    internal abstract class Enemy
    {
        /// <summary>
        /// The texture for the Enemy.
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// List of movement patterns for this Enemy.
        /// </summary>
        protected List<IMovementPattern> movementPatterns = new List<IMovementPattern>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="speed">The speed of the Enemy.</param>
        /// <param name="position">The position of the </param>
        /// <param name="texture">The texture of the Enemy.</param>
        /// <param name="enemyType">The Enemy type.</param>
        public Enemy(int speed, Vector2 position, Texture2D texture, string enemyType)
        {
            this.Speed = speed;
            this.texture = texture;
            this.Position = position;
            this.EnemyType = enemyType;
        }

        /// <summary>
        /// Gets or sets the type of Enemy.
        /// </summary>
        public string EnemyType { get; set; }

        /// <summary>
        /// Gets or sets the Speed of the Enemy.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the position of the Enemy.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Update method for updating Enemies.
        /// </summary>
        /// <param name="gameTime">GameTime to keep in sync with game runtime.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draw method for drawing the sprite.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch the sprite belongs to.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Shoot method for shooting bullets.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Add a movement pattern to the enemy.
        /// </summary>
        /// <param name="pattern">The IMovementPattern to add.</param>
        public void AddMovementPattern(IMovementPattern pattern)
        {
            this.movementPatterns.Add(pattern);
        }
    }
}
