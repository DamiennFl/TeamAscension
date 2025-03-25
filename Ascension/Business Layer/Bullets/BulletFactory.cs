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
            this.bulletTextures = new Dictionary<string, Texture2D>
            {
                { "Orange", this.contentManager.Load<Texture2D>("Bullets/BulletOrange") },
                { "Blue", this.contentManager.Load<Texture2D>("Bullets/BulletBlue") },
                { "Green", this.contentManager.Load<Texture2D>("Bulets/BulletGreen") },
            };
        }

        /// <summary>
        /// Create a bullet.
        /// </summary>
        /// <param name="damage">Damage of bullet.</param>
        /// <param name="velocity">Direction and speed.</param>
        /// <param name="bulletPosition">Start position.</param>
        /// <param name="bulletTexture">Color and texture.</param>
        /// <returns>New bullet.</returns>
        public Bullet CreateBullet(int damage, Vector2 velocity, Vector2 bulletPosition, string bulletTexture)
        {
            Texture2D texture = this.contentManager.Load<Texture2D>("Bullets/BulletBlue"); // default bullet texture if not valid texture input.
            if (this.bulletTextures.ContainsKey(bulletTexture))
            {
                texture = this.bulletTextures[bulletTexture];
            }

            return new BulletA(damage, velocity, bulletPosition, texture);
        }
    }
}