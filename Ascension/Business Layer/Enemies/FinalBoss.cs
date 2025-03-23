// <copyright file="FinalBoss.cs" company="Team Ascension">
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
    /// Creates instances of FinalBoss, a type of Enemy.
    /// </summary>
    internal class FinalBoss : Enemy
    {

        /// <summary>
        /// The bullets for the enemy.
        /// </summary>
        private List<Bullet> bullets;

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

        private float CircularShootingTimer;

        /// <summary>
        /// Interval between shots.
        /// </summary>
        private float shootInterval;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalBoss"/> class.
        /// </summary>
        /// <param name="speed">The speed of FinalBoss.</param>
        /// <param name="position">The position of FinalBoss.</param>
        /// <param name="texture">The texture of FinalBoss A.</param>
        public FinalBoss(Vector2 velocity, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(velocity, position, texture)
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootTimer = 0f;
            this.shootInterval = this.GetRandomShootInterval();
            this.CircularShootingTimer = 0f;
        }

        /// <summary>
        /// Update method for updating MidBoss instances.
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
            this.CircularShootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.shootTimer >= this.shootInterval)
            {
                //this.Shoot();
                this.shootTimer = 0f;
                this.shootInterval = this.GetRandomShootInterval();
            }
        }

        /// <summary>
        /// Draw method for drawing MidBoss instances.
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
        /// Shoot method.
        /// </summary>
        /// <exception cref="NotImplementedException">Throws exception not implemented.</exception>
        //public override void Shoot()
        //{
        //    if (this.CircularShootingTimer >= 2f)
        //    {
        //        this.CircularShootingTimer = 0f;
        //        this.CircularShooting();
        //    }
        //    else
        //    {
        //        this.StarShooting();
        //    }
        //}

        /// <summary>
        /// Shoots a bullet Shaped Like a star.
        /// </summary>
        public void StarShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletGreen");
            float angle = -0.5F;
            for (int i = 0; i < 5; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 2f);
                Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
                this.bullets.Add(bullet);
                angle += 0.2F;
            }
        }

        /// <summary>
        /// Shoots bullets in a circular pattern.
        /// </summary>
        public void CircularShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletGreen");
            int numberOfBullets = 12; // Total bullets in the circles
            float bulletSpeed = 3f; // Adjust the speed as needed
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
                this.bullets.Add(bullet);
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
