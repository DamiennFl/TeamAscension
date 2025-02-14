using System.Collections.Generic;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies
{
    internal abstract class Enemy
    {
        protected Texture2D texture;
        protected Queue<IMovementPattern> movementPatterns = new Queue<IMovementPattern>();

        public Enemy(int speed, Vector2 position, Texture2D texture, string enemyType)
        {
            this.Speed = speed;
            this.texture = texture;
            this.Position = position;
            this.EnemyType = enemyType;
        }

        public string EnemyType { get; set; }
        public int Speed { get; set; }
        public Vector2 Position { get; set; }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Shoot();

        public void AddMovementPattern(IMovementPattern pattern)
        {
            this.movementPatterns.Enqueue(pattern);
        }

        public void ClearMovementPatterns()
        {
            this.movementPatterns.Clear();
        }

        protected void UpdateMovementPatterns(GameTime gameTime)
        {
            if (this.movementPatterns.Count > 0)
            {
                var currentPattern = this.movementPatterns.Peek();
                currentPattern.Update(gameTime, this);

                if (currentPattern.IsComplete())
                {
                    this.movementPatterns.Dequeue();
                }
            }
        }
    }
}