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
        : base(speed, position, texture, "EnemyA")
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            // Basic movement: move down
            this.Position = new Vector2(this.Position.X, this.Position.Y + (this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
        }
    }
}
