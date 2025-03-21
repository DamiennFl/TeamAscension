// <copyright file="Player.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ascension.Collision;
using System.Diagnostics;

namespace Ascension
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : ICollidable
    {
        /// <summary>
        /// Gets or sets the player's speed.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        public float PlayerSpeed;
#pragma warning restore SA1401 // Fields should be private


        private Texture2D playerTexture;

        /// <summary>
        /// Gets or sets the player's score.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private


        private GraphicsDevice graphicsDevice;


        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        protected Vector2 playerPosition;

        public Vector2 PlayerSpawn
        {
            get
            {
                return new Vector2(this.graphicsDevice.Viewport.Width / 4, this.graphicsDevice.Viewport.Height - 150);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="texture">Texture of player.</param>
        /// <param name="position">Position of player.</param>
        public Player(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.playerTexture = contentManager.Load<Texture2D>("ball");

            this.playerPosition = this.PlayerSpawn;
        }

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Draw method for drawing the player.
        /// </summary>
        /// <param name="spriteBatch">Sprites.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.playerTexture,
                this.playerPosition,
                null,
                Color.White,
                0f,
                new Vector2(this.playerTexture.Width / 4, this.playerTexture.Height / 4),
                new Vector2(0.25F, 0.25F),
                SpriteEffects.None,
                0f);
        }

        /// <summary>
        /// Player movement method.
        /// </summary>
        /// <param name="updatedPlayerSpeed">updating the player speed.</param>
        public void PlayerMovement(float updatedPlayerSpeed)
        {
            var kstate = Keyboard.GetState();

            // Vector to normalize diagonal movement
            Vector2 dir = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.W))
            {
                dir.Y -= 1;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                dir.Y += 1;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                dir.X -= 1;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                dir.X += 1;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                this.PlayerSpeed = 7f;
            }
            else
            {
                this.PlayerSpeed = 3.75f;
            }

            // If Vector has values, normalize movement.
            if (dir != Vector2.Zero)
            {
                dir.Normalize();
            }

            // Update position after normalizing
            this.playerPosition += dir * this.PlayerSpeed;
        }

        /// <summary>
        /// This ensures that the player stays within the border.
        /// </summary>
        /// <param name="screenBounds">Playing Screen.</param>
        /// <param name="borderWidth">Width of the border.</param>
        public void StayInBorder(Rectangle screenBounds, int borderWidth)
        {
            int radius = this.playerTexture.Width / 8;

            // Left border
            if (this.playerPosition.X - radius < screenBounds.X)
            {
                this.playerPosition.X = screenBounds.X + radius;
            }

            // Right border
            else if (this.playerPosition.X + radius > screenBounds.X + screenBounds.Width - (2 * borderWidth))
            {
                this.playerPosition.X = screenBounds.X + screenBounds.Width - (2 * borderWidth) - radius;
            }

            // Top border
            if (this.playerPosition.Y - radius < screenBounds.Y)
            {
                this.playerPosition.Y = screenBounds.Y + radius;
            }

            // Bottom border
            else if (this.playerPosition.Y + radius > screenBounds.Y + screenBounds.Height - (2 * borderWidth))
            {
                this.playerPosition.Y = screenBounds.Y + screenBounds.Height - (2 * borderWidth) - radius;
            }
        }

        public void PlayerShoot()
        {
            // Shoot bullet

        }

        /// <summary>
        /// Gets the collision layer for the player.
        /// </summary>
        public string CollisionLayer => "Player";

        /// <summary>
        /// Gets the bounding rectangle for collision detection.
        /// </summary>
        /// <returns>A rectangle representing the player's collision bounds.</returns>
        public Rectangle Bounds
        {
            get
            {
                int radius = this.playerTexture.Width / 8;
                return new Rectangle(
                    (int)this.playerPosition.X - radius,
                    (int)this.playerPosition.Y - radius,
                    radius * 2,
                    radius * 2);
            }
        }
    }
}
