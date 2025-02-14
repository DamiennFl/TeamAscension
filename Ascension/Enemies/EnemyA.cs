using System.Collections.Generic;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ascension.Enemies
{
    internal class EnemyA : Enemy
    {
        private Texture2D bulletTexture;
        private List<Bullet> bullets;
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
        /// <param name="speed">The speed of EnemyA.</param>
        /// <param name="position">The position of EnemyA.</param>
        /// <param name="texture">The texture of Enemy A.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>
        public EnemyA(int speed, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(speed, position, texture, "EnemyA")
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
            this.random = new Random();
            this.shootTimer = 0f;
            this.shootInterval = this.GetRandomShootInterval();
        }

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

        public override void Update(GameTime gameTime)
        {
            this.UpdateMovementPatterns(gameTime);

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

        public override void Shoot()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue");
            Vector2 bulletVelocity = new Vector2(0, 2);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
            this.bullets.Add(bullet);
        }

        /// <summary>
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return (float)this.random.NextDouble() * 2f + 1f; // Random interval between 1 and 3 seconds
        }

    }
}