// <copyright file="EnemyB.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Bullets;
using Ascension.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
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
        /// The bullets for the enemy.
        /// </summary>
        private List<Bullet> bullets;

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
        /// <param name="speed">The speed of EnemyB.</param>
        /// <param name="position">The position of EnemyB.</param>
        /// <param name="texture">The texture of EnemyB.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>"
        public EnemyB(Vector2 velocity, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(velocity, position, texture)
        {
            this.bullets = new List<Bullet>();
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

            for (int i = 0; i < this.bullets.Count; i++)
            {
                this.bullets[i].BulletUpdate(gameTime);
                if (!this.bullets[i].IsActive)
                {
                    this.bullets.RemoveAt(i);
                    i--;
                }
            }

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
                new Vector2(this.texture.Width * 0.4f, this.texture.Height * 0.4f),
                new Vector2(0.4F, 0.4F),
                SpriteEffects.None,
                0f);

            foreach (var bullet in this.bullets)
            {
                bullet.BulletDraw(spriteBatch);
            }
        }

        /// <summary>
        /// Shoot method for shooting bullets.
        /// </summary>
        public void Shoot()
        {
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
            this.bullets.Add(bullet);
        }

        /// <summary>
        /// Shoots Bullets in a star pattern.
        /// </summary>
        public void StarShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletOrange");
            float angle = -0.5F;
            for (int i = 0; i < 5; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 2);
                Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
                this.bullets.Add(bullet);
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
