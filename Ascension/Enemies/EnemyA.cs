using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Enemies
{
    /// <summary>
    /// Creates instances of EnemyA, a type of Enemy.
    /// </summary>
    internal class EnemyA : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyA"/> class.
        /// </summary>
        /// <param name="speed">The speed of EnemyA.</param>
        /// <param name="texture">The texture of Enemy A.</param>
        public EnemyA(int speed, Vector2 position, Texture2D texture)
        : base(speed, position, texture)
        {
            this.Speed = speed;
            this.Texture = texture;
            this.Position = position;
        }

        /// <summary>
        /// Update method for updating EnemyA instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
        public override void Update(GameTime gameTime)
        {
            // Update stuff
        }

        /// <summary>
        /// Draw method for drawing EnemyA instances.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch the sprite belongs to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw stuff
        }
    }
}
