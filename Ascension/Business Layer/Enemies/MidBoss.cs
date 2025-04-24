// <copyright file="MidBoss.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Business_Layer.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Creates instances of MidBoss, a type of Enemy.
    /// </summary>
    internal class MidBoss : Enemy
    {
        /// <summary>
        /// The content manager for loading assets.
        /// </summary>

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
        /// Initializes a new instance of the <see cref="MidBoss"/> class.
        /// </summary>
        /// <param name="velocity">The speed of MidBoss.</param>
        /// <param name="position">The position of MidBoss.</param>
        /// <param name="texture">The texture of MidBoss A.</param>
        /// <param name="contentManager">"The content manager for loading assets.</param>
        /// <param name="bulletType">The type of bullet to shoot.</param>
        public MidBoss(Vector2 velocity, Vector2 position, int health, Texture2D texture, ContentManager contentManager, string bulletType, float shotsPerSecond)
        : base(velocity, position, health, texture, bulletType, shotsPerSecond)
        {
            this.random = new Random();
            this.shootTimer = 0f;
            //this.shootInterval = this.GetRandomShootInterval();
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

            if (this.shootTimer >= this.shootInterval/ this.shotsPerSecond)
            {
                this.Shoot();
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
               new Vector2(this.texture.Width / 2, this.texture.Height / 2),
               new Vector2(this.Scale, this.Scale),
               SpriteEffects.None,
               0f);
            spriteBatch.DrawString(this.font, string.Empty + this.Health, new Vector2(this.Bounds.X, this.Bounds.Y), Color.Red);
        }

        /// <summary>
        /// Shoot method.
        /// </summary>
        /// <exception cref="NotImplementedException">Throws exception not implemented.</exception>
        //public override void Shoot()
        //{
        //    if (this.shootCounter % 3 == 0)
        //    {
        //        this.FlowerBloomShoot();
        //        this.shootCounter++;
        //    }
        //    else
        //    {
        //        this.AlternatingRingsShoot();
        //        this.shootCounter++;
        //    }
        //}

        /// <summary>
        /// Gets a random shoot interval.
        /// </summary>
        /// <returns>the random time generated for our next shot.</returns>
        private float GetRandomShootInterval()
        {
            return ((float)this.random.NextDouble() * 1f) + 1.5f; // Random interval between 0 and 1 second
        }
    }
}