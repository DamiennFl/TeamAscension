using System.Collections.Generic;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    internal class EnemyA : Enemy
    {
        private Texture2D bulletTexture;
        private List<Bullet> bullets;
        private ContentManager contentManager;

        public EnemyA(int speed, Vector2 position, Texture2D texture, ContentManager contentManager)
        : base(speed, position, texture, "EnemyA")
        {
            this.bullets = new List<Bullet>();
            this.contentManager = contentManager;
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
        }

        public override void Shoot()
        {
            Texture2D bulletTexture = this.contentManager.Load<Texture2D>("ball");
            Vector2 bulletVelocity = new Vector2(0, 5);
            Bullet bullet = new Bullet(1, bulletVelocity, this.Position, bulletTexture);
            this.bullets.Add(bullet);
        }
    }
}