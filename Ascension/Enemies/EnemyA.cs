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
    /// Creates instances of EnemyA, a type of Enemy.
    /// </summary>
    internal class EnemyA : Enemy
    {
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
        /// Initializes a new instance of the <see cref="EnemyA"/> class.
        /// </summary>
        /// <param name="speed">The speed of EnemyA.</param>
        /// <param name="texture">The texture of Enemy A.</param>
        /// <param name="contentManager">The content manager for loading assets.</param>
        public EnemyA(int speed, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(speed, position, texture, "EnemyA")
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Draw method for drawing the sprite.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        /// <summary>
        /// Update method for updating EnemyA.
        /// </summary>
        /// <param name="gameTime">Current gametime.</param>
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
        /// Shoot method for shooting bullets.
        /// </summary>
        public override void Shoot()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("ball");
            Vector2 bulletVelocity = new Vector2(0, 5);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
            this.bullets.Add(bullet);
        }
    }
}
