// <copyright file="EnemyB.cs" company="Team Ascension">
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

namespace Ascension
{
    /// <summary>
    /// Creates instances of EnemyB, a type of Enemy.
    /// </summary>
    internal class EnemyB : Enemy
    {
        /// <summary>
        /// Random number generator.
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// The texture for the bullet.
        /// </summary>
        private Texture2D bulletTexture;

        /// <summary>
        /// When the enemy will shoot.
        /// </summary>
        private float shootTimer;

        /// <summary>
        /// Interval between shots.
        /// </summary>
        private float shootInterval;

        /// <summary>
        /// The content manager for loading assets.
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyB"/> class.
        /// </summary>
        /// <param name="velocity">The speed of EnemyB.</param>
        /// <param name="position">The position of EnemyB.</param>
        /// <param name="texture">The texture of EnemyB.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>"
        public EnemyB(Vector2 velocity, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(velocity, position, texture)
        {
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootTimer = 0f;
            this.shootInterval = this.GetRandomShootInterval();
        }

        /// <summary>
        /// Update method for updating EnemyB instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
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
        /// Draw method for drawing EnemyB instances.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch the sprite belongs to.</param>
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
        /// Shoots a bullet.
        /// </summary>
        public void Shoot()
        {
            Vector2 bulletVelocity = new Vector2(0, 1.2f);

            // base.Shoot(bulletVelocity, false);
            this.StarShooting();
        }

        /// <summary>
        /// Will shoot bullets in a regular pattern.
        /// </summary>
        public void RegularShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletOrange");
            Vector2 bulletVelocity = new Vector2(0, 1.2f);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
        }

        /// <summary>
        /// Shoots Bullets in a star pattern.
        /// </summary>
        public void StarShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletOrange");
            float angle = -0.9F;
            for (int i = 0; i < 3; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 2);
                base.Shoot(bulletVelocity, false, bulletTexture);
                angle += 0.6F;
            }
        }

        /// <summary>
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return ((float)this.random.NextDouble() * 3f) + 2f; // Random interval between 2 and 5 seconds
        }
    }
}
