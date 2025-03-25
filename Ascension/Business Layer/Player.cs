// <copyright file="Player.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ascension.Business_Layer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : ICollidable, IEntity
    {
        /// <summary>
        /// Gets or sets the player's speed.
        /// </summary>
        public float PlayerSpeed;

        /// <summary>
        /// Gets or sets the player's texture.
        /// </summary>
        private Texture2D playerTexture;

        /// <summary>
        /// Gets or sets the bullet's texture.
        /// </summary>
        private Texture2D bulletTexture;

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Gets or sets the graphics device.
        /// </summary>
        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// Gets or sets the time remaining for invincibility.
        /// </summary>
        private TimeSpan invincibleTimeRemaining = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the total time for invincibility.
        /// </summary>
        private readonly TimeSpan totalInvincibleTime = TimeSpan.FromSeconds(3);

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        protected Vector2 playerPosition;

        /// <summary>
        /// Gets or sets the player's shoot interval.
        /// </summary>
        private float shootInterval = 0.1f;

        /// <summary>
        /// Gets or sets the player's shoot timer.
        /// </summary>
        private float shootTimer = 0f;

        /// <summary>
        /// Gets the player's spawn position.
        /// </summary>
        public Vector2 PlayerSpawn
        {
            get
            {
                return new Vector2(this.graphicsDevice.Viewport.Width / 4, this.graphicsDevice.Viewport.Height - 150);
            }
        }

        /// <summary>
        /// Gets or sets the bullet's velocity.
        /// </summary>
        public Vector2 BulletVelocity = new Vector2(0, -7f);

        /// <summary>
        /// Gets or sets the bullet's position.
        /// </summary>
        public Vector2 BulletPosition;

        /// <summary>
        /// Gets or sets the play area.
        /// </summary>
        private PlayArea playArea;

        /// <summary>
        /// Event for when a bullet is fired.
        /// </summary>
        public event Action<Vector2, Vector2, bool, string> BulletFired;

        /// <summary>
        /// Gets or sets a value indicating whether the player is invincible.
        /// </summary>
        public bool IsInvincible { get; set; } = false;

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
                int radius = this.playerTexture.Width / 10;
                return new Rectangle(
                    (int)this.playerPosition.X - radius,
                    (int)this.playerPosition.Y - radius,
                    radius,
                    radius);
            }
        }

        /// <summary>
        /// Gets or sets the player's movement binds.
        /// </summary>
        public struct PlayerMovementKeys
        {
            /// <summary>
            /// Gets or sets the up key.
            /// </summary>
            public static Keys Up = Keys.W;

            /// <summary>
            /// Gets or sets the down key.
            /// </summary>
            public static Keys Down = Keys.S;

            /// <summary>
            /// Gets or sets the left key.
            /// </summary>
            public static Keys Left = Keys.A;

            /// <summary>
            /// Gets or sets the right key.
            /// </summary>
            public static Keys Right = Keys.D;

            /// <summary>
            /// Gets or sets the left key.
            /// </summary>
            public static Keys Sprint = Keys.LeftShift;

            /// <summary>
            /// Gets or sets the shoot key.
            /// </summary>
            public static Keys Shoot = Keys.Space;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="texture">Texture of player.</param>
        /// <param name="position">Position of player.</param>
        public Player(GraphicsDevice graphicsDevice, ContentManager contentManager, PlayArea playArea)
        {
            this.graphicsDevice = graphicsDevice;
            this.playerTexture = contentManager.Load<Texture2D>("ball");
            this.bulletTexture = contentManager.Load<Texture2D>("Bullets/BulletGreen");
            this.font = contentManager.Load<SpriteFont>("Fonts/Font");
            this.playArea = playArea;

            this.playerPosition = this.PlayerSpawn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// Empty constructor init.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public int Health { get; set; } = 100;

        /// <summary>
        /// Gets a value indicating whether the player is dead.
        /// </summary>
        public bool IsDead => this.Health <= 0;

        /// <summary>
        /// Draw method for drawing the player.
        /// </summary>
        /// <param name="spriteBatch">Sprites.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(
            //    this.playerTexture,
            //    this.playerPosition,
            //    null,
            //    Color.White,
            //    0f,
            //    new Vector2((this.playerTexture.Width / 2) + 13f, this.playerTexture.Height / 2),
            //    new Vector2(0.25F, 0.25F),
            //    SpriteEffects.None,
            //    0f);

            // Draw the player's health
            spriteBatch.DrawString(this.font, "Health: " + this.Health, new Vector2(800, 10), Color.White);
            DrawBounds(spriteBatch);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            Texture2D texture = this.playerTexture;
            Rectangle bounds = this.Bounds;
            Color color = Color.Red;

            // Draw top line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, bounds.Width, 1), color);
            // Draw bottom line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Bottom, bounds.Width, 1), color);
            // Draw left line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, 1, bounds.Height), color);
            // Draw right line
            spriteBatch.Draw(texture, new Rectangle(bounds.Right, bounds.Top, 1, bounds.Height), color);
        }

        /// <summary>
        /// Update method for updating the player.
        /// </summary>
        /// <param name="gameTime"> Game-time. </param>
        public void Update(GameTime gameTime)
        {
            this.PlayerMovement();
            this.StayInBorder(this.playArea.BorderRectangle, this.playArea.BorderWidth);
            this.PlayerShoot(gameTime);
            this.InvincibleTimer(gameTime);
        }

        /// <summary>
        /// This will activate the Invincibility of the player for a set amount of time.
        /// </summary>
        public void ActivateInvincibility()
        {
            this.IsInvincible = true;
            this.invincibleTimeRemaining = this.totalInvincibleTime;
            this.playerPosition = this.PlayerSpawn;
            Debug.WriteLine("Invincibility activated.");
        }

        /// <summary>
        /// Player movement.
        /// </summary>
        private void PlayerMovement()
        {
            var kstate = Keyboard.GetState();

            // Vector to normalize diagonal movement
            Vector2 dir = Vector2.Zero;

            if (kstate.IsKeyDown(PlayerMovementKeys.Up))
            {
                dir.Y -= 1;
            }

            if (kstate.IsKeyDown(PlayerMovementKeys.Down))
            {
                dir.Y += 1;
            }

            if (kstate.IsKeyDown(PlayerMovementKeys.Left))
            {
                dir.X -= 1;
            }

            if (kstate.IsKeyDown(PlayerMovementKeys.Right))
            {
                dir.X += 1;
            }

            if (kstate.IsKeyDown(PlayerMovementKeys.Sprint))
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
        private void StayInBorder(Rectangle screenBounds, int borderWidth)
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

        /// <summary>
        /// Player shooting.
        /// </summary>
        /// <param name="gameTime">game time.</param>
        private void PlayerShoot(GameTime gameTime)
        {
            // Timer for shooting
            this.shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Checking if we can shoot a bullet
            // Change this
            if (Keyboard.GetState().IsKeyDown(PlayerMovementKeys.Shoot) && this.shootTimer >= this.shootInterval)
            {
                this.BulletFired?.Invoke(this.playerPosition, this.BulletVelocity, true, "Green"); // check this
                this.shootTimer = 0;
            }
        }

        /// <summary>Bu
        /// Timer for our invincibility.
        /// </summary>
        /// <param name="gameTime">time running.</param>
        private void InvincibleTimer(GameTime gameTime)
        {
            // Are we invincible? if so decrement the time remaining.
            if (this.IsInvincible)
            {
                this.invincibleTimeRemaining -= gameTime.ElapsedGameTime;
                // Debug.WriteLine($"Invincible time remaining: {this.invincibleTimeRemaining.TotalSeconds:F2} seconds");

                // Are we no longer invincible? then set invincible to false.
                if (this.invincibleTimeRemaining <= TimeSpan.Zero)
                {
                    this.IsInvincible = false;
                    this.invincibleTimeRemaining = TimeSpan.Zero;
                    // Debug.WriteLine("Invincibility off.");
                }
            }
        }
    }
}
