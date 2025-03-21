﻿// <copyright file="Player.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player's speed.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        public float PlayerSpeed;
#pragma warning restore SA1401 // Fields should be private


        private Texture2D playerTexture;

        private Texture2D bulletTexture;

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


        public const int PlayerDamage = 10;

        public Vector2 BulletVelocity = new Vector2(0, 2f);

        public Vector2 BulletPosition;

        public Bullet bullet;

        public List<Bullet> bullets = new List<Bullet>();

        private PlayArea playArea;

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
            this.playArea = playArea;

            this.playerPosition = this.PlayerSpawn;
            this.BulletPosition = this.playerPosition; // Spawning position of the bullet should be the where the player is at
            this.bullet = new Bullet(PlayerDamage, BulletVelocity, this.BulletPosition, this.bulletTexture);
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

            foreach (var bullet in this.bullets)
            {
                bullet.BulletDraw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {

            this.PlayerMovement();
            this.StayInBorder(this.playArea.BorderRectangle, this.playArea.BorderWidth);
            this.PlayerShoot();
            foreach (var bullet in this.bullets)
            {
                bullet.BulletUpdate(gameTime);
            }
        }

        /// <summary>
        /// Player movement method.
        /// </summary>
        /// <param name="updatedPlayerSpeed">updating the player speed.</param>
        public void PlayerMovement()
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Texture2D bulletTexture = this.bulletTexture;
                this.BulletPosition = this.playerPosition;
                this.bullet = new Bullet(PlayerDamage, -BulletVelocity, this.BulletPosition, bulletTexture);
                this.bullets.Add(this.bullet);
            }

        }


    }
}
