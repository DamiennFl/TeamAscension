﻿// <copyright file="EnemyA.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Creates instances of EnemyA, a type of Enemy.
    /// </summary>
    internal class EnemyA : Enemy
    {
        /// <summary>
        /// The content manager for loading assets.
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random random;

        /// <summary>
        /// When the enemy will shoot.
        /// </summary>
        private float shootTimer;

        /// <summary>
        /// Interval between shots.
        /// </summary>
        private float shootInterval;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyA"/> class.
        /// </summary>
        /// <param name="velocity">The speed of EnemyA.</param>
        /// <param name="position">The position of EnemyA.</param>
        /// <param name="texture">The texture of Enemy A.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>
        public EnemyA(Vector2 velocity, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(velocity, position, texture)
        {
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootTimer = 0f;
            this.shootInterval = this.GetRandomShootInterval();
        }

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.Position,
                null,
                Color.White,
                0f,
                new Vector2(this.texture.Width / 2, this.texture.Height / 2),
                new Vector2(this.Scale, this.Scale),
                SpriteEffects.None,
                0f);
        }

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">Current game time.</param>
        public override void Update(GameTime gameTime)
        {
            this.MovementPattern.Move(gameTime, this);

            // Timer for shooting
            this.shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.shootTimer >= this.shootInterval)
            {
                this.Shoot();
                this.shootTimer = 0f;
                this.shootInterval = this.GetRandomShootInterval();
            }
        }

        /// <summary>
        /// Shoots a bullet.
        /// </summary>
        public void Shoot()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue");
            Vector2 bulletVelocity = new Vector2(0, 2f);
            bool isPlayerBullet = false;
            base.Shoot(bulletVelocity, isPlayerBullet, bulletTexture);
        }

        /// <summary>
        /// Shoots a bullet.
        /// </summary>
        public void RegularShoot()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue");
            Vector2 bulletVelocity = new Vector2(0, 1.2f);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
            bullet.IsPlayerBullet = false;
        }

        /// <summary>
        /// Shoots a bullet Shaped Like a star.
        /// </summary>
        public void StarShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletOrange");
            float angle = -0.5F;
            for (int i = 0; i < 5; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 0.2f);
                Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
                angle += 0.2F;
            }
        }

        /// <summary>
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return ((float)this.random.NextDouble() * 2f) + 1f; // Random interval between 1 and 3 seconds
        }
    }
}