// <copyright file="MidBoss.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    /// <summary>
    /// Creates instances of MidBoss, a type of Enemy.
    /// </summary>
    internal class MidBoss : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidBoss"/> class.
        /// </summary>
        /// <param name="speed">The speed of MidBoss.</param>
        /// <param name="position">The position of MidBoss.</param>
        /// <param name="texture">The texture of MidBoss A.</param>
        public MidBoss(int speed, Vector2 position, Texture2D texture)
        : base(speed, position, texture, "MidBoss")
        {
        }

        /// <summary>
        /// Update method for updating MidBoss instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
        public override void Update(GameTime gameTime)
        {
            // Update stuff
        }

        /// <summary>
        /// Draw method for drawing MidBoss instances.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch the sprite belongs to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw stuff
        }

        /// <summary>
        /// Shoot method.
        /// </summary>
        /// <exception cref="NotImplementedException">Throws exception not implemented.</exception>
        public override void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}