using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    internal class MoveToPositionPattern : IMovementPattern
    {
        private Vector2 targetPosition;
        private Vector2 velocity;
        private bool isComplete;

        public MoveToPositionPattern(Vector2 targetPosition, Vector2 velocity)
        {
            this.targetPosition = targetPosition;
            this.velocity = velocity;
            this.isComplete = false;
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            if (this.isComplete)
            {
                return;
            }

            Vector2 direction = this.targetPosition - enemy.Position;
            if (direction.Length() > this.velocity.Length() * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                direction.Normalize();
                enemy.Position += direction * this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                enemy.Position = this.targetPosition;
                this.isComplete = true;
            }
        }

        public bool IsComplete()
        {
            return this.isComplete;
        }
    }
}