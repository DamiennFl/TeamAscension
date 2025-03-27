// <copyright file="BorderManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography;

namespace Ascension.Business_Layer
{
    /// <summary>
    /// Manages the borders of the play area.
    /// </summary>
    public class BorderManager
    {

        /// <summary>
        /// The play area that the borders are associated with.
        /// </summary>
        private PlayArea playArea;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderManager"/> class.
        /// </summary>
        /// <param name="playArea">our play area.</param>
        public BorderManager(PlayArea playArea)
        {
            this.playArea = playArea;
        }

        /// <summary>
        /// Checks if the enemy is within the play area and reverses its velocity if it is not.
        /// </summary>
        /// <param name="enemy">enemy.</param>
        public void CheckAndReverseVelocity(Enemy enemy)
        {
            Rectangle border = this.playArea.BorderRectangle;
            Vector2 position = enemy.Position;
            Vector2 velocity = enemy.Velocity;
            Rectangle bounds = enemy.Bounds;

            float enemySpawnHalf = border.Height / 2;

            if (!(enemy.MovementPattern is GoOffScreenMovementPattern))
            {
                if (position.X <= (border.Left + (bounds.Width / 2)) || position.X >= (border.Right - (bounds.Width / 2)))
                {
                    if (position.X <= (border.Left + (bounds.Width / 2)))
                    {
                        velocity.X = System.Math.Abs(velocity.X);
                    }
                    else
                    {
                        velocity.X = -System.Math.Abs(velocity.X);
                    }
                }

                if (position.Y <= (border.Top + (bounds.Height / 2)) || position.Y >= (border.Bottom - enemySpawnHalf - (bounds.Height / 2)))
                {
                    if (position.Y <= (border.Top + (bounds.Height / 2)))
                    {
                        velocity.Y = System.Math.Abs(velocity.Y);
                    }
                    else
                    {
                        velocity.Y = -System.Math.Abs(velocity.Y);
                    }
                }

                enemy.Velocity = velocity;
            }
        }

        /// <summary>
        /// Keeps the entity within the borders of the play area.
        /// </summary>
        /// <param name="entity">player or enemy.</param>
        /// <param name="texture">texture.</param>
        public void StayInBorder(IMovable entity, Texture2D texture)
        {
            int radius = texture.Width / 8;
            Vector2 position = entity.Position;
            Rectangle screenBounds = this.playArea.BorderRectangle;
            int borderWidth = this.playArea.BorderWidth;

            // Left border
            if (position.X - radius < screenBounds.X)
            {
                position.X = screenBounds.X + radius;
            }

            // Right border
            else if (position.X + radius > screenBounds.X + screenBounds.Width - (2 * borderWidth))
            {
                position.X = screenBounds.X + screenBounds.Width - (2 * borderWidth) - radius;
            }

            // Top border
            if (position.Y - radius < screenBounds.Y)
            {
                position.Y = screenBounds.Y + radius;
            }

            // Bottom border
            else if (position.Y + radius > screenBounds.Y + screenBounds.Height - (2 * borderWidth))
            {
                position.Y = screenBounds.Y + screenBounds.Height - (2 * borderWidth) - radius;
            }

            entity.Position = position;
        }
    }
}
