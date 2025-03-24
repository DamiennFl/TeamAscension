// <copyright file="BulletManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Bullet manager class.
    /// </summary>
    internal class BulletManager
    {
        /// <summary>
        /// List of bullets.
        /// </summary>
        private List<Bullet> bullets = new List<Bullet>();

        /// <summary>
        /// Enemy bullet texture.
        /// </summary>
        private Texture2D enemyBulletTexture;

        /// <summary>
        /// Player bullet texture.
        /// </summary>
        private Texture2D playerBulletTexture;

        /// <summary>
        /// Collision manager for the bullets.
        /// </summary>
        private CollisionManager collisionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletManager"/> class.
        /// </summary>
        /// <param name="contentManager">for loading textures.</param>
        /// <param name ="collisionManager">for loading collisions.</param>
        public BulletManager(ContentManager contentManager, CollisionManager collisionManager)
        {
            this.enemyBulletTexture = contentManager.Load<Texture2D>("Bullets/BulletBlue");
            this.playerBulletTexture = contentManager.Load<Texture2D>("Bullets/BulletOrange");
            this.collisionManager = collisionManager;
        }

        /// <summary>
        /// Register each enemy.
        /// </summary>
        /// <param name="enemy">Enemy.</param>
        public void RegisterEnemy(Enemy enemy)
        {
            enemy.BulletFired += this.OnBulletFired;
        }

        /// <summary>
        /// Registers the player.
        /// </summary>
        /// <param name="player">Player.</param>
        public void RegisterPlayer(Player player)
        {
            player.BulletFired += this.OnBulletFired;
        }

        /// <summary>
        /// OnBulletFired event.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="velo">Velocity.</param>
        /// <param name="isPlayerBullet">Bool if bullet is shot by the player.</param>
        /// <param name="bulletTexture">Texture of the bullet.</param>
        private void OnBulletFired(Vector2 pos, Vector2 velo, bool isPlayerBullet, Texture2D bulletTexture)
        {
            Bullet bullet = new Bullet(1, velo, pos, bulletTexture);
            this.collisionManager.Register(bullet);
            bullet.IsPlayerBullet = isPlayerBullet;
            this.bullets.Add(bullet);
        }

        /// <summary>
        /// Updates all bullets.
        /// </summary>
        /// <param name="gTime">Current Gametime.</param>
        public void Update(GameTime gTime)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                this.bullets[i].BulletUpdate(gTime);
                if (!this.bullets[i].IsActive)
                {
                    this.collisionManager.Unregister(this.bullets[i]);
                    this.bullets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draw method for each bullet.
        /// </summary>
        /// <param name="spriteBatch">Sprite.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in this.bullets)
            {
                item.BulletDraw(spriteBatch);
            }
        }

        /// <summary>
        /// Clears the screen of bullets.
        /// </summary>
        public void ClearScreen()
        {
            for (int i = this.bullets.Count - 1; i >= 0; i--)
            {
                this.bullets[i].IsActive = false;
                this.bullets.RemoveAt(i);
            }
        }
    }
}
