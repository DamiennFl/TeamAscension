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

        private bool shootOption = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalBoss"/> class.
        /// </summary>
        /// <param name="velocity">The speed of FinalBoss.</param>
        /// <param name="position">The position of FinalBoss.</param>
        /// <param name="texture">The texture of FinalBoss A.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>
        /// <param name="bulletType">The type of bullet to shoot.</param>
        public FinalBoss(Vector2 velocity, Vector2 position, int health, Texture2D texture, ContentManager contentManager, string bulletType)
        : base(velocity, position, health, texture, bulletType)
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootTimer = 0f;
            this.shootInterval = this.GetRandomShootInterval();
            this.CircularShootingTimer = 0f;
            this.font = contentManager.Load<SpriteFont>("Fonts/Font");
        }

        /// <summary>
        /// Update method for updating MidBoss instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
        public override void Update(GameTime gameTime)
        {
            this.MovementPattern.Move(gameTime, this);

            // Timer for shooting
            this.shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.CircularShootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.shootTimer >= this.shootInterval)
            {
                this.Shoot();
                this.shootTimer = 0f;
                this.shootInterval = 2f;
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
               new Vector2(this.texture.Width / 2, this.texture.Height / 2),
               new Vector2(this.Scale, this.Scale),
               SpriteEffects.None,
               0f);
            spriteBatch.DrawString(this.font, string.Empty + this.Health, new Vector2(this.Bounds.X, this.Bounds.Y), Color.Red);
        }

        /// <summary>
        /// Shoots a bullet.
        /// </summary>
        public override void Shoot()
        {
            if (this.shootOption)
            {
                this.FireworkExplosionShoot();
                this.shootOption = false;
            }
            else
            {
                this.BulletWall();
                this.shootOption = true;
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
