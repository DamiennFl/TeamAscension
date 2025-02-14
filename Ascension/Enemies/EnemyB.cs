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
        public EnemyB(int speed, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(speed, position, texture, "EnemyB")
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Update method for updating EnemyB instances.
        /// </summary>
        /// <param name="gameTime">GameTime to sync with runtime.</param>
        public override void Update(GameTime gameTime)
        {
            // Basic movement: move down
            this.Position = new Vector2(this.Position.X, this.Position.Y + (this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            for (int i = 0; i < this.bullets.Count; i++)
            {
                this.bullets[i].BulletUpdate(gameTime);
                if (!this.bullets[i].IsActive)
                {
                    this.bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Draw method for drawing EnemyB instances.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch the sprite belongs to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw stuff
        }

        /// <summary>
        /// Shoot method for shooting bullets.
        /// </summary>
        public override void Shoot()
        {
            this.StarShooting();
        }

        /// <summary>
        /// Will shoot bullets in a regular pattern.
        /// </summary>
        public void RegularShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue");
            Vector2 bulletVelocity = new Vector2(0, 1.2f);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
            this.bullets.Add(bullet);
        }

        /// <summary>
        /// Shoots Bullets in a star pattern.
        /// </summary>
        public void StarShooting()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue");
            float angle = -0.5F;
            for (int i = 0; i < 5; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 2);
                Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
                this.bullets.Add(bullet);
                angle += 0.2F;
            }
        }
    }
}
