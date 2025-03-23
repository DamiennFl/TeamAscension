using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ascension
{
    /// <summary>
    /// Bullet manager class.
    /// </summary>
    internal class BulletManager
    {
        private List<Bullet> bullets = new List<Bullet>();

        private Texture2D enemyBulletTexture;

        private Texture2D playerBulletTexture;

        private CollisionManager collisionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletManager"/> class.
        /// </summary>
        /// <param name="contentManager">for loading textures.</param>
        public BulletManager(ContentManager contentManager)
        {
            this.enemyBulletTexture = contentManager.Load<Texture2D>("Bullets/BulletBlue");
            this.playerBulletTexture = contentManager.Load<Texture2D>("Bullets/BulletOrange");
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
        /// OnBulletFired event.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="velo">Velocity.</param>
        /// <param name="isPlayerBullet">Bool if bullet is shot by the player.</param>
        private void OnBulletFired(Vector2 pos, Vector2 velo, bool isPlayerBullet)
        {
            Texture2D bulletTexture = isPlayerBullet ? this.playerBulletTexture : this.enemyBulletTexture;
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
                    this.bullets.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in this.bullets)
            {
                item.BulletDraw(spriteBatch);
            }
        }
    }
}
