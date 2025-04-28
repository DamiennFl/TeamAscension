// <copyright file="BulletFactory.cs" company="Team Ascension">
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

namespace Ascension.Business_Layer.Bullets
{
    /// <summary>
    /// Factory class for creating bullets.
    /// </summary>
    internal class BulletFactory
    {
        /// <summary>
        /// Dictionary containing textures.
        /// </summary>
        private Dictionary<string, Texture2D> bulletTextures;

        /// <summary>
        /// Content manager for textures. 
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Movement factory for creating movement patterns.
        /// </summary>
        private MovementFactory movementFactory = new MovementFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletFactory"/> class.
        /// </summary>
        /// <param name="contentManager">ContentManager.</param>
        /// <param name="graphicsDevice">Graphics device.</param>
        /// <param name="collisionManager">Collision manager.</param>
        public BulletFactory(ContentManager contentManager)
        {
            this.contentManager = contentManager; // Initialize the contentManager field

            this.bulletTextures = new Dictionary<string, Texture2D>
            {
                { "Orange", this.contentManager.Load<Texture2D>("Bullets/BulletOrange") },
                { "Blue", this.contentManager.Load<Texture2D>("Bullets/BulletBlue") },
                { "Green", this.contentManager.Load<Texture2D>("Bullets/BulletGreen") },
            };
        }

        /// <summary>
        /// Create a bullet.
        /// </summary>
        /// <param name="velocity">Direction and speed.</param>
        /// <param name="bulletPosition">Start position.</param>
        /// <param name="bulletType">Color and texture.</param>
        /// <returns>New bullet.</returns>
        public Bullet CreateBullet(Vector2 velocity, Vector2 bulletPosition, string bulletType)
        {
            switch (bulletType)
            {
                case "A":
                    Bullet bulletA = new BulletA(velocity, bulletPosition, this.contentManager.Load<Texture2D>("Bullets/BulletGreen"));
                    bulletA.MovementPattern = this.movementFactory.CreateMovementPattern("Linear");
                    return bulletA;
                case "B":
                    Bullet bulletB = new BulletB(velocity, bulletPosition, this.contentManager.Load<Texture2D>("Bullets/BulletOrange"));
                    bulletB.MovementPattern = this.movementFactory.CreateMovementPattern("Wave");
                    return bulletB;
                case "C":
                    Bullet playerBullet = new BulletA(velocity, bulletPosition, this.contentManager.Load<Texture2D>("Bullets/BulletBlue"));
                    playerBullet.MovementPattern = this.movementFactory.CreateMovementPattern("Linear");
                    return playerBullet;
                default:
                    Bullet bullet = new BulletA(velocity, bulletPosition, this.contentManager.Load<Texture2D>("Bullets/BulletGreen"));
                    bullet.MovementPattern = this.movementFactory.CreateMovementPattern("Linear");
                    return bullet;
            }
        }
    }
}