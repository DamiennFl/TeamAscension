using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    internal class LinearMovementPattern : IMovementPattern
    {
        public LinearMovementPattern(Vector2 velocity)
        {
            this.Velocity = velocity;
            this.isComplete = false;
        }

        public Vector2 Velocity { get; set; }
        private bool isComplete;

        public void Update(GameTime gameTime, Enemy enemy)
        {
            enemy.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Assuming linear movement never completes on its own
        }

        public bool IsComplete()
        {
            return this.isComplete;
        }
    }
}