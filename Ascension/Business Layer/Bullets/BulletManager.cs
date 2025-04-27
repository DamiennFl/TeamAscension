// <copyright file="BulletManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Ascension.Business_Layer.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Bullet manager class.
    /// </summary>
    public class BulletManager
    {
        /// <summary>
        /// List of bullets.
        /// </summary>
        private List<Bullet> bullets = new List<Bullet>();

        /// <summary>
        /// Collision manager for the bullets.
        /// </summary>
        private CollisionManager collisionManager;

        /// <summary>
        /// To create different kinds of bullets.
        /// </summary>
        private BulletFactory bulletFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletManager"/> class.
        /// </summary>
        /// <param name="contentManager">for loading textures.</param>
        /// <param name ="collisionManager">for loading collisions.</param>
        public BulletManager(CollisionManager collisionManager, ContentManager contentManager)
        {
            this.collisionManager = collisionManager;
            this.bulletFactory = new BulletFactory(contentManager);
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
        /// <param name="bulletType">Type of bullet.</param>
        private void OnBulletFired(Vector2 pos, Vector2 velo, bool isPlayerBullet, string bulletType)
        {
            Bullet bullet = this.bulletFactory.CreateBullet(velo, pos, bulletType);
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
            for (int i = this.bullets.Count - 1; i >= 0; i--)
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
            // Added this line here since we were getting,
            // modified collection errors.
            foreach (var item in this.bullets.ToArray())
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
                if (!this.bullets[i].IsPlayerBullet)
                {
                    this.bullets[i].IsActive = false;
                    this.collisionManager.Unregister(this.bullets[i]);
                    this.bullets.RemoveAt(i);
                }
            }
        }
    }
}
