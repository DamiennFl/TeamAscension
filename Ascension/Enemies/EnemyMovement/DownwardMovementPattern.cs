using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    internal class DownwardMovementPattern : IMovementPattern
    {
        private Vector2 targetPosition;
        private Vector2 velocity;
        private bool isComplete;
       

        public DownwardMovementPattern(Vector2 targetPosition, Vector2 velocity)
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

            // Wait until the start time is reached

            Vector2 direction = this.targetPosition - enemy.Position;
            float distance = direction.Length();
            float step = this.velocity.Length() * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (distance > step)
            {
                direction.Normalize();
                enemy.Position += direction * step;
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
