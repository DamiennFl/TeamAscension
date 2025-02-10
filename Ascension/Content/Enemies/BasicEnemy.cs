// <copyright file="BasicEnemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Content.Enemies
{
    /// <summary>
    /// A basic enemy type.
    /// </summary>
    public class BasicEnemy : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEnemy"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The starting position.</param>
        public BasicEnemy(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
            this.Speed = 50f;
            this.Health = 1;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            // Basic movement: move left
            this.Position = new Vector2(this.Position.X, this.Position.Y + (this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
        }
    }
}