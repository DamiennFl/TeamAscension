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
        /// <param name="bulletTexture">Color and texture.</param>
        /// <returns>New bullet.</returns>
        public Bullet CreateBullet(Vector2 velocity, Vector2 bulletPosition, string bulletTexture, string bulletType)
        {
            Texture2D texture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue"); // default bullet texture if not valid texture input.
            if (this.bulletTextures.ContainsKey(bulletTexture))
            {
                texture = this.bulletTextures[bulletTexture];
            }

            switch (bulletType)
            {
                case "A":
                    Bullet bulletA = new BulletA(velocity, bulletPosition, texture);
                    bulletA.MovementPattern = this.movementFactory.CreateMovementPattern("Linear");
                    return bulletA;
                case "B":
                    Bullet bulletB = new BulletB(velocity, bulletPosition, texture);
                    bulletB.MovementPattern = this.movementFactory.CreateMovementPattern("Wave");
                    return bulletB;
                default:
                    Bullet bullet = new BulletA(velocity, bulletPosition, texture);
                    bullet.MovementPattern = this.movementFactory.CreateMovementPattern("Linear");
                    return bullet;
            }
        }
    }
}