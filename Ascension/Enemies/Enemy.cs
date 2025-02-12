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
    /// Abstract Enemy class.
    /// </summary>
    internal abstract class Enemy
    {
        /// <summary>
        /// The texture for the Enemy.
        /// </summary>
        public Texture2D Texture;

        public string EnemyType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="speed">The speed of the Enemy.</param>
        /// <param name="position">The position of the </param>
        /// <param name="texture">The texture of the Enemy.</param>
        public Enemy(int speed, Vector2 position, Texture2D texture, string enemyType)
        {
            this.Speed = speed;
            this.Texture = texture;
            this.Position = position;
            this.EnemyType = enemyType;
        }

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
    }
}
